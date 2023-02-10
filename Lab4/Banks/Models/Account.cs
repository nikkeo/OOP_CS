using System.Collections.Immutable;
using CustomExceptions;

namespace Banks.Models;

public class Account
{
    public const int LowBound = 0;
    public const int LowPercentBound = 0;
    public const int HighPercentBound = 100;
    private decimal _money;
    private List<IBankAccount> _bankAccounts;
    private List<string> _notifictions;
    private Client _client;

    public Account(decimal money, Client client)
    {
        if (money < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        _money = money;
        _client = client;
        _bankAccounts = new List<IBankAccount>();
        NotifictionSubscription = false;
        _notifictions = new List<string>();
    }

    public decimal VerificationLimit
    {
        get
        {
            return VerificationLimit;
        }
        set
        {
            if (value < LowBound)
                throw new NotCorrectAmountOfMoneyException();
            if (!_client.Verified)
            {
                VerificationLimit = value;
                _bankAccounts.ForEach(p => p.SetVerificationLimit(VerificationLimit));
            }
        }
    }

    public decimal TransactionPercent
    {
        get
        {
            return TransactionPercent;
        }
        set
        {
            if (value < LowPercentBound || value > HighPercentBound)
        throw new NotCorrectAmountOfMoneyException();
            TransactionPercent = value;
            _bankAccounts.ForEach(p => p.SetTransferPercent(TransactionPercent));
        }
    }

    public decimal TransactionLimit
    {
        get
        {
            return TransactionLimit;
        }
        set
        {
            if (value < LowBound)
                throw new NotCorrectAmountOfMoneyException();
            TransactionLimit = value;
            _bankAccounts.ForEach(p => p.SetTransferLimit(TransactionLimit));
        }
    }

    public bool ClientVerified { get => _client.Verified; }
    public ImmutableList<string> Notifictions { get => _notifictions.ToImmutableList(); }
    public bool NotifictionSubscription { get; }

    public string ClientName { get => _client.Name; }

    public ImmutableList<IBankAccount> BankAccounts { get => _bankAccounts.ToImmutableList(); }

    public void AddBankAccounts(List<IBankAccount> bankAccounts)
    {
        _bankAccounts.AddRange(bankAccounts);
    }

    public IBankAccount? FindAccount(Guid guidToFind)
    {
        return _bankAccounts.Find(p => p.GetId() == guidToFind);
    }

    public void NewNotifiction(string notifiction)
    {
        _notifictions.Add(notifiction);
    }
}