using DiscussionZone.Application.UnitOfWork;
using DiscussionZone.Persistence.Context;

namespace DiscussionZone.Persistence.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                int result = _context.SaveChanges();
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
