using System;
using System.Threading.Tasks;
using MassTransit;
using Reshape.Common.EventBus.Events;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Consumers
{
    public class NewAnalysisProfileConsumer : IConsumer<IntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IntegrationEvent> context)
        {
            await Console.Out.WriteLineAsync("Message received with MassTransit consumer.");
            return;
        }
    }
}