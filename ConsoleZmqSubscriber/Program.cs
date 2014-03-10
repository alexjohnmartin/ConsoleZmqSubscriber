using System;
using System.Collections.Generic;
using System.Text;
using ZeroMQ;

namespace ConsoleZmqSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                ShowUsage();

            SubscribeToMessages(args); 
        }

        private static void SubscribeToMessages(IEnumerable<string> addresses)
        {
            using (var context = ZmqContext.Create())
            {
                using (var subSocket = context.CreateSocket(SocketType.SUB))
                {

                    Console.WriteLine();
                    foreach (var address in addresses)
                    {
                        subSocket.Connect(address); 
                        Console.WriteLine("Listening on " + address);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press ctrl+c to exit...");
                    Console.WriteLine();
                    
                    subSocket.SubscribeAll();
                    while (true)
                    {
                        var message = subSocket.Receive(Encoding.UTF8); 
                        Console.WriteLine(message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine();
            Console.WriteLine("ConsoleZmqSubscriber");
            Console.WriteLine();
            Console.WriteLine("Usage");
            Console.WriteLine("  consoleZmqSubscriber.exe <address> [<address>] [<address>] ...");
            Console.WriteLine();
            Console.WriteLine("e.g. consoleZmqSubscriber.exe tcp://127.0.0.1:6000 tcp://127.0.0.1:6001");
            Console.WriteLine();
        }
    }
}
