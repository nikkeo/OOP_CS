namespace CustomExceptions;

public class GroupIsOverloadedException : Exception
{
    public GroupIsOverloadedException() { }
    public GroupIsOverloadedException(string message)
        : base(message) { }
    public GroupIsOverloadedException(string message, Exception inner)
        : base(message, inner) { }
}