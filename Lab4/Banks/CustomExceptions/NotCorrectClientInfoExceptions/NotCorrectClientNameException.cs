namespace CustomExceptions;

public class NotCorrectClientNameException : Exception
{
    public NotCorrectClientNameException() { }
    public NotCorrectClientNameException(string message)
        : base(message) { }
    public NotCorrectClientNameException(string message, Exception inner)
        : base(message, inner) { }
}