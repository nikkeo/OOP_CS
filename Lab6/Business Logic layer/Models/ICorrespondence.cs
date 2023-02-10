using System.Collections.Immutable;

namespace Business_Logic_layer.Models;

public interface ICorrespondence
{
    public void AddMessage(Message message);

    public void DeleteMessage(Message message);
    
    public string GetCorrespondenceName();
}