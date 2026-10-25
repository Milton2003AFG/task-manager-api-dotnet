using System.Net;

namespace TaskManagerApi.Exceptions;

public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }
    public CustomException(string title, string detail, HttpStatusCode statusCode) : base(String.Format("{0} - {1}", title, detail))
    {
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
    }
}