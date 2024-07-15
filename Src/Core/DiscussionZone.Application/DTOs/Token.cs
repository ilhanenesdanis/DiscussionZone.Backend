namespace DiscussionZone.Application.DTOs
{
    public sealed class Token
    {
        public string? AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; }
    }

}
