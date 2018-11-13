using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using DHLM.Common.Events.EventHQ;

namespace DHLM.Common.Events.AzureEventHub
{
    public class SimpleEventProcessor : IEventProcessor, IEventHQManager
    {   
        public void Register()
        {
            var eventProcessorHost = new EventProcessorHost(
                "EventHubName",
                PartitionReceiver.DefaultConsumerGroupName,
                "EventHubConnectionString",
                "StorageConnectionString",
                "StorageContainerName");

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var data = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                Console.WriteLine($"Message received. Partition: '{context.PartitionId}', Data: '{data}'");
            }
            return context.CheckpointAsync();
        }

        public string Register(Dictionary<string,string> connectionstring)
        {
            return string.Empty;
        }

        event EventHandler  _notify;
        void NotifyOnMessage()
        {
            Notify(new EventArgs());
        }

        event EventHandler Notify
        {
            add
            {
                lock (_lock)
                {
                    _notify += value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notify -= value;
                }
            }
        }


    }
}