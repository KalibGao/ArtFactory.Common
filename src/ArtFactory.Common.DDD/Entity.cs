namespace ArtFactory.Common.DDD
{
    public interface IEntity : IEntity<long>
    {

    }

    public interface IEntity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Unique Id in current entities
        /// </summary> 
        TKey Id { get; }
    }


    public abstract class Entity : Entity<long>
    {

    }

    public abstract class Entity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Unique Id in current entities
        /// </summary> 
        public TKey Id { get; protected set; }
    }
}