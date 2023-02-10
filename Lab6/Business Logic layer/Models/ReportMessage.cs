namespace Business_Logic_layer.Models;

public class ReportMessage : IReportMessage
{
    public ReportMessage(Message message, Correspondence correspondence, IAccount sender)
    {
        CurMessage = message;
        CurCorrespondence = correspondence;
        Sender = sender;
    }
    
    public Message CurMessage { get; }
    public Correspondence CurCorrespondence { get;}
    public IAccount Sender { get; }

    public Message GetMessage()
    {
        return CurMessage;
    }
}