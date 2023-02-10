using System.Collections.Immutable;
using Backups.Services;
using Zio.FileSystems;

namespace Backups.Models;

public class Backup
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();

    public Backup() { }

    public ImmutableList<RestorePoint> RestorePoints { get => _restorePoints.ToImmutableList(); }

    public List<FileInformation> CreateRestorePoint(List<BackupObject> backupObjects, string repository, string backupTaskName, Guid restorePointGuid)
    {
        _restorePoints.Add(new RestorePoint(backupObjects, repository, backupTaskName, restorePointGuid));
        return _restorePoints.Last().FileInformations.ToList();
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Remove(restorePoint);
    }
}