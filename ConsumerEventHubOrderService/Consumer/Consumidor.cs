using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Newtonsoft.Json;

namespace Consumer
{
    public class Consumidor
    {
        private const string blobStorageConnectionString = "";
        private const string blobContainerName = "";
        
        private const string EventHubConnectionString = "";

        private const string EventHubName = "pedidos";

        private string ThreadName = "";

        public async Task Consumir(string threadName)
        {
            Console.WriteLine($"PROCESSANDO: {threadName}");
            
            ThreadName = threadName;
            
            // Read from the default consumer group: $Default            
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup,
                EventHubConnectionString, EventHubName);
            
            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;
            
            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 10 seconds for the events to be processed
            await Task.Delay(60000);

            // Stop the processing
            await processor.StopProcessingAsync();
        }

        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            var stringJson = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());

            var settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

            var json = JsonConvert.DeserializeObject<PedidoDelta>(stringJson, settings);

            Console.WriteLine($"{ThreadName} = Received event; {json.orderNumber};{json.placementDate };{json.deliveryCenter ?? "N/A"}");
            
            await eventArgs.UpdateCheckpointAsync(CancellationToken.None);
        }

        Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}