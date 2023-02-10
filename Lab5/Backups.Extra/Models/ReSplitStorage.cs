using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Extra.Models;

public class ReSpilitStorage : IReStorageAlgorithm
{
    public ReSpilitStorage(IUnArchavator unArchavator)
    {
        UnArchavator = unArchavator;
    }

    public IUnArchavator UnArchavator { get; }

    public void Restore(Stream file, Stream originalStream)
    {
        UnArchavator.Decompress(file, originalStream);
    }
}