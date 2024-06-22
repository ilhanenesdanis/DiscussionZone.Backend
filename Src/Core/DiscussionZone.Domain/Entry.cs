using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class Entry : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public bool IsSolved { get; set; }
        public bool IsPinned { get; set; }
        public Guid EntryStatusId { get; set; }
        public EntryStatus EntryStatus { get; set; }
        public ICollection<EntryCategory>? EntryCategories { get; set; }
        public ICollection<EntryComment>? EntryComments { get; set; }
        public ICollection<EntryTag> EntryTags { get; set; }
        public ICollection<EntryVote> EntryVotes { get; set; }
    }
}
