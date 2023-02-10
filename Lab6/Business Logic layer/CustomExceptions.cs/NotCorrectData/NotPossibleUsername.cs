namespace CustomExceptions;

public class NotPossibleUsername : Exception
{
    public NotPossibleUsername() { }
    public NotPossibleUsername(string message)
        : base(message) { }
    public NotPossibleUsername(string message, Exception inner)
        : base(message, inner) { }
}