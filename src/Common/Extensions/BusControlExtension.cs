using System;
using System.Threading.Tasks;
using MassTransit;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// <c>MassTransit.IBusControl</c> extension.
    /// </summary>
    public static class BusControlExtension
    {
        /// <summary>
        /// Publish an integration event through the eventbus.
        /// A message type has to be provided in order for the message to be received by the subcribers listening for the corresponding event type.
        /// </summary>
        /// <param name="@event">The integration event to publish represented by the <c>IIntegrationEvent</c> interface</param>
        /// <param name="eventType">The <c>Type</c> of the <c>IIntegrationEvent</c> to publish</param>
        public static async Task PublishIntegrationEvent(this IBusControl busControl, IIntegrationEvent @event, Type eventType)
        {
            await busControl.Publish(message: @event, messageType: eventType);
            return;
        }
    }
}