namespace CustomExceptions;

public class NotCorrectClientSurnameException : Exception
{
    public NotCorrectClientSurnameException() { }
    public NotCorrectClientSurnameException(string message)
        : base(message) { }
    public NotCorrectClientSurnameException(string message, Exception inner)
        : base(message, inner) { }
}