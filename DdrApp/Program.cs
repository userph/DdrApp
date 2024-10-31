using RabbitMQ.Client;
using System.Text;


namespace Publisher

{
    class Program
    {
        static async Task Main(string[] args) 
        {
            var transaction = new Transaction();
            var transactionTask = transaction.CreateTransactionTask();

        
            await transactionTask();

            
            Console.ReadLine();
        }
    }
}



