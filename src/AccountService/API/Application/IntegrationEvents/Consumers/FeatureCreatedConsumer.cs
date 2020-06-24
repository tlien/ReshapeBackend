using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    /// <summary>
    /// Handler for consuming <c>FeatureCreated</c> integration events.
    /// Adds a new <c>Feature</c> to the database.
    /// </summary>
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
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var feature = await _repository.AddFeature(new Feature(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("A NEW FEATURE HAS BEEN CREATED! ヾ(^▽^*)))");
            _logger.LogDebug("New Feature: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", feature.Id, feature.Name, feature.Description, feature.Price);
            return;
        }
    }
}