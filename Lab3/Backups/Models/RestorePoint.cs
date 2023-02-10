using System.Collections.Immutable;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Models;

public class RestorePoint
{
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private List<FileInformation> _fileInformations = new List<FileInformation>();

    public RestorePoint(List<BackupObject> backupObjects, string repository, string backupTaskName, Guid restorePointGuid)
    {
        TimeOfCreation = DateTime.Now;
        _backupObjects = backupObjects;
        string pathToNewFile = Path.Combine(repository, backupTaskName, restorePointGuid.ToString());
        Id = restorePointGuid;
        foreach (BackupObject backupObject in backupObjects)
        {
            if (backupObject.NewPath != null)
                _fileInformations.Add(new FileInformation(Path.GetFileName(backupObject.OldPath), backupObject.NewPath, pathToNewFile));
            BackupObjects.Add(backupObject);
        }
    }

    public ImmutableList<FileInformation> FileInformations { get => _fileInformations.ToImmutableList(); }
    public DateTime TimeOfCreation { get; }
    public ImmutableList<BackupObject> BackupObjects { get => _backupObjects.ToImmutableList(); }
    public Guid Id { get; }
}