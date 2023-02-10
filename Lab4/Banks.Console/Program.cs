using Banks.Entities;
using Banks.Models;
using CustomExceptions;

Console.WriteLine("Write EXIT to leave or HELP to get functions");
string? reader = Console.ReadLine();
List<Client> clients = new List<Client>();
List<Bank> banks = new List<Bank>();
List<IBankAccount> bankAccounts = new List<IBankAccount>();
List<Account> accounts = new List<Account>();
CentralBankRealization centralBankRealization = new CentralBankRealization();

while (reader != "EXIT")
{
    if (reader == "HELP")
    {
        Console.WriteLine("Create Client");
        Console.WriteLine("Create Bank Account");
        Console.WriteLine("Create account");
    }

    if (reader == "Create Client")
    {
        Console.WriteLine("Enter Name");
        string name = Console.ReadLine() ?? throw new NotCorrectClientNameException();
        Console.WriteLine("Enter Surname");
        string surname = Console.ReadLine() ?? throw new NotCorrectClientSurnameException();
        Console.WriteLine("Enter address or ''");
        string address = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter passport Info or ''");
        string passportInfo = Console.ReadLine() ?? throw new InvalidOperationException();
        Client client = new Client(name, surname, address, passportInfo);
        clients.Add(client);
    }

    if (reader == "Create Bank Account")
    {
        Console.WriteLine("Enter Type of account : debit, deposit, credit");
        string type = Console.ReadLine() ?? throw new InvalidOperationException();
        if (type == "debit")
        {
            Console.WriteLine("Enter money");
            decimal money = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            Console.WriteLine("Enter percent");
            decimal percent = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            DebitAccount debitAccount = new DebitAccount(money, percent, Guid.NewGuid());
            bankAccounts.Add(debitAccount);
        }

        if (type == "credit")
        {
            Console.WriteLine("Enter money");
            decimal money = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            Console.WriteLine("Enter minimal limit");
            decimal lowLimit = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            Console.WriteLine("Enter percent");
            decimal percent = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            CreditAccount debitAccount = new CreditAccount(money, percent, lowLimit, Guid.NewGuid());
            bankAccounts.Add(debitAccount);
        }

        if (type == "deposit")
        {
            Console.WriteLine("Enter money");
            decimal money = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            Console.WriteLine("Enter dateTime");
            DateTime dateTime = Convert.ToDateTime(Console.ReadLine() ?? throw new NotCorrectClientNameException());
            string input = " ";
            decimal lowLimit;
            decimal highlimit;
            decimal percent;
            List<DepositPercent> depositPercents = new List<DepositPercent>();
            while (input != "end")
            {
                Console.WriteLine("Enter LowLimit || end");
                input = Console.ReadLine() ?? throw new InvalidOperationException();
                if (input == "end")
                {
                    break;
                }

                lowLimit = Convert.ToDecimal(input);
                Console.WriteLine("Enter HighLimit");
                input = Console.ReadLine() ?? throw new InvalidOperationException();
                if (input == "end")
                {
                    break;
                }

                highlimit = Convert.ToDecimal(input);
                Console.WriteLine("Enter percent");
                input = Console.ReadLine() ?? throw new InvalidOperationException();
                if (input == "end")
                {
                    break;
                }

                percent = Convert.ToDecimal(input);

                depositPercents.Add(new DepositPercent(lowLimit, highlimit, percent));
            }

            DepositAccount depositAccount = new DepositAccount(money, dateTime, depositPercents, Guid.NewGuid());
            bankAccounts.Add(depositAccount);
        }
    }

    if (reader == "Create account")
    {
        Console.WriteLine("Enter money");
        decimal money = Convert.ToDecimal(Console.ReadLine() ?? throw new NotCorrectClientNameException());
        Console.WriteLine("Enter clientName");
        string clientName = Console.ReadLine() ?? throw new NotCorrectClientNameException();
        Client client = clients.Find(p => p.Name == clientName) ?? throw new InvalidOperationException();
        Account account = new Account(money, client);
        accounts.Add(account);
    }

    if (reader == "Create Bank")
    {
        Console.WriteLine("Enter bank name");
        string bankName = Console.ReadLine() ?? throw new NotCorrectClientNameException();
        string input = " ";
        List<Account> curaccounts = new List<Account>();
        while (input != "end")
        {
            Console.WriteLine("Enter account client name");
            input = Console.ReadLine() ?? throw new InvalidOperationException();
            curaccounts.Add(accounts.Find(p => p.ClientName == input) ?? throw new InvalidOperationException());
        }

        Bank bank = centralBankRealization.CreateBank(bankName, accounts, new SimpleTextNotifiction());
        banks.Add(bank);
    }

    reader = Console.ReadLine();
}