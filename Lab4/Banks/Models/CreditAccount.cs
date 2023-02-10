using System.Collections.Immutable;
using CustomExceptions;

namespace Banks.Models;

public class CreditAccount : IBankAccount
{
    public const int LowBound = 0;
    public const int NotSetParam = 0;
    public const decimal LowPercentBound = 0;
    public const decimal HighPercentBound = 100;
    public const int NumberOfMonthsPerYear = 12;
    private Guid _id;
    private decimal _money;
    private decimal _percentOfCommisionUnderZero;
    private decimal _verificationLimit;
    private decimal _transactionPercent;
    private decimal _transactionLimit;
    private List<ITransaction> _transactions;

    public CreditAccount(decimal money, decimal lowLimit, decimal percentOfCommisionUnderZero, Guid id, decimal verificationLimit = 0)
    {
        if (money < lowLimit)
            throw new NotCorrectAmountOfMoneyException();
        if (percentOfCommisionUnderZero < LowPercentBound || percentOfCommisionUnderZero > HighPercentBound)
            throw new NotCorrectPercentException();
        _money = money;
        _percentOfCommisionUnderZero = percentOfCommisionUnderZero;
        _id = id;
        _verificationLimit = verificationLimit;
        _transactionPercent = NotSetParam;
        _transactionLimit = NotSetParam;
        _transactions = new List<ITransaction>();
    }

    public decimal Money { get => _money; }
    public decimal Percent { get => _percentOfCommisionUnderZero; }
    public Guid Id { get => _id; }
    public ImmutableList<ITransaction> Transactions { get => _transactions.ToImmutableList(); }

    public void Add(decimal money, AddTransaction addTransaction)
    {
        if (money < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        _money += money;
        _transactions.Add(addTransaction);
    }

    public void Transfer(decimal money, DateTime curDateTime, TransferTransaction transaction)
    {
        if (money + _money < LowBound)
            throw new NotEnoughtMoneyException();
        if (_verificationLimit != NotSetParam && _verificationLimit < money)
            throw new MoneyLimitForUnverifiedClientException();
        if (_transactionLimit != NotSetParam && _transactionLimit >= money)
            throw new MoneyLimitForTransferExceptionc();
        _money += money - (money * _transactionPercent);
        if (!_transactions.Contains(transaction))
            _transactions.Add(transaction);
    }

    public void Withdraw(decimal money, DateTime curDateTime, WithdrawTransaction withdrawTransaction)
    {
        if (money < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        if (money > _money)
            throw new NotEnoughtMoneyException();
        if (_verificationLimit != NotSetParam && _verificationLimit < money)
            throw new MoneyLimitForUnverifiedClientException();
        _money -= money;
        _transactions.Add(withdrawTransaction);
    }

    public decimal MoneyInTheFuture(DateTime futureDateTime)
    {
        if (_money >= LowBound)
            return _money;
        if (futureDateTime < DateTime.Now)
            throw new NotCorrectTimeException();
        int numberOfMonths = (DateTime.Now.Year * NumberOfMonthsPerYear) + DateTime.Now.Month -
            (futureDateTime.Year * NumberOfMonthsPerYear) + futureDateTime.Month;
        decimal currentMoney = _money;
        List<ITransaction> transactions = _transactions;
        while (numberOfMonths > LowBound)
        {
            if (transactions.Count > LowBound)
            {
                if (DateTime.Now.Month + NumberOfMonthsPerYear <= transactions[0].TimeOfTransaction().Month)
                {
                    currentMoney += transactions[LowBound].MoneyOfTransaction();
                    transactions.Remove(transactions[LowBound]);
                }
            }

            currentMoney /= Percent;
            numberOfMonths--;
        }

        return currentMoney;
    }

    public void AccountUpToDate(DateTime futureDateTime)
    {
        _money = MoneyInTheFuture(futureDateTime);
    }

    public void SetVerificationLimit(decimal limit)
    {
        if (limit < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        _verificationLimit = limit;
    }

    public void SetTransferPercent(decimal percent)
    {
        if (percent < LowBound)
            throw new NotCorrectPercentException();
        _transactionPercent = percent;
    }

    public void SetTransferLimit(decimal limit)
    {
        if (limit < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        _transactionLimit = limit;
    }

    public Guid GetId() => _id;
}