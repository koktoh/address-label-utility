using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Arguments.Csv;
using AddressLabelUtilityCli.Arguments.Pdf;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCli.Helper;

namespace AddressLabelUtilityCli.Parse
{
    internal class ArgumentParser : IParser
    {
        private readonly IArgumentResolver _commonArgumentResolver;
        private readonly IArgumentResolver _csvArgumentResolver;
        private readonly IArgumentResolver _pdfArgumentResolver;

        public ArgumentParser()
        {
            this._commonArgumentResolver = new CommonArgumentResolver();
            this._csvArgumentResolver = new CsvArgumentResolver();
            this._pdfArgumentResolver = new PdfArgumentResolver();
        }

        public IEnumerable<IArgument> Parse(string[] args)
        {
            var defaultArgs = this.ParseToDefault(args).ToList();

            for (int i = 0; i < defaultArgs.Count; i++)
            {
                var arg = defaultArgs[i];

                if (this.TryConvertToCommon(arg, out var result)
                    || this.TryConvertToCsv(arg, out result)
                    || this.TryConvertToPdf(arg, out result))
                {
                    if (!result.ShouldHaveArgument || i + 1 >= defaultArgs.Count)
                    {
                        yield return result;
                        continue;
                    }

                    if (result.TryConcat(defaultArgs.ElementAt(i + 1), out result))
                    {
                        i++;
                        yield return result;
                        continue;
                    }

                    yield return result;
                    continue;
                }

                yield return arg;
            }
        }

        private IEnumerable<IArgument> ParseToDefault(string[] args)
        {
            return args.Select(x =>
            {
                return new DefaultArgument
                {
                    Raw = x,
                };
            });
        }

        private bool TryConvertToCommon(IArgument source, out IArgument result)
        {
            result = source;

            if (ArgumentHelper.GetCommonArguments().Contains(source))
            {
                result = this._commonArgumentResolver.Resolve(source);

                return true;
            }

            return false;
        }

        private bool TryConvertToCsv(IArgument source, out IArgument result)
        {
            result = source;

            if (ArgumentHelper.GetCsvArguments().Contains(source))
            {
                result = this._csvArgumentResolver.Resolve(source);

                return true;
            }

            return false;
        }

        private bool TryConvertToPdf(IArgument source, out IArgument result)
        {
            result = source;

            if (ArgumentHelper.GetPdfArguments().Contains(source))
            {
                result = this._pdfArgumentResolver.Resolve(source);

                return true;
            }

            return false;
        }
    }
}
