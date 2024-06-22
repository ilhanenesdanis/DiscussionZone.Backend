using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public sealed class EntryComment : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public Guid? ParentCommentId { get; set; }
        public required Guid EntryId { get; set; }
        public Entry Entry { get; set; }

    }
}
