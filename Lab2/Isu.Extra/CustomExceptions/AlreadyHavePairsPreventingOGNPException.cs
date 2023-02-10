namespace CustomExceptions;

public class AlreadyHavePairsPreventingOGNPException : Exception
{
    public AlreadyHavePairsPreventingOGNPException() { }
    public AlreadyHavePairsPreventingOGNPException(string message)
        : base(message) { }
    public AlreadyHavePairsPreventingOGNPException(string message, Exception inner)
        : base(message, inner) { }
}