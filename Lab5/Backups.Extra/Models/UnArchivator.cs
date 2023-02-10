using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Extra.Models;

public class UnArchavator : IUnArchavator
{
    public void Decompress(Stream newFile, Stream originalStream)
    {
        GZipStream zip = new GZipStream(originalStream, CompressionMode.Decompress, false);
        zip.CopyTo(newFile);
    }
}