using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace queue_send
{
    class Program
    {
        private static string _bus_connectionstring= "Endpoint=sb://serbusburnley.servicebus.windows.net/;SharedAccessKeyName=programpolicy;SharedAccessKey=9tQPJxDzL4n/JFuohsBozIC2X3dwOlraFJh6JjppCWk=";
        private static string _queue_name = "appque";
        static async Task Main(string[] args)
        {
            //IQueClient is the class obj to handle que
            IQueueClient _client;
            _client = new QueueClient(_bus_connectionstring, _queue_name);
            Console.WriteLine("Sending Messages");
            for (int i = 1; i <=10; i++)
            {
                Order obj = new Order();
                //messages for service bus ques have to be bytes
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                await _client.SendAsync(_message);
                Console.WriteLine($"Sending Message : {obj.Id} ");
            }
        }
        }
}
