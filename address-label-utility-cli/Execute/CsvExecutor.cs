using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Arguments.Csv;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Csv.Convert;
using AddressLabelUtilityCore.Csv.IO;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCli.Execute
{
    internal class CsvExecutor : ExecutorBase
    {
        public CsvExecutor() : base() { }

        protected override int ExecuteArguments(IEnumerable<IArgument> args)
        {
            var srcPathArg = args.Get<Src1Argument>();
            var srcTypeArg = args.Get<SrcType1Argument>();
            var destPathArg = args.Get<DestArgument>();
            var destTypeArg = args.Get<DestTypeArgument>();

            var srcType = CsvResolver.ResolveType(srcTypeArg);
            var destType = CsvResolver.ResolveType(destTypeArg);

            var srcConvertKind = ConvertKindResolver.Resolve(srcType);
            var destConvertKind = ConvertKindResolver.Resolve(destType);

            try
            {
                var converter = ConverterFactory.Create(srcConvertKind, destConvertKind);

                var records = CsvReader.Read(srcType, srcPathArg.Argument).Cast<ICsvModel>();

                var dest = converter.Convert(records);

                CsvWriter.Write(destType, destPathArg.Argument, dest);

                this._messenger.Send("CSV 変換が終了しました");

                return 0;
            }
            catch (Exception ex)
            {
                this._messenger.Send("CSV 変換でエラーが発生しました");
                this._messenger.Send(ex.Message);
                this._messenger.Send(ex.StackTrace);

                return 1;
            }
        }
    }
}
