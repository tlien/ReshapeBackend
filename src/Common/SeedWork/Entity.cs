using System;
using System.Collections.Generic;
using MediatR;

namespace Reshape.Common.SeedWork
{
    /// <summary>
    /// A base class for domain entities and domain aggregates.
    /// Entities have a unique identifier, are mutable and should adhere to one of the following conventions:
    ///     1. The entity does not implement IAggregateRoot and should therefore be owned by one or more domain aggregates.
    ///     2. The entity implements IAggregateRoot and is therefore a domain aggregate.
    /// </summary>
    public abstract class Entity
    {
        int? _requestedHashCode;
        Guid _Id;
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// The Entity is transient if it has not yet been given an ID by the Entity Framework
        /// </summary>
        public bool IsTransient()
        {
            return Id == default;
        }

        /// <summary>
        /// Entity Equals method override that also:
        ///     1. Checks if the entities are transient
        ///     2. Checks if the entity IDs are the same
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        /// <summary>
        /// Returns the Entity Hashcode, which is based on the Entity Id.
        /// If the Entity is transient, the base.GetHashCode() returned.
        /// On first run, the HashCode is stored in a private variable for future efficiency.
        /// </summary>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode();

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        /// <summary>
        /// == operator that implements the Entity.Equals override
        /// </summary>
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        /// <summary>
        /// != operator that utilises the Entity.Equals override implementation of the == operator
        /// </summary>
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}