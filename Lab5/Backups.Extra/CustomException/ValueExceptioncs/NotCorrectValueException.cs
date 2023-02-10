namespace CustomExceptions;

public class NotCorrectValueException : Exception
{
    public NotCorrectValueException() { }
    public NotCorrectValueException(string message)
        : base(message) { }
    public NotCorrectValueException(string message, Exception inner)
        : base(message, inner) { }
}