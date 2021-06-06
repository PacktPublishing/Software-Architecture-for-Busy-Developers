// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace event_consumer
{
    public static class EventGridEventTriggerFunction
    {
        [FunctionName("EventGridEventTriggerFunction")]
        public static void Run(
            ILogger logger,
            [EventGridTrigger] EventGridEvent e)
        {
            logger.LogInformation("Event received {type} {subject}", e.EventType, e.Subject);
        }
    }
}
