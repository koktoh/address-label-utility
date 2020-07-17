using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Arguments.Common;
using AddressLabelUtilityCli.Messenger;

namespace AddressLabelUtilityCli.Execution
{
    internal class DefaultExecutor : IExecutor
    {
        private readonly IMessenger _messenger;

        public DefaultExecutor()
        {
            this._messenger = new ConsoleMessenger();
        }

        public int Execute(IEnumerable<IArgument> args)
        {
            var help = new HelpArgument();

            this._messenger.Send(help.GetMessage());

            return 0;
        }
    }
}
