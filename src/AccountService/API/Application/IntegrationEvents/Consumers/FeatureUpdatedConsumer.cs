using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    public class FeatureUpdatedConsumer : IConsumer<FeatureUpdated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public FeatureUpdatedConsumer(ILogger<FeatureUpdatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<FeatureUpdated> context)
        {
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var feature = await _repository.UpdateFeature(new Feature(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("A FEATURE HAS BEEN UPDATED! ヾ(^▽^*)))");
            _logger.LogDebug("Updated Feature: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", feature.Id, feature.Name, feature.Description, feature.Price);
            return;
        }
    }
}