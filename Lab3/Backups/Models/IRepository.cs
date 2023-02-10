using Backups.Entities;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Services;

public interface IRepository
{
    public Stream Read(BackupObject backupObject);

    public void CreatePath(string path);

    public Stream OpenOrCreateFile(string pathToNewFile);

    public void CreateRestorePointInfo(List<FileInformation> fileInformations);
}