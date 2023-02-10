namespace CustomExceptions;

public class NotCorrectBuyerNameException : Exception
{
    public NotCorrectBuyerNameException() { }
    public NotCorrectBuyerNameException(string message)
        : base(message) { }
    public NotCorrectBuyerNameException(string message, Exception inner)
        : base(message, inner) { }
}