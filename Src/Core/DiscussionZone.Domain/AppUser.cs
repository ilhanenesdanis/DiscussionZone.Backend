using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.Domain
{
    public sealed class AppUser : IdentityUser<string>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Job { get; set; }
        public DateTime? BirthDate { get; set; }
        public UserAvatar? UserAvatar { get; set; }
    }
}
