using System.Collections.Generic;
using System.Collections.Immutable;
using CustomExceptions;

namespace Business_Logic_layer.Models;

public class SourceMessage
{
    private List<ICorrespondence> _correspondences;
    public SourceMessage(string sourceOfMessage)
    {
        if (string.IsNullOrWhiteSpace(sourceOfMessage))
            throw new NotPossibleSourceName();
        _correspondences = new List<ICorrespondence>();
        SourceOfMessage = sourceOfMessage;
    }
    
    public SourceMessage(List <ICorrespondence> messages, string sourceOfMessage)
    {
        if (string.IsNullOrWhiteSpace(sourceOfMessage))
            throw new NotPossibleSourceName();
        _correspondences = messages;
        SourceOfMessage = sourceOfMessage;
    }
    
    public ImmutableList<ICorrespondence> Correspondences { get => _correspondences.ToImmutableList(); }
    
    public string SourceOfMessage { get; }

    public void AddCorrespondence(ICorrespondence correspondence)
    {
        _correspondences.Add(correspondence);
    }
    
    public void DeleteCorrespondence(ICorrespondence correspondence)
    {
        _correspondences.Remove(correspondence);
    }
}