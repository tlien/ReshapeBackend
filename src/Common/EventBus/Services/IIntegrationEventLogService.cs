using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// Maintains a log of integration events going out of the event bus publisher (assembly) it is registered in.
    /// DDD dictates that everything happening in a business transaction must be either completed fully or rolled back entirely.
    /// Integration event are therefore tightly bound to the transaction that created the event.
    /// This requirement necessitates the reuse of the DbConnection used for other parts of the transaction, as the unique DbContextTransaction is held by the DbConnection instance.
    /// This is enforced in the IntegrationEventLogService by requiring a DbContextTransaction for a given event in order to persist it to the database.
    /// Failure to provide a transaction id belonging to the current DbContextTransaction when attempting to publish the events will result in the following exception:
    /// System.InvalidOperationException: The specified transaction is not associated with the current connection. Only transactions associated with the current connection may be used.
    /// </summary>
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IIntegrationEvent @event, IDbContextTransaction transaction);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
