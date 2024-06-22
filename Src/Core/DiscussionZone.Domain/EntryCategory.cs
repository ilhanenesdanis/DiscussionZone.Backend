using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public class EntryCategory : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid EntryId { get; set; }
        public Entry Entry { get; set; }
    }
}
