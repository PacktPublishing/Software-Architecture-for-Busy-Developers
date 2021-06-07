using Azure;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace packt_serverless_architecture
{
    class Program{
        static async Task Main(string[] args){            
            EventGridPublisherClient client = new EventGridPublisherClient(
                new Uri(Environment.GetEnvironmentVariable("EvGridEndpoint")),
                new AzureKeyCredential(Environment.GetEnvironmentVariable("EvGridAccessKey")));
            while(true){
                await client.SendEventAsync(new EventGridEvent(
               "Serverless",
               "Serverless.OrderEvent",
               "1.0",
               Guid.NewGuid().ToString())
                );
                Thread.Sleep(100);
            }          
        }
    }
}
