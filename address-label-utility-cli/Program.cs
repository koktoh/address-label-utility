using System;
using System.Text;
using AddressLabelUtilityCli.Execution;
using AddressLabelUtilityCli.Messenger;
using AddressLabelUtilityCli.Parser;
using AddressLabelUtilityCli.Validation;

namespace AddressLabelUtilityCli
{
    class Program
    {
        private static readonly IMessenger _messenger = new ConsoleMessenger();
        private static readonly IParser _parser = new ArgumentParser();
        private static readonly IValidator _validator = new Validator();
        private static readonly IExecutorFactory _executorFactory = new ExecutorFactory();

        static int Main(string[] args)
        {
            // shift-jis を使えるように
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                var parsed = _parser.Parse(args);

                if (!_validator.Validate(parsed, out var message))
                {
                    _messenger.Send(message);

                    return 1;
                }

                var executor = _executorFactory.Create(parsed);

                return executor.Execute(parsed);
            }
            catch (Exception ex)
            {
                _messenger.Send(ex.Message);

                return 1;
            }
        }
    }
}
