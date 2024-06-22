using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class EntryTag : BaseEntity
    {
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid EntryId { get; set; }
        public Entry Entry { get; set; }
    }
}
