namespace CustomExceptions;

public class NotCorrectAmountOfMoneyException : Exception
{
    public NotCorrectAmountOfMoneyException() { }
    public NotCorrectAmountOfMoneyException(string message)
        : base(message) { }
    public NotCorrectAmountOfMoneyException(string message, Exception inner)
        : base(message, inner) { }
}