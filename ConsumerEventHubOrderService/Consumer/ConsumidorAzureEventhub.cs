using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace Consumer
{
    public class ConsumidorAzureEventhub
    {
        private const string StorageConnectionString = "";
        private const string StorageContainerName = "";
        
        private const string EventHubConnectionString = "";

        private const string EventHubName = "pedidos";

        public async void Consumir()
        {
            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);
            
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();
        }
    }
    
    public class SimpleEventProcessor : IEventProcessor
    {
        public Task OpenAsync(PartitionContext context)
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            throw new NotImplementedException();
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            throw new NotImplementedException();
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            throw new NotImplementedException();
        }
    }
}