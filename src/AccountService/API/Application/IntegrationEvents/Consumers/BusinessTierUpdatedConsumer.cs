using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    /// <summary>
    /// Handler for consuming <c>BusinessTierUpdated</c> integration events.
    /// Updates an existing <c>BusinessTier</c> and saves it to the database.
    /// </summary>
    public class BusinessTierUpdatedConsumer : IConsumer<BusinessTierUpdated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public BusinessTierUpdatedConsumer(ILogger<BusinessTierUpdatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<BusinessTierUpdated> context)
        {
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var businessTier = await _repository.UpdateBusinessTier(new BusinessTier(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("A BUSINESSTIER HAS BEEN UPDATED! ヾ(^▽^*)))");
            _logger.LogDebug("Updated BusinessTier: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", businessTier.Id, businessTier.Name, businessTier.Description, businessTier.Price);
            return;
        }
    }
}