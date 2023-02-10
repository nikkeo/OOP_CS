using System.Collections.Immutable;
using Backups.Entities;
using Backups.Extra.Models;
using Backups.Models;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Services;

public class BackupExtraAddon : IBackupExtraAddon
{
    private List<BackupTask> _backupTasks = new List<BackupTask>();
    private Dictionary<BackupTask, ILog> _logs = new Dictionary<BackupTask, ILog>();
    private Dictionary<BackupTask, IRestorePointLimit> _restorePointLimits = new Dictionary<BackupTask, IRestorePointLimit>();

    public BackupExtraAddon() { }

    public BackupTask CreateBackupTask(string name, string pathToRepository, IStorageAlgorithms storageAlgorithm, FileSystem filesystem, ILog log, IRestorePointLimit restorePointLimit)
    {
        BackupTask backupTask = new BackupTask(name, pathToRepository, storageAlgorithm, filesystem);
        _backupTasks.Add(backupTask);
        _logs.Add(backupTask, log);
        _restorePointLimits.Add(backupTask, restorePointLimit);
        log.SendMessage("BackupTask was created");
        return backupTask;
    }

    public void AddBackupObject(BackupTask backupTask, BackupObject backupObject)
    {
        backupTask.AddBackupObject(backupObject);
        _logs[backupTask].SendMessage("BackupObject was added to BackupTask");
    }

    public void DeleteBackupObject(BackupTask backupTask, BackupObject backupObject)
    {
        backupTask.DeleteBackupObject(backupObject);
        _logs[backupTask].SendMessage("BackupObject was deleted from BackupTask");
    }

    public RestorePoint CreateRestorePoint(BackupTask backupTask, Guid id)
    {
        backupTask.CreateRestorePoint(id);
        _logs[backupTask].SendMessage("RestorePoint was created");
        _restorePointLimits[backupTask].Check(backupTask.RestorePoints.ToList(), backupTask);
        return backupTask.RestorePoints.ToList().Find(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public RestorePoint? GetRestorePoint(BackupTask backupTask, Guid id)
    {
        return backupTask.RestorePoints.ToList().Find(p => p.Id == id);
    }

    public RestorePoint Merge(BackupTask backupTask, RestorePoint firstPoint, RestorePoint secondPoint, Guid newguid)
    {
        if (!backupTask.RestorePoints.Contains(firstPoint) || !backupTask.RestorePoints.Contains(secondPoint))
            throw new NotCorrectRestorePointException();

        if (backupTask.StorageAlgorithm is SingleStorage)
        {
            backupTask.DeleteRestorePoint(firstPoint);
            return secondPoint;
        }

        List<BackupObject> backupObjects = secondPoint.BackupObjects.ToList();
        backupObjects.Concat(firstPoint.BackupObjects.ToList());
        RestorePoint restorePoint = new RestorePoint(backupObjects, backupTask.CurrentRepository.PathToRepository, backupTask.Name, newguid);
        backupTask.DeleteRestorePoint(firstPoint);
        backupTask.DeleteRestorePoint(secondPoint);

        return restorePoint;
    }

    public void CheckRestorePointLimit(BackupTask backupTask)
    {
        _restorePointLimits[backupTask].Check(backupTask.RestorePoints.ToList(), backupTask);
    }

    public void Restore(RestorePoint restorePoint, BackupTask backupTask, IReStorageAlgorithm reStorageAlgorithm, string newPath = "")
    {
        string path;
        foreach (BackupObject backupObject in restorePoint.BackupObjects.ToList())
        {
            if (string.IsNullOrWhiteSpace(newPath))
                path = backupObject.OldPath;
            else
                path = Path.Combine(newPath, Path.GetFileName(backupObject.OldPath));
            Stream oldFile =
                backupTask.CurrentRepository.OpenOrCreateFile(backupObject.NewPath ??
                                                              throw new InvalidOperationException());
            Stream newfile = backupTask.CurrentRepository.OpenOrCreateFile(path);
            reStorageAlgorithm.Restore(newfile, oldFile);
            oldFile.Close();
            newfile.Close();
        }
    }
}