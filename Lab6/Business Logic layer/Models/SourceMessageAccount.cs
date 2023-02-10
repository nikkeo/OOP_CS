using System.Collections.Immutable;
using CustomExceptions;

namespace Business_Logic_layer.Models;

public class SourceMessageAccount : IAccount
{
    private string _password;
    public SourceMessageAccount(string username, string password, SourceMessage sourceMessage)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new NotPossibleUsername();
        if (string.IsNullOrWhiteSpace(password))
            throw new NotPossiblePassword();
        Username = username;
        _password = password;
        CurSourceMessage = sourceMessage;
    }
    
    public string Username { get; }
    public SourceMessage CurSourceMessage { get; }

    public bool IsPasswordCorrect(string password) => password == _password;
    
    public string GetUserName()
    {
        return Username;
    }
}