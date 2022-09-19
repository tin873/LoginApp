using System.Net;

namespace Core.Exceptions
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public const HttpStatusCode DefaultStatus = HttpStatusCode.BadRequest;
        public HttpStatusCode Status { get; set; }
        public UserFriendlyException(string message) : base(message)
        {
            Status = DefaultStatus;
        }
        public UserFriendlyException(string message, HttpStatusCode httpStatus) : base(message)
        {
            Status = httpStatus;
        }
        public UserFriendlyException(string message, Exception innerException) : base(message, innerException)
        {
            Status = DefaultStatus;
        }
    }
}
