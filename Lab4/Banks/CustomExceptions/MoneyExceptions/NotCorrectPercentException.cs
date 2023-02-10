namespace CustomExceptions;

public class NotCorrectPercentException : Exception
{
    public NotCorrectPercentException() { }
    public NotCorrectPercentException(string message)
        : base(message) { }
    public NotCorrectPercentException(string message, Exception inner)
        : base(message, inner) { }
}