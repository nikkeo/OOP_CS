using System.Collections.Immutable;
using CustomExceptions;
using Data_Access_layer;

namespace Business_Logic_layer.Models;

public class BDSyncAdminAccount : IAccount, IDisposable
{
    private string _password;
    private List<IAccount> _accounts;
    private DB _bd;
    public BDSyncAdminAccount(string adminName, string adminPassword, string server, string port, string username, string password, string database)
    {
        if (string.IsNullOrWhiteSpace(adminName))
            throw new NotPossibleUsername();
        if (string.IsNullOrWhiteSpace(adminPassword))
            throw new NotPossiblePassword();
        _bd = new DB(server, port, username, password, database);
        Username = adminName;
        _password = adminPassword;
        _accounts = new List<IAccount>();
    }
    
    public string Username { get; }
    public ImmutableList<IAccount> Accounts { get => _accounts.ToImmutableList(); }

    public bool IsPasswordCorrect(string password) => password == _password;
    
    public EmployeeAccount CreateEmployeeAccount(string username, string password)
    {
        EmployeeAccount employeeAccount = new EmployeeAccount(username, password);
        _accounts.Add(employeeAccount);
        _bd.WriteEmployeeAccounts(username, password);
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
        _bd.WriteSourseMessageAccountAccounts(username, password, sourceMessage.ToString());
        return sourceMessageAccount;
    }

    public void SyncUsers()
    {
        List<string> info = _bd.SyncEmployeeAccounts();
        for (int i = 0; i < info.Count; i += 2)
        {
            if (i + 1 == info.Count)
                break;
            _accounts.Add(new EmployeeAccount(info[i], info[i + 1]));
        }
    }
    
    public void Dispose()
    {
        if (_bd.Connection != null) { _bd.Dispose(); }
    }

    public string GetUserName()
    {
        return Username;
    }
}