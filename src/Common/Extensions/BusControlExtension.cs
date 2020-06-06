using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Reshape.Common.EventBus.Services
{
    public static class BusControlExtension
    {
        public static async Task PublishIntegrationEvent(this IBusControl busControl, string content, Type eventType)
        {
            // Specify endpoint based on event name
            var eventName = eventType.Name.ToLower().Split("integrationevent")[0];
            Uri uri = new Uri($"rabbitmq://localhost/{eventName}_queue");
            var endPoint = await busControl.GetSendEndpoint(uri);

            // Send message
            var message = JsonConvert.DeserializeObject(content, eventType);
            await endPoint.Send(message);
            return;
        }
    }
}