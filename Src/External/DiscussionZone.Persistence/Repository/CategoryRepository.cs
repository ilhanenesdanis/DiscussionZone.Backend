using DiscussionZone.Application.Repository;
using DiscussionZone.Application.UnitOfWork;
using DiscussionZone.Domain;
using DiscussionZone.Persistence.Context;
using DiscussionZone.Persistence.Repository.GenericRepository;

namespace DiscussionZone.Persistence.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
