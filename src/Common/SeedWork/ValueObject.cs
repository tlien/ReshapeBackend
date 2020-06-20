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
        /// Get ValueObject values as an IEnumerable
        /// </summary>
        protected abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// ValueObject Equals method override that allows for direct comparison between all ValueObject properties.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <summary>
        /// Returns a HashCode based on the values of the ValueObject properties
        /// </summary>
        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// == operator that implements the ValueObject.Equals override
        /// </summary>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
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
