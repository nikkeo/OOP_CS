namespace CustomExceptions;

public class MissedException : Exception
{
    public MissedException() { }
    public MissedException(string message)
        : base(message) { }
    public MissedException(string message, Exception inner)
        : base(message, inner) { }
}