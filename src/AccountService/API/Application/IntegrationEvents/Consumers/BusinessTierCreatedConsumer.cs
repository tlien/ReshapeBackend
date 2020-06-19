using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    public class BusinessTierCreatedConsumer : IConsumer<BusinessTierCreated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public BusinessTierCreatedConsumer(ILogger<BusinessTierCreatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<BusinessTierCreated> context)
        {
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var businessTier = await _repository.AddBusinessTier(new BusinessTier(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("A NEW BUSINESSTIER HAS BEEN CREATED! ヾ(^▽^*)))");
            _logger.LogDebug("New BusinessTier: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", businessTier.Id, businessTier.Name, businessTier.Description, businessTier.Price);
            return;
        }
    }
}