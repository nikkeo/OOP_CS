namespace CustomExceptions;

public class NotPossibleSourceName : Exception
{
    public NotPossibleSourceName() { }
    public NotPossibleSourceName(string message)
        : base(message) { }
    public NotPossibleSourceName(string message, Exception inner)
        : base(message, inner) { }
}