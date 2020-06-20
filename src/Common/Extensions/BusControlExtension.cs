using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// <c>MassTransit.IBusControl</c> extension.
    /// </summary>
    public static class BusControlExtension
    {
        /// <summary>
        /// Deserialize the content of the integrationvent to the corresponding event type (The eventbus will only accept objects and not primitives),
        /// then publish the message through the eventbus.
        /// </summary>
        /// <param name="content">The json content of an <c>IntegrationEventLog</c></param>
        /// <param name="eventType">The <c>Type</c> of the <c>IIntegrationEvent</c> to publish</param>
        public static async Task PublishIntegrationEvent(this IBusControl busControl, string content, Type eventType)
        {
            // TODO: Use System.Text.Json when it has matured
            // The System.Text.Json workaround is currently rather cumbersome: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to#deserialize-to-immutable-classes-and-structs
            var message = JsonConvert.DeserializeObject(content, eventType);
            await busControl.Publish(message, messageType: eventType);
            return;
        }
    }
}