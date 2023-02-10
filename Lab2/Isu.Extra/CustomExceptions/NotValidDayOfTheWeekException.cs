namespace CustomExceptions;

public class NotValidDayOfTheWeekException : Exception
{
    public NotValidDayOfTheWeekException() { }
    public NotValidDayOfTheWeekException(string message)
        : base(message) { }
    public NotValidDayOfTheWeekException(string message, Exception inner)
        : base(message, inner) { }
}