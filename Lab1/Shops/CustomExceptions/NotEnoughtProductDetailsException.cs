namespace CustomExceptions;

public class NotEnoughtMoneyException : Exception
{
    public NotEnoughtMoneyException() { }
    public NotEnoughtMoneyException(string message)
        : base(message) { }
    public NotEnoughtMoneyException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotEnoughtQuantityException : Exception
{
    public NotEnoughtQuantityException() { }
    public NotEnoughtQuantityException(string message)
        : base(message) { }
    public NotEnoughtQuantityException(string message, Exception inner)
        : base(message, inner) { }
}