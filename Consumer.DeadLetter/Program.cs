using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer.DeadLetter
{
    class Program
    {
        static void Main(string[] args)
        {


            /*


            var deadLetterExchange = "X_dead_letter_exchange";
            var deadLetterQueue = "X_dead_letter_queue";
            var deadLetterKey = "X_dead_letter_key";

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: deadLetterQueue, durable: true, exclusive: false, autoDelete: false);
                channel.ExchangeDeclare(deadLetterExchange, ExchangeType.Direct);
                channel.QueueBind(deadLetterQueue, deadLetterExchange, deadLetterKey);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine("Received message: " + message);
                };

                channel.BasicConsume(queue: deadLetterQueue, autoAck: true, consumer: consumer);
                Console.WriteLine($"Subscribed to the queue {deadLetterQueue}");
                Console.ReadLine();
            }



            
            */
        }




    }
}