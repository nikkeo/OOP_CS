namespace CustomExceptions;

public class NotCorrectOGNPException : Exception
{
    public NotCorrectOGNPException() { }
    public NotCorrectOGNPException(string message)
        : base(message) { }
    public NotCorrectOGNPException(string message, Exception inner)
        : base(message, inner) { }
}