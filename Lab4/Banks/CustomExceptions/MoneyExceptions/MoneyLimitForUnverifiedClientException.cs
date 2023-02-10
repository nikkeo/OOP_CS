namespace CustomExceptions;

public class MoneyLimitForUnverifiedClientException : Exception
{
    public MoneyLimitForUnverifiedClientException() { }
    public MoneyLimitForUnverifiedClientException(string message)
        : base(message) { }
    public MoneyLimitForUnverifiedClientException(string message, Exception inner)
        : base(message, inner) { }
}