namespace CustomExceptions;

public class NotCorrectFacultyDirectionException : Exception
{
    public NotCorrectFacultyDirectionException() { }
    public NotCorrectFacultyDirectionException(string message)
        : base(message) { }
    public NotCorrectFacultyDirectionException(string message, Exception inner)
        : base(message, inner) { }
}