namespace CustomExceptions;

public class DepositTimeIsNotCameYetExceptioncs : Exception
{
    public DepositTimeIsNotCameYetExceptioncs() { }
    public DepositTimeIsNotCameYetExceptioncs(string message)
        : base(message) { }
    public DepositTimeIsNotCameYetExceptioncs(string message, Exception inner)
        : base(message, inner) { }
}