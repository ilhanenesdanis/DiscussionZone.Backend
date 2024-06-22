namespace DiscussionZone.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
