using Banks.Entities;
using Banks.Models;
using Xunit;

namespace Banks.Test;

public class IsuServiceTest
{
    [Fact]
    public void BankHasAccountWithAllAccounts_ThrowException()
    {
        Client cl = new Client("Jon", "Wick");
        CentralBankRealization centralBankRealization = new CentralBankRealization();
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        Guid guid3 = Guid.NewGuid();
        CreditAccount creditAccount = new CreditAccount(100, -100, 1, guid1);
        DebitAccount debitAccount = new DebitAccount(100, 1, guid2);
        DepositPercent depositPercent = new DepositPercent(1, 1000, 1);
        List<DepositPercent> depositPercents = new List<DepositPercent>();
        depositPercents.Add(depositPercent);
        DepositAccount depositAccount = new DepositAccount(100, DateTime.MaxValue, depositPercents, guid3);

        Account account = new Account(1000, cl);
        List<IBankAccount> bankAccounts = new List<IBankAccount>();
        bankAccounts.Add(creditAccount);
        bankAccounts.Add(debitAccount);
        bankAccounts.Add(depositAccount);
        account.AddBankAccounts(bankAccounts);
        List<Account> accounts = new List<Account>();
        accounts.Add(account);

        Bank bank = centralBankRealization.CreateBank("bank", accounts, new SimpleTextNotifiction());

        Assert.True(
            bank.Accounts.Contains(account)
                    && bank.Accounts[0].BankAccounts.Contains(debitAccount)
                    && bank.Accounts[0].BankAccounts.Contains(creditAccount)
                    && bank.Accounts[0].BankAccounts.Contains(depositAccount),
            "Not correct accounts");
    }

    [Fact]
    public void TransferSuccedAndCancelSucessfulAsWell_ThrowException()
    {
        Client client1 = new Client("Jon", "Wick");
        Client client2 = new Client("Josh", "Notwick");
        CentralBankRealization centralBankRealization = new CentralBankRealization();
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        CreditAccount creditAccount = new CreditAccount(100, -100, 1, guid1);
        DebitAccount debitAccount = new DebitAccount(100, 1, guid2);

        Account account1 = new Account(1000, client1);
        Account account2 = new Account(1000, client2);
        List<IBankAccount> bankAccounts1 = new List<IBankAccount>();
        List<IBankAccount> bankAccounts2 = new List<IBankAccount>();
        bankAccounts1.Add(creditAccount);
        bankAccounts2.Add(debitAccount);
        account1.AddBankAccounts(bankAccounts1);
        account2.AddBankAccounts(bankAccounts2);
        List<Account> accounts = new List<Account>();
        accounts.Add(account1);
        accounts.Add(account2);

        Bank bank = centralBankRealization.CreateBank("bank", accounts, new SimpleTextNotifiction());
        Assert.True(creditAccount.Money == 100 && debitAccount.Money == 100, "Money amount is not correct");

        centralBankRealization.Transfer(guid1, guid2, 100);
        Assert.True(creditAccount.Money == 0 && debitAccount.Money == 200, "Money is not transfered");

        centralBankRealization.CancelTransfer(guid1, guid2, 100);
        Assert.True(creditAccount.Money == 100 && debitAccount.Money == 100, "Transfer was not cancelled");
    }
}