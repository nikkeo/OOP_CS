namespace CustomExceptions;

public class NotPossiblePassword : Exception
{
    public NotPossiblePassword() { }
    public NotPossiblePassword(string message)
        : base(message) { }
    public NotPossiblePassword(string message, Exception inner)
        : base(message, inner) { }
}