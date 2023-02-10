namespace CustomExceptions;

public class NotCorrectFilePathException : Exception
{
    public NotCorrectFilePathException() { }
    public NotCorrectFilePathException(string message)
        : base(message) { }
    public NotCorrectFilePathException(string message, Exception inner)
        : base(message, inner) { }
}