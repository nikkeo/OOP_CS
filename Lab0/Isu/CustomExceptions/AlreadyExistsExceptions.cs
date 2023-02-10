namespace CustomExceptions;

public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException() { }
    public GroupAlreadyExistsException(string message)
        : base(message) { }
    public GroupAlreadyExistsException(string message, Exception inner)
        : base(message, inner) { }
}

public class StudentAlreadyInGroupException : Exception
{
    public StudentAlreadyInGroupException() { }
    public StudentAlreadyInGroupException(string message)
        : base(message) { }
    public StudentAlreadyInGroupException(string message, Exception inner)
        : base(message, inner) { }
}