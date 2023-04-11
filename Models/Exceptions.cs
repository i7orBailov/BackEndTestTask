using BackEndTestTask.Models.Enums;

namespace BackEndTestTask.Models
{
    public class SecureException : Exception
    {
        public virtual ExceptionType ExceptionType { get; set; } = ExceptionType.Secure;

        public SecureException(string? message) : base(message)
        {
        }
    }
}