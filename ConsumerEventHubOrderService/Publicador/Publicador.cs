using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.EventHubs;

namespace Publicador
{
    public class Publicador
    {
        string json = $"{{\"accountId\":\"test-load-g-0\",\"paymentMethod\":\"CREDIT\"}}";
        
        private const string EventHubConnectionString = "";

        private const string EventHubName = "pedidos";
        
        public async Task Publicar(int qtdeMensagens)
        {
            Console.WriteLine($"Publicando {qtdeMensagens} mensagens");  
            
            // var eventHubName = "order_processed";
            //
            // await using (var producer = new EventHubProducerClient(EventHubConnectionString, eventHubName))
            // {
            //     string[] partitionIds = await producer.GetPartitionIdsAsync();
            // }
            
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubConnectionString)
            {
                EntityPath = EventHubName
            };

            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            
            //var d = eventHubClient.RegisteredPlugins;

            for (int i = 0; i <= qtdeMensagens; i++)
            {
                var evento = new EventData(Encoding.UTF8.GetBytes(json));
                evento.Properties.Add("Content-Type", "application/json");
                evento.Properties.Add("country", "BR");
                evento.Properties.Add("requestTraceId", Guid.NewGuid().ToString());
                
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(json)));
            }
        }
    }
}