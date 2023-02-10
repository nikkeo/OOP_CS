using System.Collections.Immutable;
using System.IO.Compression;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class BackupTask
{
    private Backup _backup = new Backup();
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private List<BackupObject> _notStoragedBackupObjects = new List<BackupObject>();
    public BackupTask(string name, string pathToRepository, IStorageAlgorithms storageAlgorithm, FileSystem filesystem)
    {
        Name = name;
        CurrentRepository = new Repository(filesystem, pathToRepository);
        StorageAlgorithm = storageAlgorithm;
        CurrentRepository.CreatePath(pathToRepository);
    }

    public string Name { get; }
    public Repository CurrentRepository { get; }
    public IStorageAlgorithms StorageAlgorithm { get; }
    public ImmutableList<BackupObject> BackupObjects { get => _backupObjects.ToImmutableList(); }
    public ImmutableList<RestorePoint> RestorePoints { get => _backup.RestorePoints; }

    public void AddBackupObject(BackupObject backupObject)
    {
        _backupObjects.Add(backupObject);
        _notStoragedBackupObjects.Add(backupObject);
    }

    public void Store(BackupObject backupObject)
    {
        Stream originalStream = CurrentRepository.Read(backupObject);
        Stream fileToZip = CurrentRepository.OpenOrCreateFile(StorageAlgorithm.PathToZip(backupObject));
        StorageAlgorithm.Store(fileToZip, originalStream, backupObject);
        fileToZip.Close();
        originalStream.Close();
    }

    public void DeleteBackupObject(BackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
            throw new NotValidBackupObjectException();
        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint(Guid aguid)
    {
        _notStoragedBackupObjects.ForEach(p => Store(p));
        _notStoragedBackupObjects = new List<BackupObject>();
        List<FileInformation> fileInformations = _backup.CreateRestorePoint(_backupObjects, CurrentRepository.PathToRepository, Name, aguid);
        CurrentRepository.CreateRestorePointInfo(fileInformations);
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        CurrentRepository.DeleteDirectory(restorePoint.FileInformations.First().PathToCurrentRestorePoint);
        _backup.DeleteRestorePoint(restorePoint);
    }
}