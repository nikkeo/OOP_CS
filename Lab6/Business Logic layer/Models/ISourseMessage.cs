using System.Collections.Immutable;

namespace Business_Logic_layer.Models;

public interface ISourceMessage
{
    public void AddCorrespondence(ICorrespondence correspondence);

    public void DeleteCorrespondence(ICorrespondence correspondence);
}