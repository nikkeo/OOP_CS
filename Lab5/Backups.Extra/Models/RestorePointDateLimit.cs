using System.Text;
using Backups.Entities;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public class RestorePointDateLimit : IRestorePointLimit
{
    public const int MinAmountOfListElements = 1;
    public RestorePointDateLimit(TimeSpan dateTimelimit)
    {
        DateTimeLimit = dateTimelimit;
    }

    public TimeSpan DateTimeLimit { get; }

    public List<RestorePoint> Check(List<RestorePoint> restorePoints, BackupTask backupTask)
    {
        List<RestorePoint> toRemove = restorePoints.FindAll(p => ((p.TimeOfCreation + DateTimeLimit) < DateTime.Now));

        foreach (RestorePoint restorePoint in toRemove)
        {
            if (restorePoints.Count == MinAmountOfListElements)
                return restorePoints;
            backupTask.DeleteRestorePoint(restorePoint);
            restorePoints.Remove(restorePoint);
        }

        return restorePoints;
    }
}