namespace CustomExceptions;

public class NotCorrectStorageAlgorithmException : Exception
{
    public NotCorrectStorageAlgorithmException() { }
    public NotCorrectStorageAlgorithmException(string message)
        : base(message) { }
    public NotCorrectStorageAlgorithmException(string message, Exception inner)
        : base(message, inner) { }
}