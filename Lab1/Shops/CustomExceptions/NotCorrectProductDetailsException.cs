namespace CustomExceptions;

public class NotCorrectMoneyAmountException : Exception
{
    public NotCorrectMoneyAmountException() { }
    public NotCorrectMoneyAmountException(string message)
        : base(message) { }
    public NotCorrectMoneyAmountException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotCorrectQuantityException : Exception
{
    public NotCorrectQuantityException() { }
    public NotCorrectQuantityException(string message)
        : base(message) { }
    public NotCorrectQuantityException(string message, Exception inner)
        : base(message, inner) { }
}