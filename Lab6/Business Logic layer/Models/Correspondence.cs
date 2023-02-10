using System.Collections.Immutable;

namespace Business_Logic_layer.Models;

public class Correspondence
{
    private List<Message> _messages;
    public Correspondence(string name)
    {
        _messages = new List<Message>();
        Name = name;
    }
    
    public Correspondence(List <Message> messages, string name)
    {
        _messages = messages;
        Name = name;
    }
    
    public ImmutableList<Message> Messages { get => _messages.ToImmutableList(); }
    public string Name { get; }

    public void AddMessage(Message message)
    {
        _messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        _messages.Remove(message);
    }

    public string GetCorrespondenceName()
    {
        return Name;
    }
}