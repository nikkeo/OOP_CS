namespace CustomExceptions;

public class NotCorrectTeacherSurnameException : Exception
{
    public NotCorrectTeacherSurnameException() { }
    public NotCorrectTeacherSurnameException(string message)
        : base(message) { }
    public NotCorrectTeacherSurnameException(string message, Exception inner)
        : base(message, inner) { }
}