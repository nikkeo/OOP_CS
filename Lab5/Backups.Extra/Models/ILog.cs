using System.Collections.Immutable;
using Backups.Models;

namespace Backups.Extra.Models;

public interface ILog
{
    public void SendMessage(string message);
}