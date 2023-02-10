namespace CustomExceptions;

public class StudentCantChooseOGNPFromHisFacultyException : Exception
{
    public StudentCantChooseOGNPFromHisFacultyException() { }
    public StudentCantChooseOGNPFromHisFacultyException(string message)
        : base(message) { }
    public StudentCantChooseOGNPFromHisFacultyException(string message, Exception inner)
        : base(message, inner) { }
}