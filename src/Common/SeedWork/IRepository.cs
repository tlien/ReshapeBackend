namespace Reshape.Common.SeedWork
{
    /// <summary>
    /// A contract that states that a repository must belong to a domain aggregate
    /// </summary>
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}