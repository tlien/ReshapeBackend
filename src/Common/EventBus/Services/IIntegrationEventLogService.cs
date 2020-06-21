using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// Maintains a log of integration events going out of the event bus publisher it is registered in.
    /// </summary>
    public interface IIntegrationEventLogService
    {
        /// <summary>
        /// Get integration events marked as 'NotPublished(0)' from the log database.
        /// </summary>
        /// <param name="transactionId">The desired transaction id of the integration events to retrieve.</param>
        /// <returns>An <c>IEnumerable containing the events logged with a given transaction id</c></returns>
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        /// <summary>
        /// Save event to log along with the transaction id of the transaction in which the event was created.
        /// </summary>
        Task SaveEventAsync(IIntegrationEvent @event, IDbContextTransaction transaction);
        /// <summary>
        /// Marks an event as published, meaning it has been successfully sent by the eventbus.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as published</param>
        Task MarkEventAsPublishedAsync(Guid eventId);
        /// <summary>
        /// Marks an event as in progress, meaning it has been retrieved from the log and is awaiting publishing.!--
        /// This also increments the <c>TimesSent</c> property of the event in order to track repeated attempts at publishing.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as in progress</param>
        Task MarkEventAsInProgressAsync(Guid eventId);
        /// <summary>
        /// Marks an event as failed, meaning the event bus could not publish the message.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as failed</param>
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
