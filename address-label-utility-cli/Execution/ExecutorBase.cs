using System;
using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCli.Messenger;

namespace AddressLabelUtilityCli.Execution
{
    internal abstract class ExecutorBase : IExecutor
    {
        protected readonly IArgumentNormalizer _normalizer;
        protected readonly IMessenger _messenger;

        public ExecutorBase()
        {
            this._normalizer = new ArgumentNormalizer();
            this._messenger = new ConsoleMessenger();
        }

        public int Execute(IEnumerable<IArgument> args)
        {
            if (args.Contains<HelpArgument>())
            {
                this._messenger.Send(args.Get<HelpArgument>().GetMessage());

                return 1;
            }

            var normalized = this._normalizer.Normalize(args);

            this.ShowParameters(normalized);

            return this.ExecuteArguments(normalized);
        }


        protected abstract int ExecuteArguments(IEnumerable<IArgument> args);

        protected virtual void ShowParameters(IEnumerable<IArgument> args)
        {
            foreach (var arg in args)
            {
                this._messenger.Send(arg.GetMessage());
            }
        }
    }
}
