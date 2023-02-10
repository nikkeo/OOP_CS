namespace CustomExceptions;

public class NotCorrectShopAdressException : Exception
{
    public NotCorrectShopAdressException() { }
    public NotCorrectShopAdressException(string message)
        : base(message) { }
    public NotCorrectShopAdressException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotCorrectShopNameException : Exception
{
    public NotCorrectShopNameException() { }
    public NotCorrectShopNameException(string message)
        : base(message) { }
    public NotCorrectShopNameException(string message, Exception inner)
        : base(message, inner) { }
}