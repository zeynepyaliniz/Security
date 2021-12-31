using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMq.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ftiplzny:HjkfuxDy_Tpu-z-Qho4WwU5pn9GBcdgN@fish.rmq.cloudamqp.com/ftiplzny");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            //channel.QueueDeclare("hello-queue", true, false, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue",true,consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Message: {0}",message);
                

            };
            Console.ReadLine();
        }

        //private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
