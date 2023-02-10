namespace CustomExceptions;

public class NotCorrectFormOfEducationException : Exception
{
    public NotCorrectFormOfEducationException() { }
    public NotCorrectFormOfEducationException(string message)
        : base(message) { }
    public NotCorrectFormOfEducationException(string message, Exception inner)
        : base(message, inner) { }
}