using Backups.Entities;
using Backups.Extra.Models;
using Backups.Extra.Services;
using Backups.Models;
using Backups.Services;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupTests
{
    [Fact]
    public void SplitStorageTest_ThrowException()
    {
        UPath rootPath = UPath.Root;
        FileSystem fileSystem = new MemoryFileSystem();
        fileSystem.CreateDirectory(rootPath);
        BackupExtraAddon backupExtraAddon = new BackupExtraAddon();
        string curpath = Path.Combine(rootPath.FullName, "example");
        string testPath = Path.Combine(curpath, "test");
        fileSystem.CreateDirectory(curpath);
        fileSystem.CreateDirectory(testPath);
        var file = fileSystem.CreateFile(Path.Combine(curpath, "1.txt"));
        file.Close();
        var file2 = fileSystem.CreateFile(Path.Combine(curpath, "2.txt"));
        file2.Close();
        BackupObject backupObject1 = new BackupObject(Path.Combine(curpath, "1.txt"));
        BackupObject backupObject2 = new BackupObject(Path.Combine(curpath, "2.txt"));
        BackupTask backupTask = backupExtraAddon.CreateBackupTask("one", testPath, new SpilitStorage(testPath, new Archavator()), fileSystem, new ConsoleLog(), new RestorePointQuantityLimit(100));
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        Guid guid1 = Guid.NewGuid();
        RestorePoint restorePoint = backupExtraAddon.CreateRestorePoint(backupTask, guid1);

        fileSystem.DeleteFile(Path.Combine(curpath, "1.txt"));
        fileSystem.DeleteFile(Path.Combine(curpath, "2.txt"));

        Assert.False(
            fileSystem.FileExists(Path.Combine(curpath, "1.txt"))
            && fileSystem.FileExists(Path.Combine(curpath, "2.txt")),
            "File Were Not Deleted");

        backupExtraAddon.Restore(restorePoint, backupTask, new ReSpilitStorage(new UnArchavator()));

        Assert.True(
            fileSystem.FileExists(Path.Combine(curpath, "1.txt"))
            && fileSystem.FileExists(Path.Combine(curpath, "2.txt")),
            "File Were Not Restored");
    }

    [Fact]
    public void SingleStorageTest_ThrowException()
    {
        UPath rootPath = UPath.Root;
        FileSystem fileSystem = new MemoryFileSystem();
        fileSystem.CreateDirectory(rootPath);
        BackupExtraAddon backupExtraAddon = new BackupExtraAddon();
        string curpath = Path.Combine(rootPath.FullName, "example");
        string testPath = Path.Combine(curpath, "test");
        fileSystem.CreateDirectory(curpath);
        fileSystem.CreateDirectory(testPath);
        var file = fileSystem.CreateFile(Path.Combine(curpath, "1.txt"));
        file.Close();
        var file2 = fileSystem.CreateFile(Path.Combine(curpath, "2.txt"));
        file2.Close();
        BackupObject backupObject1 = new BackupObject(Path.Combine(curpath, "1.txt"));
        BackupObject backupObject2 = new BackupObject(Path.Combine(curpath, "2.txt"));
        BackupTask backupTask = backupExtraAddon.CreateBackupTask("one", testPath, new SingleStorage(testPath, new Archavator()), fileSystem, new ConsoleLog(), new RestorePointQuantityLimit(100));
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        Guid guid1 = Guid.NewGuid();
        RestorePoint restorePoint = backupExtraAddon.CreateRestorePoint(backupTask, guid1);

        fileSystem.DeleteFile(Path.Combine(curpath, "1.txt"));
        fileSystem.DeleteFile(Path.Combine(curpath, "2.txt"));

        Assert.False(
            fileSystem.FileExists(Path.Combine(curpath, "1.txt"))
            && fileSystem.FileExists(Path.Combine(curpath, "2.txt")),
            "File Were Not Deleted");

        backupExtraAddon.Restore(restorePoint, backupTask, new ReSingleStorage(new UnArchavator()));

        Assert.True(
            fileSystem.FileExists(Path.Combine(curpath, "1.txt"))
            && fileSystem.FileExists(Path.Combine(curpath, "2.txt")),
            "File Were Not Restored");
    }
}