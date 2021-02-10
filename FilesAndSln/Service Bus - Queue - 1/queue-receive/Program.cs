using Microsoft.Azure.ServiceBus;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace queue_receive
{
    class Program
    {
        private static string _bus_connectionstring = "Endpoint=sb://serbusburnley.servicebus.windows.net/;SharedAccessKeyName=programpolicy;SharedAccessKey=9tQPJxDzL4n/JFuohsBozIC2X3dwOlraFJh6JjppCWk=";
        private static string _queue_name = "appque";
        private static QueueClient _client;

        static void Main(string[] args)
        {
            QueueFunction().GetAwaiter().GetResult();
        }

        static async Task QueueFunction()
        {
                _client = new QueueClient(_bus_connectionstring, _queue_name);

                var _options = new MessageHandlerOptions(ExceptionReceived)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };

                _client.RegisterMessageHandler(Process_Message, _options);
            Console.ReadKey();
            }
        
        //method used to receive messages. Receiving is a 2-step process: receive and then delete. 
        //these are done by the CompleteAsync method
        static async Task Process_Message(Message _message,CancellationToken _token)
        {
            Console.WriteLine(Encoding.UTF8.GetString(_message.Body));
            

            await _client.CompleteAsync(_message.SystemProperties.LockToken);
        }

        static Task ExceptionReceived(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
