namespace CustomExceptions;

public class NotCorrectAddressException : Exception
{
    public NotCorrectAddressException() { }
    public NotCorrectAddressException(string message)
        : base(message) { }
    public NotCorrectAddressException(string message, Exception inner)
        : base(message, inner) { }
}