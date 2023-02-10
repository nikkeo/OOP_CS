using System.Text;
using Backups.Entities;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public class RestorePointQuantityLimit : IRestorePointLimit
{
    public const int ZeroLimit = 0;
    public RestorePointQuantityLimit(int limit)
    {
        if (limit <= ZeroLimit)
            throw new NotCorrectValueException();
        Limit = limit;
    }

    public int Limit { get; }

    public List<RestorePoint> Check(List<RestorePoint> restorePoints, BackupTask backupTask)
    {
        while (Limit < restorePoints.Count)
        {
            backupTask.DeleteRestorePoint(restorePoints[ZeroLimit]);
            restorePoints.RemoveAt(ZeroLimit);
        }

        return restorePoints;
    }
}