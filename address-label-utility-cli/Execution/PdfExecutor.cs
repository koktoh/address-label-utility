using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Arguments.Pdf;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Csv.IO;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtilityCli.Execution
{
    internal class PdfExecutor : ExecutorBase
    {
        public PdfExecutor() : base() { }

        protected override int ExecuteArguments(IEnumerable<IArgument> args)
        {
            var srcPath = args.Get<Src1Argument>();
            var srcType = args.Get<SrcType1Argument>();
            var src2Path = args.Get<Src2Argument>();
            var src2Type = args.Get<SrcType2Argument>();
            var destPath = args.Get<DestArgument>();
            var dpi = args.Get<DpiArgument>();
            var width = args.Get<LineWidthArgument>();
            var margin = args.Get<MarginArgument>();
            var parPage = args.Get<ParPageArgument>();

            var pdfContext = new PdfContext
            {
                FileName = Path.GetFileName(destPath.Argument),
                OutputPath = Path.GetDirectoryName(destPath.Argument),
                Dpi = dpi.GetArgumentAsFloat(),
                IsVisibleSeparateLine = args.Contains<VisibleLineArgument>(),
            };

            var labelContext = new LabelContext
            {
                OutlineWidth = width.GetArgumentAsInt(),
                MarginRatio = margin.GetArgumentAsFloat(),
                ParPage = parPage.GetArgumentAsInt(),
            };

            try
            {
                var toAddressList = CsvReader.Read(CsvResolver.ResolveType(srcType), srcPath.Argument)
                    .Cast<IAddress>();
                var fromAddress = CsvReader.Read(CsvResolver.ResolveType(src2Type), src2Path.Argument)
                    .Cast<IAddress>().First();

                var labelContents = this.BuildLabelContents(toAddressList, fromAddress);

                var drawer = new PdfDrawer(pdfContext, labelContext);
                drawer.Draw(labelContents);

                this._messenger.Send("PDF の作成を終了しました");

                return 0;
            }
            catch (Exception ex)
            {
                this._messenger.Send("PDF の作成でエラーが発生しました");
                this._messenger.Send(ex.Message);

                return 1;
            }
        }

        private IEnumerable<LabelContent> BuildLabelContents(IEnumerable<IAddress> toAddressList, IAddress fromAddress)
        {
            foreach (var toAddress in toAddressList)
            {
                yield return new LabelContent
                {
                    ToAddress = (AddressBase)toAddress,
                    FromAddress = (AddressBase)fromAddress,
                };
            }
        }
    }
}
