namespace CustomExceptions;

public class NotEnoughtMoneyException : Exception
{
    public NotEnoughtMoneyException() { }
    public NotEnoughtMoneyException(string message)
        : base(message) { }
    public NotEnoughtMoneyException(string message, Exception inner)
        : base(message, inner) { }
}