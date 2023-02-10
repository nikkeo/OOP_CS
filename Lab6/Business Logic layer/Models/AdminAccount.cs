using System.Collections.Immutable;
using CustomExceptions;

namespace Business_Logic_layer.Models;

public class AdminAccount : IAccount
{
    private string _password;
    private List<IAccount> _accounts;
    public AdminAccount(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new NotPossibleUsername();
        if (string.IsNullOrWhiteSpace(password))
            throw new NotPossiblePassword();
        Username = username;
        _password = password;
        _accounts = new List<IAccount>();
    }
    
    public string Username { get; }
    public ImmutableList<IAccount> Accounts { get => _accounts.ToImmutableList(); }

    public bool IsPasswordCorrect(string password) => password == _password;
    
    public EmployeeAccount CreateEmployeeAccount(string username, string password)
    {
        EmployeeAccount employeeAccount = new EmployeeAccount(username, password);
        _accounts.Add(employeeAccount);
        return employeeAccount;
    }

    public void MakeSupervisorConnection(EmployeeAccount supervisor, EmployeeAccount subordinates)
    {
        supervisor.AddSubordinate(subordinates);
        subordinates.AddSupervisor(supervisor);
    }

    public SourceMessageAccount CreateSourceMessageAccount(string username, string password, SourceMessage sourceMessage)
    {
        SourceMessageAccount sourceMessageAccount = new SourceMessageAccount(username, password, sourceMessage);
        _accounts.Add(sourceMessageAccount);
        return sourceMessageAccount;
    }
    
    public string GetUserName()
    {
        return Username;
    }
}