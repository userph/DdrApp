using RabbitMQ.Client;
using System.Diagnostics.Metrics;
using System.Text;

namespace Publisher
{
    public class Transaction
    {
        private static int counter = 0;
        private static Random rnd = new Random();
        private static readonly List<string> banks = new List<string> { "Sber", "T-bank", "Vtb", "Alfabank" };
        private static readonly List<string> currancy = new List<string> { "RUB", "DigitalRUB", "EUR", "USD" };


        private static string GenerateRoutingKey()

        {

            return $"{banks[rnd.Next(0, 3)]}.{currancy[rnd.Next(0, 3)]}.{rnd.Next(1000,5001)}";

        }






        public Func<Task> CreateTransactionTask()


        {

            return async () =>

            {

                do
                {
                    await Task.Delay(1000);
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())

                    {
                        channel.ExchangeDeclare(exchange: "exchange_one", type: ExchangeType.Topic);

                        string _routingKey = GenerateRoutingKey();

                        string message = $"Transaction: {_routingKey} winth ID {counter++}";

                        var _body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "exchange_one",
                            routingKey: _routingKey,
                            basicProperties: null,
                            body: _body);

                        Console.WriteLine($"Message |{_routingKey}| is sent into Topic Exchange");



                    }


                } while (true);






            };




        }








    }
}
