using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Models;

public class SpilitStorage : IStorageAlgorithms
{
    public SpilitStorage(string repository, IArchavator archavator)
    {
        Repository = repository;
        Archavator = archavator;
    }

    public string Repository { get; }
    public IArchavator Archavator { get; }

    public void Store(Stream file, Stream originalStream, BackupObject backupObject)
    {
        backupObject.NewPath = PathToZip(backupObject);
        Archavator.Compress(file, originalStream);
    }

    public string PathToZip(BackupObject backupObject)
    {
        return Path.Combine(Repository, Path.GetFileNameWithoutExtension(backupObject.OldPath) + ".zip");
    }
}