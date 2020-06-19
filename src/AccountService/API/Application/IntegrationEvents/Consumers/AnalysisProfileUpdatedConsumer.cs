using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    public class AnalysisProfileUpdatedConsumer : IConsumer<AnalysisProfileUpdated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public AnalysisProfileUpdatedConsumer(ILogger<AnalysisProfileUpdatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<AnalysisProfileUpdated> context)
        {
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var analysisProfile = await _repository.UpdateAnalysisProfile(new AnalysisProfile(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("AN ANALYSIS PROFILE HAS BEEN UPDATED! ヾ(^▽^*)))");
            _logger.LogDebug("Updated AnalysisProfile: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", analysisProfile.Id, analysisProfile.Name, analysisProfile.Description, analysisProfile.Price);
            return;
        }
    }
}