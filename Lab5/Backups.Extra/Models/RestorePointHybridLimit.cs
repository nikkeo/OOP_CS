using System.Text;
using Backups.Entities;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public class RestorePointHybridLimit : IRestorePointLimit
{
    public const int MinAmountOfListElements = 1;
    public RestorePointHybridLimit(TimeSpan dateTimelimit, int limit, bool bothNessesary)
    {
        RestorePointQuantityLimit = new RestorePointQuantityLimit(limit);
        RestorePointDateLimit = new RestorePointDateLimit(dateTimelimit);
    }

    public RestorePointDateLimit RestorePointDateLimit { get; }
    public RestorePointQuantityLimit RestorePointQuantityLimit { get; }

    public bool BothNessesary { get; }

    public List<RestorePoint> Check(List<RestorePoint> restorePoints, BackupTask backupTask)
    {
        if (BothNessesary)
            return BothNessesaryCheck(restorePoints, backupTask);
        else
            return SingleNessesaryCheck(restorePoints, backupTask);
    }

    private List<RestorePoint> BothNessesaryCheck(List<RestorePoint> restorePoints, BackupTask backupTask)
    {
        List<RestorePoint> toRemove = restorePoints.FindAll(p => ((p.TimeOfCreation + RestorePointDateLimit.DateTimeLimit) < DateTime.Now));

        foreach (RestorePoint restorePoint in toRemove)
        {
            if (restorePoints.Count == MinAmountOfListElements)
                return restorePoints;
            if (restorePoints.Count < RestorePointQuantityLimit.Limit)
                break;
            backupTask.DeleteRestorePoint(restorePoint);
            restorePoints.Remove(restorePoint);
        }

        if (restorePoints.Count > RestorePointQuantityLimit.Limit)
            restorePoints = RestorePointDateLimit.Check(restorePoints, backupTask);
        return restorePoints;
    }

    private List<RestorePoint> SingleNessesaryCheck(List<RestorePoint> restorePoints, BackupTask backupTask)
    {
        restorePoints = RestorePointDateLimit.Check(restorePoints, backupTask);
        return RestorePointQuantityLimit.Check(restorePoints, backupTask);
    }
}