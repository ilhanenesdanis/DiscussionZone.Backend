using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class Tag : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<EntryTag> EntryTags { get; set; }
    }
}
