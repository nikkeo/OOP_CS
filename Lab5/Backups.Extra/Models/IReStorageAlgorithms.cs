using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Extra.Models;

public interface IReStorageAlgorithm
{
    public void Restore(Stream file, Stream originalStream);
}