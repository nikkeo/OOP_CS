namespace CustomExceptions;

public class NotCorrectBankNameException : Exception
{
    public NotCorrectBankNameException() { }
    public NotCorrectBankNameException(string message)
        : base(message) { }
    public NotCorrectBankNameException(string message, Exception inner)
        : base(message, inner) { }
}