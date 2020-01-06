using System;

namespace ArtFactory.Common.DDD
{
    public abstract class ValueObject
    {
        public static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public static bool NotEqualOperator(ValueObject left, ValueObject right) => !EqualOperator(left, right);

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            ValueObject other = (ValueObject)obj;

            IEnumerator<object> thisValues = this.GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                    return false;

                if (ReferenceEquals(thisValues.Current, null) && !thisValues.Current.Equals(otherValues.Current))
                    return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext(); 
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((curr, next) => curr ^ next);
        }
    }
}