using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    public class FeatureCreatedConsumer : IConsumer<FeatureCreated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public FeatureCreatedConsumer(ILogger<FeatureCreatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<FeatureCreated> context)
        {
            await Task.Run(() => _logger.LogDebug("Message received with MassTransit consumer. A NEW FEATURE HAS BEEN CREATED!"));
            var lol = context.Message;
            _logger.LogDebug("Message: {lol}", lol);
            return;
        }
    }
}