using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class EntryVote : BaseEntity
    {
        public Guid EntryId { get; set; }
        public Entry Entry { get; set; }
    }
}
