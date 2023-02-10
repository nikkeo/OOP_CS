using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Models;

public class Archavator : IArchavator
{
    public void Compress(Stream newFile, Stream originalStream)
    {
        GZipStream zip = new GZipStream(newFile, CompressionMode.Compress, true);
        originalStream.CopyTo(zip);
    }
}