using System.Text;
using Backups.Entities;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public interface IRestorePointLimit
{
    public List<RestorePoint> Check(List<RestorePoint> restorePoints, BackupTask backupTask);
}