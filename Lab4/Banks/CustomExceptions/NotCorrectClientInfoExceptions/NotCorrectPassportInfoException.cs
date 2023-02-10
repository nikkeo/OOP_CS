namespace CustomExceptions;

public class NotCorrectPassportInfoException : Exception
{
    public NotCorrectPassportInfoException() { }
    public NotCorrectPassportInfoException(string message)
        : base(message) { }
    public NotCorrectPassportInfoException(string message, Exception inner)
        : base(message, inner) { }
}