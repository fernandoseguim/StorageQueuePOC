using Microsoft.WindowsAzure.Storage;
using StorageQueuePOC.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace StorageQueuePOC.Client
{
    internal class Program
    {
        private const string MESSAGE_QUEUE_NAME = "message-queue-poc";

        public static async Task Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(StorageAccountSettings.ConnectionString);

            var queueClient = storageAccount.CreateCloudQueueClient();
            var messageQueue = queueClient.GetQueueReference(MESSAGE_QUEUE_NAME);

            Console.WriteLine("Get message list from storage queue...");

            //Only one list of 32 messages can be retrieved at a time
            const int MESSAGE_LIST_LENGTH = 32;
            var retrievedMessage = await messageQueue.GetMessagesAsync(MESSAGE_LIST_LENGTH);

            //This alternative method defines the time interval that the message is visible,
            //it means that the message, after reading, can be only read again after the specified time
            //var retrievedMessage = await messageQueue.GetMessagesAsync(32, TimeSpan.FromMinutes(1), null, null);

            var messages = retrievedMessage.ToList();

            Console.WriteLine("Process messages...");
            messages.ToList().ForEach(message =>
                                      {
                                          Console.WriteLine( message.AsString );
                                          Console.WriteLine(message.NextVisibleTime);
                                      } );

            //Console.WriteLine("Remove messages...");
            //messages.ToList().ForEach(async message => { await messageQueue.DeleteMessageAsync(message); });
            
            Console.ReadKey();
        }
    }
}
