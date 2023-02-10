namespace CustomExceptions;

public class NotCorrectProductException : Exception
{
    public NotCorrectProductException() { }
    public NotCorrectProductException(string message)
        : base(message) { }
    public NotCorrectProductException(string message, Exception inner)
        : base(message, inner) { }
}