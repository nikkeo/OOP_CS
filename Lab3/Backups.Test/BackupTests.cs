using Backups.Entities;
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
        BackupTask backupTask =
            new BackupTask("one", testPath, new SpilitStorage(testPath, new Archavator()), fileSystem);
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        backupTask.CreateRestorePoint(guid1);

        backupTask.DeleteBackupObject(backupObject1);
        backupTask.CreateRestorePoint(guid2);

        Assert.True(
            fileSystem.FileExists(Path.Combine(testPath, backupTask.Name, guid1.ToString(), "1.txt"))
            && fileSystem.FileExists(Path.Combine(testPath, backupTask.Name, guid1.ToString(), "2.txt"))
            && fileSystem.FileExists(Path.Combine(testPath, backupTask.Name, guid2.ToString(), "2.txt")),
            "RestorePoints Are Inscorrect");
    }

    [Fact]
    public void SingleStorageTest_ThrowException()
    {
        UPath rootPath = UPath.Root;
        FileSystem fileSystem = new MemoryFileSystem();
        fileSystem.CreateDirectory(rootPath);
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
        BackupTask backupTask =
            new BackupTask("one", testPath, new SingleStorage(testPath, new Archavator()), fileSystem);
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);

        backupTask.CreateRestorePoint(Guid.NewGuid());
        Assert.True(
            fileSystem.FileExists(Path.Combine(testPath, "storage.zip")),
            "SingleStorage is Inscorrect");
    }
}