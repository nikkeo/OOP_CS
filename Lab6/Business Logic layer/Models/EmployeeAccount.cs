using System.Collections.Immutable;
using CustomExceptions;

namespace Business_Logic_layer.Models;

public class EmployeeAccount : IAccount
{
    private string _password;
    private List<IReportMessage> _upToDateReport;
    private List<IReportMessage> _report;
    private List<EmployeeAccount> _supervisors;
    private List<EmployeeAccount> _subordinates;
    public EmployeeAccount(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new NotPossibleUsername();
        if (string.IsNullOrWhiteSpace(password))
            throw new NotPossiblePassword();
        Username = username;
        _password = password;
        _supervisors = new List<EmployeeAccount>();
        _subordinates = new List<EmployeeAccount>();
        _upToDateReport = new List<IReportMessage>();
        _report = new List<IReportMessage>();
    }
    
    public string Username { get; }
    public ImmutableList<EmployeeAccount> Supervisors { get => _supervisors.ToImmutableList(); }
    public ImmutableList<EmployeeAccount> Subordinates { get => _subordinates.ToImmutableList(); }
    public ImmutableList<IReportMessage> Report { get => _report.ToImmutableList(); }

    public bool IsPasswordCorrect(string password) => password == _password;

    public void AddSupervisor(EmployeeAccount supervisor)
    {
        _supervisors.Add(supervisor);
    }
    
    public void DeleteSupervisor(EmployeeAccount supervisor)
    {
        _supervisors.Remove(supervisor);
    }
    
    public void AddSubordinate(EmployeeAccount subordinate)
    {
        _subordinates.Add(subordinate);
    }
    
    public void DeleteSubordinate(EmployeeAccount subordinate)
    {
        _subordinates.Remove(subordinate);
    }

    public void WriteToCorrespondence(Correspondence correspondence, Message message)
    {
        correspondence.AddMessage(message);
        message.MessageRecieved();
        ReportMessage reportMessage = new ReportMessage(message, correspondence, this);
        _upToDateReport.Add(reportMessage);
        
        _supervisors.ForEach(p => p.AddMessageToReport(reportMessage));
    }

    public void AddMessageToReport(ReportMessage repostMessage)
    {
        _upToDateReport.Add(repostMessage);
    }

    public void MakeReportUpToDate()
    {
        _report = _upToDateReport;
    }

    public void CheckMessage(Message message)
    {
        message.MessageWasChecked();
    }

    public string GetUserName()
    {
        return Username;
    }
}