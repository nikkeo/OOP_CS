namespace Business_Logic_layer.Models;

public class Message
{
    public Message(string message)
    {
        MessageText = message;
        Status = MessageStatus.New;
    }
    
    public string MessageText { get; }
    public MessageStatus Status { get; private set; }

    public void MessageRecieved()
    {
        Status = MessageStatus.Recieved;
    }

    public void MessageWasChecked()
    {
        Status = MessageStatus.Checked;
    }
}