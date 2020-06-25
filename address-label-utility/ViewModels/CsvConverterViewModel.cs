using System.IO;
using System.Linq;
using System.Windows;
using AddressLabelUtility.Models.Csv;
using AddressLabelUtilityCore.Csv.Convert;
using AddressLabelUtilityCore.Csv.Infer;
using AddressLabelUtilityCore.Exceptions;
using AddressLabelUtilityCore.Extensions;
using GongSolutions.Wpf.DragDrop;
using Prism.Commands;
using Prism.Mvvm;

namespace AddressLabelUtility.ViewModels
{
    internal class CsvConverterViewModel : BindableBase, IDropTarget
    {
        #region Private fields

        private readonly Inferencer _inferencer;

        private string _srcPath;
        private string _destPath;
        private ConvertKind _srcKind;
        private ConvertKind _destKind;
        private string _status;

        private DelegateCommand _runCommand;

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

        public ConvertKind SrcKind
        {
            get { return this._srcKind; }
            set { this.SetProperty(ref this._srcKind, value); }
        }

        public ConvertKind DestKind
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
            => this._runCommand ??= new DelegateCommand(this.Execute);

        #endregion

        #region Constructor

        public CsvConverterViewModel()
        {
            this._inferencer = new Inferencer();

            this.SrcKind = ConvertKind.デフォルト;
            this.DestKind = ConvertKind.デフォルト;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            try
            {
                var context = new CsvBuildContext
                {
                    ConvertFrom = this.SrcKind,
                    ConvertTo = this.DestKind,
                    SrcPath = this.SrcPath,
                    DestPath = this.DestPath
                };

                var builder = new CsvBuilder(context);
                builder.Build();
            }
            catch　(CsvException ex)
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

        public void DragOver(IDropInfo dropInfo)
        {
            var file = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().FirstOrDefault();
            dropInfo.Effects = file.IsNullOrWhiteSpace() ? DragDropEffects.None : DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var file = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().FirstOrDefault();

            if (file.IsNullOrWhiteSpace())
            {
                return;
            }

            try
            {
                this.SrcPath = file;
                this.DestPath = Path.Combine(Path.GetDirectoryName(file), "output.csv");

                var type = this._inferencer.Infer(file);
                var kind = ConvertKindResolver.Resolve(type);

                this.SrcKind = kind;
            }
            catch
            {
                this.Status = "ファイル読み込みエラー";
            }
        }

        #endregion
    }
}
