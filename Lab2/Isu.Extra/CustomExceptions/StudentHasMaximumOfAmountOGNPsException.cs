namespace CustomExceptions;

public class StudentHasMaximumOfAmountOGNPsException : Exception
{
    public StudentHasMaximumOfAmountOGNPsException() { }
    public StudentHasMaximumOfAmountOGNPsException(string message)
        : base(message) { }
    public StudentHasMaximumOfAmountOGNPsException(string message, Exception inner)
        : base(message, inner) { }
}