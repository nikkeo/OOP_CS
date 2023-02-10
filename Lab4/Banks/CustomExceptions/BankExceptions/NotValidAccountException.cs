namespace CustomExceptions;

public class NotValidAccountException : Exception
{
    public NotValidAccountException() { }
    public NotValidAccountException(string message)
        : base(message) { }
    public NotValidAccountException(string message, Exception inner)
        : base(message, inner) { }
}