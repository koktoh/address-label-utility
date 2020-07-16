using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCli.Helper;

namespace AddressLabelUtilityCli.Validation
{
    internal class Validator : IValidator
    {
        public bool Validate(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            if (args.Contains<HelpArgument>())
            {
                return true;
            }

            if (!this.ValidateExecutionType(args, out message))
            {
                return false;
            }

            var isValid = true;
            var builder = new StringBuilder();

            if (!this.ValidateRequiredCommonArguments(args, out message))
            {
                isValid = false;
                builder.AppendLine(message);
            }

            if (args.Contains<ExecutionCsvArgument>()
                && !this.ValidateRequiredCsvArguments(args, out message))
            {
                isValid = false;
                builder.AppendLine(message);
            }

            if (args.Contains<ExecutionPdfArgument>()
                && !this.ValidateRequiredPdfArguments(args, out message))
            {
                isValid = false;
                builder.AppendLine(message);
            }

            if (!this.ValidateArguments(args, out message))
            {
                isValid = false;
                builder.AppendLine(message);
            }

            if (isValid)
            {
                return true;
            }
            else
            {
                message = builder.ToString();
                return false;
            }
        }

        private bool ValidateExecutionType(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            if (args.Contains<ExecutionCsvArgument>() && args.Contains<ExecutionPdfArgument>())
            {
                message = "--csv か --pdf どちらかのみを指定してください";
                return false;
            }

            if (!args.Contains<ExecutionCsvArgument>() && !args.Contains<ExecutionPdfArgument>())
            {
                message = "--csv か --pdf どちらかを指定してください";
                return false;
            }

            return true;
        }

        private bool ValidateRequiredCommonArguments(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            var required = ArgumentHelper.GetCommonArguments().Where(x => x.IsRequired && x.ArgumentKind != ArgumentKind.ExecCsv && x.ArgumentKind != ArgumentKind.ExecPdf);
            var target = args.Where(x => x.IsRequired);

            if (required.All(x => target.Select(y => y.ArgumentKind).Contains(x.ArgumentKind)))
            {
                return true;
            }

            var extracted = this.Extract(required, target);

            message = $"必須オプションが不足しています : {extracted.Select(x => x.Alias.Join(" | ")).Join(", ")}";

            return false;
        }

        private bool ValidateRequiredCsvArguments(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            var required = ArgumentHelper.GetCsvArguments().Where(x => x.IsRequired);
            var target = args.Where(x => x.IsRequired);

            if (required.All(x => target.Select(y => y.ArgumentKind).Contains(x.ArgumentKind)))
            {
                return true;
            }

            var extracted = this.Extract(required, target);

            message = $"CSV 変換の必須オプションが不足しています : {extracted.Select(x => x.Alias.Join(" | ")).Join(",")}";

            return false;
        }

        private bool ValidateRequiredPdfArguments(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            var required = ArgumentHelper.GetPdfArguments().Where(x => x.IsRequired);
            var target = args.Where(x => x.IsRequired);

            if (required.All(x => target.Select(y => y.ArgumentKind).Contains(x.ArgumentKind)))
            {
                return true;
            }

            var extracted = this.Extract(required, target);

            message = $"PDF 作成の必須オプションが不足しています : {extracted.Select(x => x.Alias.Join(" | ")).Join(",")}";

            return false;
        }

        private bool ValidateArguments(IEnumerable<IArgument> args, out string message)
        {
            message = string.Empty;

            var isValid = true;
            var builder = new StringBuilder();

            foreach (var argument in args.Where(x => x.ShouldHaveArgument))
            {
                if (!argument.Validate(out message))
                {
                    isValid = false;
                    builder.AppendLine(message);
                }
            }

            if (isValid)
            {
                return true;
            }
            else
            {
                message = builder.ToString();
                return false;
            }
        }

        private IEnumerable<IArgument> Extract(IEnumerable<IArgument> source, IEnumerable<IArgument> target)
        {
            foreach (var argument in source)
            {
                if (target.Select(x => x.ArgumentKind).Contains(argument.ArgumentKind))
                {
                    continue;
                }

                yield return argument;
            }
        }
    }
}
