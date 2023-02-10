using System.Collections.Immutable;
using Backups.Entities;
using Backups.Extra.Models;
using Backups.Models;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Services;

public interface IBackupExtraAddon
{
    public BackupTask CreateBackupTask(string name, string pathToRepository, IStorageAlgorithms storageAlgorithm, FileSystem filesystem, ILog log, IRestorePointLimit restorePointLimit);

    public void AddBackupObject(BackupTask backupTask, BackupObject backupObject);

    public void DeleteBackupObject(BackupTask backupTask, BackupObject backupObject);

    public RestorePoint CreateRestorePoint(BackupTask backupTask, Guid id);

    public RestorePoint Merge(BackupTask backupTask, RestorePoint firstPoint, RestorePoint secondPoint, Guid newguid);

    public void CheckRestorePointLimit(BackupTask backupTask);
}