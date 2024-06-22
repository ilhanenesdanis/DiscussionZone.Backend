using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public class EntryStatus : BaseEntity
    {
        public required string Name { get; set; }
    }
}
