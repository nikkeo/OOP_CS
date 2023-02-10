namespace CustomExceptions;

public class NotCorrectTimeException : Exception
{
    public NotCorrectTimeException() { }
    public NotCorrectTimeException(string message)
        : base(message) { }
    public NotCorrectTimeException(string message, Exception inner)
        : base(message, inner) { }
}