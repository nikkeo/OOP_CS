namespace CustomExceptions;

public class NotCorrectClassNumberException : Exception
{
    public NotCorrectClassNumberException() { }
    public NotCorrectClassNumberException(string message)
        : base(message) { }
    public NotCorrectClassNumberException(string message, Exception inner)
        : base(message, inner) { }
}