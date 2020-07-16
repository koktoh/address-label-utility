using System;

namespace AddressLabelUtilityCli.Messenger
{
    internal class ConsoleMessenger : IMessenger
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
