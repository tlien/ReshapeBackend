using System.Collections.Generic;
using System.Linq;

namespace Reshape.Common.SeedWork
{
    /// <summary>
    /// A class that domain value objects inherit from.
    /// Value objects are immutable, have no specific identifier, and must be owned by either a domain entity or aggregate.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Get ValueObject properties as an IEnumerable
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// ValueObject Equals method override that allows for direct comparison between all ValueObject properties.
        /// Returns false if either object has more values than the other.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is ValueObject))
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Returns a HashCode based on the values of the ValueObject properties
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// == operator that implements the ValueObject.Equals override
        /// </summary>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left is null || left.Equals(right);
        }

        /// <summary>
        /// != operator that utilises the ValueObject.Equals override implementation of the == operator
        /// </summary>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
    }
}
