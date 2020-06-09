using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    public class NewAnalysisProfileConsumer : IConsumer<IntegrationEvent>
    {
        private readonly ILogger _logger;

        public NewAnalysisProfileConsumer(ILogger<NewAnalysisProfileConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEvent> context)
        {
            await Task.Run(() => _logger.LogDebug("Message received with MassTransit consumer. A NEW ANALYSISPROFILE HAS BEEN CREATED"));
            return;
        }
    }
}