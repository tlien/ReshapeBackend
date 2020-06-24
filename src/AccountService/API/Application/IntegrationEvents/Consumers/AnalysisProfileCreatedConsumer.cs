using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Consumers
{
    /// <summary>
    /// Handler for consuming <c>AnalysisProfileCreated</c> integration events.
    /// Adds a new <c>AnalysisProfile</c> to the database.
    /// </summary>
    public class AnalysisProfileCreatedConsumer : IConsumer<AnalysisProfileCreated>
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _repository;

        public AnalysisProfileCreatedConsumer(ILogger<AnalysisProfileCreatedConsumer> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<AnalysisProfileCreated> context)
        {
            _logger.LogDebug("Integration event received by {name}\n\tEventId {eventId}\n\tTimeStamp {timeStamp}", GetType().Name, context.Message.EventId, context.Message.TimeStamp);
            var analysisProfile = await _repository.AddAnalysisProfile(new AnalysisProfile(context.Message.Id, context.Message.Name, context.Message.Description, context.Message.Price));
            _logger.LogDebug("A NEW ANALYSIS PROFILE HAS BEEN CREATED! ヾ(^▽^*)))");
            _logger.LogDebug("New AnalysisProfile: \n\tId: {Id}\n\tName: {Name}\n\tDescription: {Description}\n\tPrice: {Price}", analysisProfile.Id, analysisProfile.Name, analysisProfile.Description, analysisProfile.Price);
            return;
        }
    }
}