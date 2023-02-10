namespace CustomExceptions;

public class PathIsNotCorrectException : Exception
{
    public PathIsNotCorrectException() { }
    public PathIsNotCorrectException(string message)
        : base(message) { }
    public PathIsNotCorrectException(string message, Exception inner)
        : base(message, inner) { }
}