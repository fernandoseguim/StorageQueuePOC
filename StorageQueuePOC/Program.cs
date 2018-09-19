using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using StorageQueuePOC.Shared;
using System;
using System.Threading.Tasks;

namespace StorageQueuePOC
{
    internal class Program
    {
        private const string MESSAGE_QUEUE_NAME = "message-queue-poc";
        
        public static async Task Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(StorageAccountSettings.ConnectionString);

            var queueClient = storageAccount.CreateCloudQueueClient();
            var messageQueue = queueClient.GetQueueReference(MESSAGE_QUEUE_NAME);

            Console.WriteLine("Creating queue if not exists...");
            await messageQueue.CreateIfNotExistsAsync();

            Console.WriteLine("Adding messages into queue...");
            for ( var i = 0; i < 64; i++ )
            {
                var message = new CloudQueueMessage($"Message {i} - Hello, Storage Queues from Azure (timespan {DateTime.Now.ToFileTime()})!");
                await messageQueue.AddMessageAsync(message);
            }

            Console.WriteLine("Messages added...");
            Console.ReadKey();
        }
    }
}
