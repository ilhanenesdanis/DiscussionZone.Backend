using DiscussionZone.Domain.Base;

namespace DiscussionZone.Domain
{
    public class UserAvatar : BaseEntity
    {
        public required string FileUrl { get; set; }
        public required string FileName { get; set; }
        public AppUser AppUser { get; set; }
    }
}
