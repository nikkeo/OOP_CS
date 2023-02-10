namespace CustomExceptions;

public class NotCorrectBuyerException : Exception
{
    public NotCorrectBuyerException() { }
    public NotCorrectBuyerException(string message)
        : base(message) { }
    public NotCorrectBuyerException(string message, Exception inner)
        : base(message, inner) { }
}