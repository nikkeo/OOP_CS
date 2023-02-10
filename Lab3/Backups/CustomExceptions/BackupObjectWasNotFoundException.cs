namespace CustomExceptions;

public class BackupObjectWasNotFoundException : Exception
{
    public BackupObjectWasNotFoundException() { }
    public BackupObjectWasNotFoundException(string message)
        : base(message) { }
    public BackupObjectWasNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}