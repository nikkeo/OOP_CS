namespace CustomExceptions;

public class MoneyLimitForTransferExceptionc : Exception
{
    public MoneyLimitForTransferExceptionc() { }
    public MoneyLimitForTransferExceptionc(string message)
        : base(message) { }
    public MoneyLimitForTransferExceptionc(string message, Exception inner)
        : base(message, inner) { }
}