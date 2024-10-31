using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace Consumer.Sber

{
    class Program
    {

        static void Main(string[] args)
        {



            // Настройка Dead Letter Exchange
            var deadLetterExchange = "X_dead_letter_exchange";
            var deadLetterQueue = "X_dead_letter_queue";
            var deadLetterKey = "X_dead_letter_key";

            // Настраиваем очередь с использованием Dead Letter Exchange
            var arguments = new Dictionary<string, object>
            {
                  {"x-dead-letter-exchange", deadLetterExchange},
                  {"x-dead-letter-routing-key", deadLetterKey}
            };




            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())

            {

                //основной обменник и очередь

                channel.ExchangeDeclare(exchange: "X_sber_exchange", type: ExchangeType.Topic);
                channel.QueueDeclare(queue: "X_sber_queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: arguments);

                channel.QueueBind(queue: "X_sber_queue",
                                  exchange: "X_sber_exchange",
                                  routingKey: "Sber.#");



                //мёртвый обменник и очередь
                channel.ExchangeDeclare(exchange: deadLetterExchange, type: ExchangeType.Direct);
                channel.QueueDeclare(deadLetterQueue, durable: true, exclusive: false, autoDelete: false);
                channel.QueueBind(deadLetterQueue, deadLetterExchange, deadLetterKey);







                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);


                consumer.Received += (sender, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine("Received message:"+ message);

                    // Логируем перед отклонением
                    Console.WriteLine("Rejecting message...");
                    channel.BasicReject(e.DeliveryTag, requeue: false);



                };



                channel.BasicConsume(queue: "X_sber_queue",
                     autoAck: false,
                     consumer: consumer);



                Console.WriteLine($"Subscribed to the sber_queue");
                Console.ReadLine();




            }


        }



    }



}



