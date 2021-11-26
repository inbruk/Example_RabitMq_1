using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace Publisher
{
    class Program
    {
        static void Main(String[] args)
        {
            var counter = 0;
            do
            {
                var timeToSleep = new Random().Next(1000, 3000);
                Thread.Sleep(timeToSleep);

                var factory = new ConnectionFactory() { HostName="localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("some-queue",false,false,false,null);

                    var message = $"Message to some queue N:{counter++}";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: "some-queue", basicProperties:null, body:body);

                    Console.WriteLine(message);
                }
            }
            while (true);

        }
    }
}
