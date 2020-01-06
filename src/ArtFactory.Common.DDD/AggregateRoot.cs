using System;

namespace ArtFactory.Common.DDD
{
    /// <summary>
    /// <see cref="IAggregateRoot"> mark the implemented class as aggregate entity.
    /// 它标记了实体是AggregateRoot
    /// </summary>
    public interface IAggregateRoot { }

    public abstract class AggregateRoot<TKey> : Entity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Soft Delete
        /// </summary>
        public bool? IsDeleted { get; protected set; }
    }

    public abstract class AggregateRoot : IAggregateRoot
    {

    }
}
