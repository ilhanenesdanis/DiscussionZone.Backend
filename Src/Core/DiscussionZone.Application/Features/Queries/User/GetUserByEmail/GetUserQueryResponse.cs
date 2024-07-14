namespace DiscussionZone.Application.Features.Queries.User.GetUserByEmail
{
    public sealed class GetUserQueryResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Job { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
