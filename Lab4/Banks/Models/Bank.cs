using System.Collections.Immutable;
using Banks.Entities;
using CustomExceptions;

namespace Banks.Models;

public class Bank
{
    public const int LowBound = 0;
    private List<Account> _accounts;

    public Bank(string name, List<Account> accounts, INotifiction notifiction)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NotCorrectBankNameException();
        Name = name;
        _accounts = accounts;
        Notifiction = notifiction;
    }

    public string Name { get; }
    public ImmutableList<Account> Accounts { get => _accounts.ToImmutableList(); }
    public INotifiction Notifiction { get; }

    public void AddAccounts(List<Account> accounts)
    {
        if (accounts.Count <= LowBound)
            throw new NotValidAccountException();
        _accounts.AddRange(accounts);
    }

    public void UpToDateAccounts(DateTime curDateTime)
    {
        _accounts.SelectMany(p => p.BankAccounts).ToList().ForEach(a => a.AccountUpToDate(curDateTime));
    }

    public void SetLimitForUnverifiedClients(decimal limit)
    {
        _accounts.ForEach(p => p.VerificationLimit = limit);
        _accounts.FindAll(p => p.NotifictionSubscription == true && !p.ClientVerified).ForEach(p => p.NewNotifiction(Notifiction.LimitChangeNotifiction()));
    }

    public void SetPercentForUnverifiedClients(decimal percent)
    {
        _accounts.ForEach(p => p.TransactionPercent = percent);
        _accounts.FindAll(p => p.NotifictionSubscription == true).ForEach(p => p.NewNotifiction(Notifiction.PercentChangeNotifiction()));
    }

    public void SetLimitForTransaction(decimal limit)
    {
        _accounts.ForEach(p => p.TransactionLimit = limit);
        _accounts.FindAll(p => p.NotifictionSubscription == true).ForEach(p => p.NewNotifiction(Notifiction.LimitChangeNotifiction()));
    }
}