using DiscussionZone.Application.Repository.GenericRepository;
using DiscussionZone.Application.UnitOfWork;
using DiscussionZone.Domain.Base;
using DiscussionZone.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DiscussionZone.Persistence.Repository.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        protected DbSet<T> entity => _context.Set<T>();
        public Repository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork;
        }

        public int Add(T entity)
        {
            this.entity.Add(entity);
            return _unitOfWork.Commit();
        }

        public int Add(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            entity.AddRange(entities);
            return _unitOfWork.Commit();
        }

        public async Task<int> AddAsync(T entity)
        {
            await this.entity.AddAsync(entity);
            return await _unitOfWork.CommitAsync();

        }

        public async Task<int> AddAsync(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return await Task.FromResult(0);

            entity.AddRange(entities);
            return await _unitOfWork.CommitAsync();
        }

        public int AddOrUpdate(T entity)
        {
            if (this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);

            return _unitOfWork.Commit();
        }

        public async Task<int> AddOrUpdateAsync(T entity)
        {
            if (this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);

            return await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return entity.AsQueryable();
        }

        public async Task BulkAdd(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await entity.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task BulkDelete(Expression<Func<T, bool>> predicate)
        {
            _context.RemoveRange(entity.Where(predicate));
            await _unitOfWork.CommitAsync();
        }

        public async Task BulkDelete(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && ids.Any())
                return;

            _context.RemoveRange(entity.Where(x => ids.Contains(x.Id)));
            await _unitOfWork.CommitAsync();
        }

        public async Task BulkUpdate(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return;

            foreach (var entityItem in entities)
            {
                entity.Update(entityItem);
            }

            await _unitOfWork.CommitAsync();
        }

        public int Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return _unitOfWork.Commit();
        }

        public int Delete(Guid id)
        {
            var entity = this.entity.Find(id);
            return Delete(entity);
        }

        public Task<int> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await this.entity.FindAsync(id);
            return await DeleteAsync(result);
        }

        public bool DeleteRange(Expression<Func<T, bool>> predicate)
        {
            _context.RemoveRange(entity.Where(predicate));
            return _unitOfWork.Commit() > 0;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
        {
            _context.RemoveRange(entity.Where(predicate));
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Get(predicate, noTracking, includes).FirstOrDefaultAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);


            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;

        }

        public async Task<List<T>> GetAll(bool noTracking = true)
        {
            if (noTracking)
                return await entity.AsNoTracking().ToListAsync();

            return await entity.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            T found = await entity.FindAsync(id);

            if (found == null)
                return null;

            if (noTracking)
                _context.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<T, object>> include in includes)
                await _context.Entry(found).Reference(include).LoadAsync();

            return found;

        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> predicate, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entity;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public int Update(T entity)
        {
            this.entity.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            return _unitOfWork.Commit();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            this.entity.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            return await _unitOfWork.CommitAsync();
        }
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
                foreach (var includeItem in includes)
                    query = query.Include(includeItem);

            return query;

        }
    }
}
