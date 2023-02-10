using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Extra.Models;

public class ReSingleStorage : IReStorageAlgorithm
{
    public ReSingleStorage(IUnArchavator unArchavator)
    {
        UnArchavator = unArchavator;
    }

    public IUnArchavator UnArchavator { get; }

    public void Restore(Stream file, Stream originalStream)
    {
        UnArchavator.Decompress(file, originalStream);
    }
}