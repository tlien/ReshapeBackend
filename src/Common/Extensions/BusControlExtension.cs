using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;

namespace Reshape.Common.EventBus.Services
{
    public static class BusControlExtension
    {
        public static async Task PublishIntegrationEvent(this IBusControl busControl, string content, Type eventType)
        {
            // Publish message
            // TODO: See if this can't be done with System.Text.Json instead
            var message = JsonConvert.DeserializeObject(content, eventType);
            await busControl.Publish(message, messageType: eventType);
            return;
        }
    }
}