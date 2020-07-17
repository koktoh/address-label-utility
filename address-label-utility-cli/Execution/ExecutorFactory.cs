using System;
using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Extensions;

namespace AddressLabelUtilityCli.Execution
{
    internal class ExecutorFactory : IExecutorFactory
    {
        public IExecutor Create(IEnumerable<IArgument> args)
        {
            if (args.Contains<ExecutionCsvArgument>())
            {
                return new CsvExecutor();
            }
            else if (args.Contains<ExecutionPdfArgument>())
            {
                return new PdfExecutor();
            }

            return new DefaultExecutor();
        }
    }
}
