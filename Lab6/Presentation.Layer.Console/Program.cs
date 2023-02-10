// See https://aka.ms/new-console-template for more information

using Business_Logic_layer.Models;
using Data_Access_layer;

Console.WriteLine("Write EXIT to leave or HELP to get functions");
string? reader = Console.ReadLine();
BDSyncAdminAccount? db = null;
AdminAccount? admin = null;
bool isDB = false;
List<IAccount> accounts = new List<IAccount>();
List<Correspondence> correspondences = new List<Correspondence>();
EmployeeAccount currentAccount = null;

while (reader != "EXIT")
{
    if (reader == "HELP")
    {
        Console.WriteLine("Create DBAdmin");
        Console.WriteLine("Create Admin");
        Console.WriteLine("Create Employee");
        Console.WriteLine("Create SourceMessageAccount");
        Console.WriteLine("Log in");
        Console.WriteLine("Create Correspondence");
        Console.WriteLine("Send message");
    }

    if (reader == "Create DBAdmin")
    {
        Console.WriteLine("Enter AdminUserName");
        string adminUserName = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter Password");
        string password = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter Server");
        string server = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter port");
        string port = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter server username");
        string serverUsername = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter server Password");
        string serverPassword = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter database name");
        string database = Console.ReadLine() ?? throw new InvalidOperationException();

        db = new BDSyncAdminAccount(adminUserName, password, server, port, serverUsername, serverPassword, database);
        isDB = true;
    }
    
    if (reader == "Create Admin")
    {
        Console.WriteLine("Enter AdminUserName");
        string adminUserName = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter Password");
        string password = Console.ReadLine() ?? throw new InvalidOperationException();

        admin = new AdminAccount(adminUserName, password);
        accounts.Add(admin);
    }

    if (reader == "Create Employee")
    {
        if (isDB)
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine() ?? throw new InvalidOperationException();
            accounts.Add(db.CreateEmployeeAccount(userName, password));
        }
        else
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine() ?? throw new InvalidOperationException();
            accounts.Add(admin.CreateEmployeeAccount(userName, password));
        }
    }
    
    if (reader == "Create SourceMessageAccount")
    {
        if (isDB)
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Source of Message");
            string sourceMessage = Console.ReadLine() ?? throw new InvalidOperationException();
            accounts.Add(db.CreateSourceMessageAccount(userName, password, new SourceMessage(new List<ICorrespondence>(), sourceMessage)));
        }
        else
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine() ?? throw new InvalidOperationException();
            Console.WriteLine("Enter Source of Message");
            string sourceMessage = Console.ReadLine() ?? throw new InvalidOperationException();
            accounts.Add(admin.CreateSourceMessageAccount(userName, password, new SourceMessage(new List<ICorrespondence>(), sourceMessage)));
        }
    }

    if (reader == "Log in")
    {
        Console.WriteLine("Enter UserName");
        string userName = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Enter Password");
        string password = Console.ReadLine() ?? throw new InvalidOperationException();
        EmployeeAccount account = accounts.Find(p => p.GetUserName() == userName) as EmployeeAccount;
        if (account.IsPasswordCorrect(password))
            currentAccount = account;
        else
            Console.WriteLine("not correct password");
    }
    
    if (reader == "Create Correspondence"){
        Console.WriteLine("Enter Correspondence name");
        string name = Console.ReadLine() ?? throw new InvalidOperationException();

        correspondences.Add(new Correspondence(name));
    }

    if (reader == "Send message")
    {
        if (currentAccount != null)
        {
            Console.WriteLine("Enter Correspondence name");
            string name = Console.ReadLine() ?? throw new InvalidOperationException();
            Correspondence correspondence = correspondences.Find(p => p.Name == name) ?? throw new InvalidOperationException();
            Console.WriteLine("Enter message text");
            string text = Console.ReadLine() ?? throw new InvalidOperationException();
            currentAccount.WriteToCorrespondence(correspondence, new Message(text));
        }
        else
        {
            Console.WriteLine("log in firstly");
        }
    }
}