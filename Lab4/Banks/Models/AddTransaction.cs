using CustomExceptions;

namespace Banks.Models;

public class AddTransaction : ITransaction
{
    public const int LowBound = 0;
    private decimal _sumOfTransaction;
    private DateTime _timeOfTransaction;
    public AddTransaction(IBankAccount addTo, decimal sumOfTransaction)
    {
        if (sumOfTransaction < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        AddTo = addTo;
        _sumOfTransaction = sumOfTransaction;
        _timeOfTransaction = DateTime.Now;
        addTo.Add(_sumOfTransaction, this);
    }

    public IBankAccount AddTo { get; }

    public decimal MoneyOfTransaction()
    {
        return _sumOfTransaction;
    }

    public DateTime TimeOfTransaction()
    {
        return _timeOfTransaction;
    }
}