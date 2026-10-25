using System.Net;

namespace TaskManagerApi.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException(string title, string details) : base(title, details, HttpStatusCode.Unauthorized) { }
}