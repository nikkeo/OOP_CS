namespace CustomExceptions;

public class NotCorrectRestorePointException : Exception
{
    public NotCorrectRestorePointException() { }
    public NotCorrectRestorePointException(string message)
        : base(message) { }
    public NotCorrectRestorePointException(string message, Exception inner)
        : base(message, inner) { }
}