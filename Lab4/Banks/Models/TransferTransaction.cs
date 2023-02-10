using CustomExceptions;

namespace Banks.Models;

public class TransferTransaction : ITransaction
{
    public const int LowBound = 0;
    private decimal _sumOfTransaction;
    private DateTime _timeOfTransaction;
    public TransferTransaction(IBankAccount transactionFrom, IBankAccount transactionTo, decimal sumOfTransaction)
    {
        if (sumOfTransaction < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        TransactionFrom = transactionFrom;
        TransactionTo = transactionTo;
        _sumOfTransaction = sumOfTransaction;
        _timeOfTransaction = DateTime.Now;
        IsRefunded = false;

        transactionFrom.Transfer(sumOfTransaction * -1, DateTime.Now, this);
        transactionTo.Transfer(sumOfTransaction, DateTime.Now, this);
    }

    public IBankAccount TransactionTo { get; }
    public IBankAccount TransactionFrom { get; }

    public bool IsRefunded { get; private set; }

    public void Refund()
    {
        TransactionFrom.Transfer(_sumOfTransaction, DateTime.Now, this);
        TransactionTo.Transfer(_sumOfTransaction * -1, DateTime.Now, this);
        IsRefunded = true;
    }

    public decimal MoneyOfTransaction()
    {
        return _sumOfTransaction;
    }

    public DateTime TimeOfTransaction()
    {
        return _timeOfTransaction;
    }
}