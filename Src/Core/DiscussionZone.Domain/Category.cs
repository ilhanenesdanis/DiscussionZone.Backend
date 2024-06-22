using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class Category : BaseEntity
    {
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }

        public ICollection<EntryCategory> EntryCategories { get; set; }

    }
}
