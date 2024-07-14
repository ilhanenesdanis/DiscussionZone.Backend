namespace DiscussionZone.Application.Exceptions
{
    public sealed class InvalidRequestParameterException : Exception
    {
        public InvalidRequestParameterException() : base()
        {

        }
        public InvalidRequestParameterException(string message) : base(message) { }
        public InvalidRequestParameterException(string message, Exception innerException) : base(message, innerException) { }

    }
}
