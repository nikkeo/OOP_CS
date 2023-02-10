namespace Backups.Models;

public class FileInformation
{
    public FileInformation(string filename, string newPath, string pathToCurrentRestorePoint)
    {
        Filename = filename;
        NewPath = newPath;
        PathToCurrentRestorePoint = pathToCurrentRestorePoint;
    }

    public string Filename { get; }
    public string NewPath { get; }
    public string PathToCurrentRestorePoint { get; }
}