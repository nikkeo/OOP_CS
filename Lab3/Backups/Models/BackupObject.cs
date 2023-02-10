namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string path)
    {
        OldPath = path;
    }

    public string OldPath { get; }

    public string? NewPath { get; set; }
}