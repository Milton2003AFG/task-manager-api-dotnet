using System.Net;

namespace TaskManagerApi.Exceptions;

public class UserAlreadyExistsException : CustomException
{
    public UserAlreadyExistsException(string title, string detail) : base(title, detail, HttpStatusCode.Conflict) { }
    
}