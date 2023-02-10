namespace CustomExceptions;

public class NotFoundStudentException : Exception
{
    public NotFoundStudentException() { }
    public NotFoundStudentException(string message)
        : base(message) { }
    public NotFoundStudentException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotFoundGroupException : Exception
{
    public NotFoundGroupException() { }
    public NotFoundGroupException(string message)
        : base(message) { }
    public NotFoundGroupException(string message, Exception inner)
        : base(message, inner) { }
}