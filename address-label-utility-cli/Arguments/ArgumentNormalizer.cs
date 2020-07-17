using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Arguments.Csv;
using AddressLabelUtilityCli.Arguments.Pdf;
using AddressLabelUtilityCli.Execution;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Csv.Inference;

namespace AddressLabelUtilityCli.Arguments
{
    internal class ArgumentNormalizer : IArgumentNormalizer
    {
        private readonly Inferencer _inferencer;

        public ArgumentNormalizer()
        {
            this._inferencer = new Inferencer();
        }

        public IEnumerable<IArgument> Normalize(IEnumerable<IArgument> args)
        {
            var commonArguments = this.NormalizeCommonArguments(args);

            if (args.Contains<ExecutionCsvArgument>())
            {
                return commonArguments.Concat(this.NormalizeCsvArguments(args));
            }

            if (args.Contains<ExecutionPdfArgument>())
            {
                return commonArguments.Concat(this.NormalizePdfArguments(args));
            }

            return commonArguments;
        }

        private IEnumerable<IArgument> NormalizeCommonArguments(IEnumerable<IArgument> args)
        {
            if (args.Contains<HelpArgument>())
            {
                yield return args.Get<HelpArgument>();
            }

            var srcPath = args.Get<Src1Argument>();

            yield return srcPath;

            if (args.Contains<SrcType1Argument>())
            {
                yield return args.Get<SrcType1Argument>();
            }
            else
            {
                var type = this._inferencer.Infer(srcPath.Argument);

                yield return new SrcType1Argument
                {
                    Argument = CsvResolver.ResolveArgument(type),
                };
            }

            if (args.Contains<DestArgument>())
            {
                yield return args.Get<DestArgument>();
            }
            else
            {
                yield return new DestArgument
                {
                    Argument = Path.Combine(Path.GetDirectoryName(srcPath.Argument),
                        $"output{(args.Contains<ExecutionCsvArgument>() ? ".csv" : ".pdf")}"),
                };
            }
        }

        private IEnumerable<IArgument> NormalizeCsvArguments(IEnumerable<IArgument> args)
        {
            if (args.Contains<DestTypeArgument>())
            {
                yield return args.Get<DestTypeArgument>();
            }
            else
            {
                yield return new DestTypeArgument();
            }
        }

        private IEnumerable<IArgument> NormalizePdfArguments(IEnumerable<IArgument> args)
        {
            var src2Path = args.Get<Src2Argument>();

            yield return src2Path;

            if (args.Contains<SrcType2Argument>())
            {
                yield return args.Get<SrcType2Argument>();
            }
            else
            {
                var type = this._inferencer.Infer(src2Path.Argument);

                yield return new SrcType2Argument
                {
                    Argument = CsvResolver.ResolveArgument(type),
                };
            }

            if (args.Contains<DpiArgument>())
            {
                yield return args.Get<DpiArgument>();
            }
            else
            {
                yield return new DpiArgument();
            }

            if (args.Contains<VisibleLineArgument>())
            {
                yield return args.Get<VisibleLineArgument>();
            }

            if (args.Contains<LineWidthArgument>())
            {
                yield return args.Get<LineWidthArgument>();
            }
            else
            {
                yield return new LineWidthArgument();
            }

            if (args.Contains<MarginArgument>())
            {
                yield return args.Get<MarginArgument>();
            }
            else
            {
                yield return new MarginArgument();
            }

            if (args.Contains<ParPageArgument>())
            {
                yield return args.Get<ParPageArgument>();
            }
            else
            {
                yield return new ParPageArgument();
            }
        }
    }
}
