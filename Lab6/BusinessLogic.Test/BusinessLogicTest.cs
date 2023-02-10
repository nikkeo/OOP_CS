using Xunit;
using Business_Logic_layer.Models;

namespace BusinessLogic.Test;

public class UnitTest1
{
    [Fact]
    public void MessageRecievedAndChecked_throwException()
    {
        AdminAccount admin = new AdminAccount("root", "root");
        EmployeeAccount account1 = admin.CreateEmployeeAccount("user", "user");
        Correspondence correspondence = new Correspondence("1");
        Message message = new Message("hello");
        Assert.True(message.Status == MessageStatus.New, "Message status is not correct");

        account1.WriteToCorrespondence(correspondence, message);
        Assert.True(message.Status == MessageStatus.Recieved, "Message was not recieved");
        
        account1.CheckMessage(message);
        Assert.True(message.Status == MessageStatus.Checked, "Message was not checked");
    }

    [Fact]
    public void SupervisorGetsReportFromSubordinate_throwException()
    {
        AdminAccount admin = new AdminAccount("root", "root");
        EmployeeAccount supervisor = admin.CreateEmployeeAccount("user", "user"); 
        EmployeeAccount subordinate = admin.CreateEmployeeAccount("user", "user");
        Correspondence correspondence = new Correspondence("2");
        Message message = new Message("hello");
        
        admin.MakeSupervisorConnection(supervisor, subordinate);
        
        subordinate.WriteToCorrespondence(correspondence, message);
        Assert.False(subordinate.Report.ToList().Find(p => p.GetMessage() == message) != null, "Report contains message, which it can not contain");
        subordinate.MakeReportUpToDate();
        Assert.True(subordinate.Report.ToList().Find(p => p.GetMessage() == message) != null, "Report is not contain message");
        
        Assert.False(supervisor.Report.ToList().Find(p => p.GetMessage() == message) != null, "Report contains message, which it can not contain");
        supervisor.MakeReportUpToDate();
        Assert.True(supervisor.Report.ToList().Find(p => p.GetMessage() == message) != null, "Report is not contain message");
    }
}