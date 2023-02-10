using CustomExceptions;

namespace Banks.Models;

public class WithdrawTransaction : ITransaction
{
    public const int LowBound = 0;
    private decimal _sumOfTransaction;
    private DateTime _timeOfTransaction;
    public WithdrawTransaction(IBankAccount withdrowFrom, decimal sumOfTransaction)
    {
        if (sumOfTransaction < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        WithdrowFrom = withdrowFrom;
        _sumOfTransaction = sumOfTransaction;
        _timeOfTransaction = DateTime.Now;
        IsRefunded = false;
        WithdrowFrom.Withdraw(sumOfTransaction, DateTime.Now, this);
    }

    public IBankAccount WithdrowFrom { get; }

    public bool IsRefunded { get; private set; }

    public decimal MoneyOfTransaction()
    {
        return _sumOfTransaction;
    }

    public DateTime TimeOfTransaction()
    {
        return _timeOfTransaction;
    }
}