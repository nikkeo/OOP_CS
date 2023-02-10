using System.Collections.Immutable;
using Banks.Entities;
using Banks.Models;
using CustomExceptions;

namespace Banks.Models;

public class CentralBankRealization : ICentralBank
{
    private List<Bank> _banks;
    private List<Account> _accounts;
    private List<TransferTransaction> _transactions;

    public CentralBankRealization()
    {
        _banks = new List<Bank>();
        _accounts = new List<Account>();
        _transactions = new List<TransferTransaction>();
    }

    public Bank CreateBank(string name, List<Account> accounts, INotifiction notifiction)
    {
        Bank bank = new Bank(name, accounts, notifiction);
        _banks.Add(bank);
        _accounts.AddRange(accounts);
        return bank;
    }

    public void Transfer(Guid guidToWithdraw, Guid guidToAdd, decimal money)
    {
        Account accountToWithdrow = _accounts.Find(p => p.FindAccount(guidToWithdraw) != null) ?? throw new NotValidAccountException();
        IBankAccount bankAccountToWithdrow = accountToWithdrow.FindAccount(guidToWithdraw) ?? throw new NotValidAccountException();
        Account accountToAdd = _accounts.Find(p => p.FindAccount(guidToAdd) != null) ?? throw new NotValidAccountException();
        IBankAccount bankAccountToAdd = accountToAdd.FindAccount(guidToAdd) ?? throw new NotValidAccountException();

        _transactions.Add(new TransferTransaction(bankAccountToWithdrow, bankAccountToAdd, money));
    }

    public void CancelTransfer(Guid guidToWithdraw, Guid guidToAdd, decimal money)
    {
        Account accountToWithdrow = _accounts.Find(p => p.FindAccount(guidToWithdraw) != null) ?? throw new NotValidAccountException();
        IBankAccount bankAccountToWithdrow = accountToWithdrow.FindAccount(guidToWithdraw) ?? throw new NotValidAccountException();
        Account accountToAdd = _accounts.Find(p => p.FindAccount(guidToAdd) != null) ?? throw new NotValidAccountException();
        IBankAccount bankAccountToAdd = accountToAdd.FindAccount(guidToAdd) ?? throw new NotValidAccountException();

        TransferTransaction transaction = _transactions.Find(p =>
            p.TransactionFrom == bankAccountToWithdrow && p.TransactionTo == bankAccountToAdd &&
            p.MoneyOfTransaction() == money) ?? throw new InvalidOperationException();
        transaction.Refund();
    }

    public void UpToDateAccounts(DateTime curDateTime)
    {
        _banks.ForEach(p => p.UpToDateAccounts(curDateTime));
    }

    public void SetLimitForUnverifiedClients(Bank bank, decimal limit)
    {
        bank.SetLimitForUnverifiedClients(limit);
    }

    public void SetPercentForTransaction(Bank bank, decimal percent)
    {
        bank.SetPercentForUnverifiedClients(percent);
    }

    public void SetLimitForTransaction(Bank bank, decimal limit)
    {
        bank.SetLimitForTransaction(limit);
    }
}