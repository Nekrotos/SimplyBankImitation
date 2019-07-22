using CommonLibrary.SeedOfWork;

namespace Account.Domain.DomainModels.SeedOfWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
