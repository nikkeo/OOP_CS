using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Models;

public interface IStorageAlgorithms
{
    public void Store(Stream file, Stream originalStream, BackupObject backupObject);
    public string PathToZip(BackupObject backupObject);
}