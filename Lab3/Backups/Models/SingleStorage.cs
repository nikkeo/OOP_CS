using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Models;

public class SingleStorage : IStorageAlgorithms
{
    public SingleStorage(string zipPath, IArchavator archavator)
    {
        ZipPath = zipPath;
        Archavator = archavator;
    }

    public string ZipPath { get; private set; }

    public IArchavator Archavator { get; }

    public void Store(Stream file, Stream originalStream, BackupObject backupObject)
    {
        backupObject.NewPath = PathToZip(backupObject);
        Archavator.Compress(file, originalStream);
    }

    public string PathToZip(BackupObject backupObject)
    {
        return Path.Combine(ZipPath, "storage.zip");
    }
}