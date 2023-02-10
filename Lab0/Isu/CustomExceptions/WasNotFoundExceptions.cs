namespace CustomExceptions;

public class StudentWasNotFoundException : Exception
{
    public StudentWasNotFoundException() { }
    public StudentWasNotFoundException(string message)
        : base(message) { }
    public StudentWasNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}