﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AddressLabelUtility.Models.Pdf;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Csv.Inference;
using AddressLabelUtilityCore.Csv.IO;
using AddressLabelUtilityCore.Exceptions;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace AddressLabelUtility.ViewModels
{
    internal class LabelMakerViewModel : BindableBase
    {
        #region Private fields

        private readonly LabelContext _labelContext;
        private readonly PdfContext _pdfContext;
        private readonly CsvTypeInferencer _inferencer;
        private readonly OpenFileDialog _dialog;

        private string _outputPath;
        private string _fileName;
        private string _toAddressSrcPath;
        private string _fromAddressSrcPath;
        private ObservableCollection<BindableAddress> _toAddressList;
        private ObservableCollection<BindableAddress> _fromAddressList;

        private PdfSizeSet _pdfSize;
        private float _dpi;
        private int _parPage;
        private float _marginRatio;
        private int _lineWidth;
        private bool _isVisibleLine;

        private string _status;

        private DelegateCommand _executeCommand;
        private DelegateCommand _openToAddressFileCommand;
        private DelegateCommand _openFromAddressFileCommand;
        private DelegateCommand<DragEventArgs> _previewDragOverCommand;
        private DelegateCommand<DragEventArgs> _dropToAddressCommand;
        private DelegateCommand<DragEventArgs> _dropFromAddressCommand;

        #endregion

        #region Properties

        public string OutputPath
        {
            get { return this._outputPath; }
            set { this.SetProperty(ref this._outputPath, value); }
        }

        public string FileName
        {
            get { return this._fileName; }
            set { this.SetProperty(ref this._fileName, value); }
        }

        public string ToAddressSrcPath
        {
            get { return this._toAddressSrcPath; }
            set { this.SetProperty(ref this._toAddressSrcPath, value); }
        }

        public string FromAddressSrcPath
        {
            get { return this._fromAddressSrcPath; }
            set { this.SetProperty(ref this._fromAddressSrcPath, value); }
        }

        public ObservableCollection<BindableAddress> ToAddressList
        {
            get { return this._toAddressList; }
            set { this.SetProperty(ref this._toAddressList, value); }
        }

        public ObservableCollection<BindableAddress> FromAddressList
        {
            get { return this._fromAddressList; }
            set { this.SetProperty(ref this._fromAddressList, value); }
        }

        public PdfSizeSet PdfSize
        {
            get { return this._pdfSize; }
            set { this.SetProperty(ref this._pdfSize, value); }
        }

        public float Dpi
        {
            get { return this._dpi; }
            set { this.SetProperty(ref this._dpi, value); }
        }

        public int ParPage
        {
            get { return this._parPage; }
            set { this.SetProperty(ref this._parPage, value); }
        }

        public float MarginRatio
        {
            get { return this._marginRatio; }
            set { this.SetProperty(ref this._marginRatio, value); }
        }

        public int LineWidth
        {
            get { return this._lineWidth; }
            set { this.SetProperty(ref this._lineWidth, value); }
        }

        public bool IsVisibleLine
        {
            get { return this._isVisibleLine; }
            set { this.SetProperty(ref this._isVisibleLine, value); }
        }

        public string Status
        {
            get { return this._status; }
            set { this.SetProperty(ref this._status, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand ExecuteCommand
            => this._executeCommand ??= new DelegateCommand(async () => { await this.ExecuteAsync(); });

        public DelegateCommand OpenToAddressFileCommand
            => this._openToAddressFileCommand ??= new DelegateCommand(async () => { await this.OpenToAddressFileAsync(); });

        public DelegateCommand OpenFromAddressFileCommand
            => this._openFromAddressFileCommand ??= new DelegateCommand(async () => { await this.OpenFromAddressFileAsync(); });

        public DelegateCommand<DragEventArgs> PreviewDragOverCommand
            => this._previewDragOverCommand ??= new DelegateCommand<DragEventArgs>(this.PreviewDragOver);

        public DelegateCommand<DragEventArgs> DropToAddressCommand
            => this._dropToAddressCommand ??= new DelegateCommand<DragEventArgs>(async args => { await this.DropToAddressAsync(args); });

        public DelegateCommand<DragEventArgs> DropFromAddressCommand
            => this._dropFromAddressCommand ??= new DelegateCommand<DragEventArgs>(async args => { await this.DropFromAddressAsync(args); });

        #endregion

        #region Constructor

        public LabelMakerViewModel()
        {
            this._inferencer = new CsvTypeInferencer();

            this._dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "CSV File|*.csv|All|*.*",
            };

            this.OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.FileName = "output.pdf";

            this._labelContext = new LabelContext();
            this._pdfContext = new PdfContext();

            this.PdfSize = this._pdfContext.PdfSizeSet;
            this.Dpi = this._pdfContext.Dpi;
            this.ParPage = this._labelContext.ParPage;
            this.MarginRatio = this._labelContext.MarginRatio;
            this.LineWidth = this._labelContext.OutlineWidth;
            this.IsVisibleLine = this._pdfContext.IsVisibleSeparateLine;

            this.ToAddressList = new ObservableCollection<BindableAddress>();
            this.FromAddressList = new ObservableCollection<BindableAddress>();
        }

        #endregion

        #region Methods

        public async Task ExecuteAsync()
        {
            if (this.ToAddressList.Count(x => x.IsSelected) <= 0)
            {
                this.Status = "宛て先を選択してください";
                return;
            }

            if (this.FromAddressList.Count(x => x.IsSelected) <= 0)
            {
                this.Status = "差出人を選択してください";
                return;
            }

            this.Status = "PDF 作成中...";

            try
            {
                await Task.Run(() =>
                {
                    this._pdfContext.OutputPath = this.OutputPath;
                    this._pdfContext.FileName = this.FileName;
                    this._pdfContext.PdfSizeSet = this.PdfSize;
                    this._pdfContext.Dpi = this.Dpi;
                    this._pdfContext.IsVisibleSeparateLine = this.IsVisibleLine;

                    this._labelContext.OutlineWidth = this.LineWidth;
                    this._labelContext.MarginRatio = this.MarginRatio;
                    this._labelContext.ParPage = this.ParPage;

                    var context = new PdfBuildingContext
                    {
                        PdfContext = this._pdfContext,
                        LabelContext = this._labelContext,
                        ToAddressList = this.ToAddressList.Where(x => x.IsSelected),
                        FromAddress = this.FromAddressList.FirstOrDefault(x => x.IsSelected)
                    };

                    var builder = new PdfBuilder(context);
                    builder.Build();
                });
            }
            catch (Exception ex) when (ex is LayoutException || ex is LabelException || ex is PdfException)
            {
                this.Status = ex.Message;
                return;
            }
            catch
            {
                this.Status = "不明なエラー";
                return;
            }

            this.Status = "出力終了";
        }

        public async Task OpenToAddressFileAsync()
        {
            if (this._dialog.ShowDialog() == true)
            {
                await this.FillToAddressInfo(this._dialog.FileName);
            }
        }

        public async Task OpenFromAddressFileAsync()
        {
            if (this._dialog.ShowDialog() == true)
            {
                await this.FillFromAddressInfo(this._dialog.FileName);
            }
        }

        private async Task FillToAddressInfo(string path)
        {
            this.Status = "宛て先ファイル読み込み中...";

            await Task.Run(() =>
            {
                var type = this.InferCsvModel(path);

                var addressList = CsvReader.Read(type, path)
                    .Cast<IAddress>()
                    .Select(x => new BindableAddress(x));

                this.ToAddressSrcPath = path;
                this.ToAddressList = new ObservableCollection<BindableAddress>(addressList);
            });

            this.Status = "宛て先ファイル読み込み終了";
        }

        private async Task FillFromAddressInfo(string path)
        {
            this.Status = "差出人ファイル読み込み中...";

            await Task.Run(() =>
            {
                var type = this.InferCsvModel(path);

                var addressList = CsvReader.Read(type, path)
                    .Cast<IAddress>()
                    .Select(x => new BindableAddress(x));

                this.FromAddressSrcPath = path;
                this.FromAddressList = new ObservableCollection<BindableAddress>(addressList);
            });

            this.Status = "差出人ファイル読み込み終了";
        }

        private bool IsCsvFile(string path)
        {
            return path.HasMeaningfulValue() && path.ToLower().EndsWith(".csv");
        }

        private Type InferCsvModel(string path)
        {
            return this._inferencer.Infer(path);
        }

        #region Events

        public void PreviewDragOver(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();

            e.Effects = this.IsCsvFile(file) ? DragDropEffects.Copy : DragDropEffects.None;
            e.Handled = true;
        }

        public async Task DropToAddressAsync(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();

            if (!this.IsCsvFile(file))
            {
                return;
            }

            try
            {
                await this.FillToAddressInfo(file);
            }
            catch
            {
                this.Status = "ファイル読み込みエラー";
            }
        }

        public async Task DropFromAddressAsync(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();

            if (!this.IsCsvFile(file))
            {
                return;
            }

            try
            {
                await this.FillFromAddressInfo(file);
            }
            catch
            {
                this.Status = "ファイル読み込みエラー";
            }
        }

        #endregion

        #endregion
    }
}
