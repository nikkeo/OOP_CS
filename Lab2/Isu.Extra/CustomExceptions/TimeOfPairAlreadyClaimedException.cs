namespace CustomExceptions;

public class TimeOfPairAlreadyClaimedException : Exception
{
    public TimeOfPairAlreadyClaimedException() { }
    public TimeOfPairAlreadyClaimedException(string message)
        : base(message) { }
    public TimeOfPairAlreadyClaimedException(string message, Exception inner)
        : base(message, inner) { }
}