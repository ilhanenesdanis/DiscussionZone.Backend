namespace DiscussionZone.Application.Exceptions
{

    public sealed class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base()
        { }
        public InvalidRequestException(string message) : base(message)
        {
        }
        public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
