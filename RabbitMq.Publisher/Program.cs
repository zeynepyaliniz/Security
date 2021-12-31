using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ftiplzny:HjkfuxDy_Tpu-z-Qho4WwU5pn9GBcdgN@fish.rmq.cloudamqp.com/ftiplzny");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("hello-queue",true,false,false);

            string message = "hello world";
            var messageBody = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);
            Console.WriteLine("Message Sent!!!!");
            Console.ReadLine();
        }
    }
}
