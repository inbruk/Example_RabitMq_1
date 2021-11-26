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
            var factory = new ConnectionFactory() { HostName="localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("some-queue",false,false,false,null);

                var message = "Some meassage for some queue";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "some-queue", basicProperties:null, body:body);

                Console.WriteLine("Message sent to default exchange !");
            }
            
            Console.ReadKey();
        }
    }
}
