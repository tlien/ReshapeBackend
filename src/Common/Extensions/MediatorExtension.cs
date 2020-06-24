using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;

using Reshape.Common.SeedWork;

namespace Reshape.Common.Extensions
{
    public static class MediatorExtension
    {
        /// <summary>
        /// Dispatch all pending domain events on the entities tracked in the current transaction.
        /// This is supposed to happen before a transaction is committed to ensure all side effects have occured and are ready to be persisted.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <typeparam name="TDbContext">The DbContext from which to get domain events.</typeparam>
        /// <returns></returns>
        public static async Task DispatchDomainEventsAsync<TDbContext>(this IMediator mediator, TDbContext ctx) where TDbContext : DbContext
        {
            // Get all Domain Entities (Entities and Aggregates) that currently have domain events enqueued.
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // Get all domain events from Domain Entities from previous step.
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Before publishing, all events are removed from containing Domain Entity.
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            // Dispatch Domain Events to their respective handlers.
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}