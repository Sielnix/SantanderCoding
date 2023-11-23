namespace Santander.Coding.BestStories;

public class HackerNewsResponseException : Exception
{
    public HackerNewsResponseException()
    {
    }

    public HackerNewsResponseException(string? message) : base(message)
    {
    }

    public HackerNewsResponseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
