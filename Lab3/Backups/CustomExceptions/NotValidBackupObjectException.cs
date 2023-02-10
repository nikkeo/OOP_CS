namespace CustomExceptions;

public class NotValidBackupObjectException : Exception
{
    public NotValidBackupObjectException() { }
    public NotValidBackupObjectException(string message)
        : base(message) { }
    public NotValidBackupObjectException(string message, Exception inner)
        : base(message, inner) { }
}