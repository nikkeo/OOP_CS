using System.IO.Compression;
using System.Text;
using Backups.Entities;
using Backups.Models;
using CustomExceptions;
using Zio;
using Zio.FileSystems;

namespace Backups.Services;

public class Repository : IRepository
{
    public Repository(FileSystem fileSystem, string pathToRepositoryrepository)
    {
        Filesystem = fileSystem;
        PathToRepository = pathToRepositoryrepository;
    }

    public FileSystem Filesystem { get; }
    public string PathToRepository { get; }

    public Stream Read(BackupObject backupObject)
    {
        Stream file = Filesystem.OpenFile(Path.Combine(backupObject.OldPath), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        return file;
    }

    public void CreatePath(string path)
    {
        Filesystem.CreateDirectory(path);
    }

    public Stream OpenOrCreateFile(string pathToNewFile)
    {
        var file = Filesystem.OpenFile(pathToNewFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        return file;
    }

    public void DeleteFile(string pathToFile)
    {
        Filesystem.DeleteFile(pathToFile);
    }

    public void DeleteDirectory(string pathToDirectory)
    {
        Filesystem.DeleteDirectory(pathToDirectory, true);
    }

    public void CreateRestorePointInfo(List<FileInformation> fileInformations)
    {
        foreach (FileInformation fileInformation in fileInformations)
        {
            if (!Filesystem.DirectoryExists(fileInformation.PathToCurrentRestorePoint))
            {
                CreatePath(fileInformation.PathToCurrentRestorePoint);
            }

            var file = Filesystem.OpenFile(Path.Combine(fileInformation.PathToCurrentRestorePoint, fileInformation.Filename), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            file.Write(Encoding.ASCII.GetBytes(fileInformation.NewPath));
            file.Close();
        }
    }
}