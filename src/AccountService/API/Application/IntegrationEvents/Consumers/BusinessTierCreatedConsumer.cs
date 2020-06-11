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
            await Task.Run(() => _logger.LogDebug("Message received with MassTransit consumer. A NEW BUSINESSTIER HAS BEEN CREATED!"));
            var lol = context.Message;
            _repository.AddBusinessTier(new BusinessTier(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            return;
        }
    }
}