using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AddressLabelUtility.Models.Csv;
using AddressLabelUtilityCore.Csv;
using AddressLabelUtilityCore.Csv.Inference;
using AddressLabelUtilityCore.Exceptions;
using AddressLabelUtilityCore.Extensions;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace AddressLabelUtility.ViewModels
{
    internal class CsvConverterViewModel : BindableBase
    {
        #region Private fields

        private readonly CsvTypeInferencer _inferencer;
        private readonly OpenFileDialog _dialog;

        private string _srcPath;
        private string _destPath;
        private CsvKind _srcKind;
        private CsvKind _destKind;
        private string _status;

        private DelegateCommand _runCommand;
        private DelegateCommand _openFileCommand;
        private DelegateCommand<DragEventArgs> _previewDragOverCommand;
        private DelegateCommand<DragEventArgs> _dropCommand;

        #endregion

        #region Properties

        public string SrcPath
        {
            get { return this._srcPath; }
            set { SetProperty(ref this._srcPath, value); }
        }

        public string DestPath
        {
            get { return this._destPath; }
            set { SetProperty(ref this._destPath, value); }
        }

        public CsvKind SrcKind
        {
            get { return this._srcKind; }
            set { this.SetProperty(ref this._srcKind, value); }
        }

        public CsvKind DestKind
        {
            get { return this._destKind; }
            set { this.SetProperty(ref this._destKind, value); }
        }

        public string Status
        {
            get { return this._status; }
            set { SetProperty(ref this._status, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand RunCommand
            => this._runCommand ??= new DelegateCommand(async ()=> { await this.Execute(); });

        public DelegateCommand OpenFileCommand
            => this._openFileCommand ??= new DelegateCommand(async ()=> { await this.OpenFile(); });

        public DelegateCommand<DragEventArgs> PreviewDragOverCommand
            => this._previewDragOverCommand ??= new DelegateCommand<DragEventArgs>(this.PreviewDragOver);

        public DelegateCommand<DragEventArgs> DropCommand
            => this._dropCommand ??= new DelegateCommand<DragEventArgs>(async args => { await this.DropAsync(args); });

        #endregion

        #region Constructor

        public CsvConverterViewModel()
        {
            this._inferencer = new CsvTypeInferencer();

            this._dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "CSV File|*.csv|All|*.*",
            };

            this.SrcKind = CsvKind.デフォルト;
            this.DestKind = CsvKind.デフォルト;
        }

        #endregion

        #region Methods

        public async Task Execute()
        {
            this.Status = "CSV 変換中...";

            try
            {
                await Task.Run(() =>
                {
                    var context = new CsvBuildingContext
                    {
                        ConvertFrom = this.SrcKind,
                        ConvertTo = this.DestKind,
                        SrcPath = this.SrcPath,
                        DestPath = this.DestPath
                    };

                    var builder = new CsvBuilder(context);
                    builder.Build();
                });
            }
            catch (CsvException ex)
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

        public async Task OpenFile()
        {
            if (this._dialog.ShowDialog() == true)
            {
                await this.FillFileInfo(this._dialog.FileName);
            }
        }

        private async Task FillFileInfo(string path)
        {
            this.Status = "ファイル読み込み中...";

            await Task.Run(() =>
            {
                this.SrcPath = path;
                this.DestPath = Path.Combine(Path.GetDirectoryName(path), "output.csv");

                var type = this._inferencer.Infer(path);
                var kind = CsvKindResolver.Resolve(type);

                this.SrcKind = kind;
            });

            this.Status = "ファイル読み込み終了";
        }

        private bool IsCsvFile(string path)
        {
            return path.HasMeaningfulValue() && path.ToLower().EndsWith(".csv");
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


        public async Task DropAsync(DragEventArgs e)
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
                await this.FillFileInfo(file);
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
