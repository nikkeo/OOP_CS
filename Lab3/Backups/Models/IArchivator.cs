using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Models;

public interface IArchavator
{
    public void Compress(Stream newFile, Stream originalStream);
}