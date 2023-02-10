using System.IO.Compression;
using Backups.Models;
using CustomExceptions;

namespace Backups.Extra.Models;

public interface IUnArchavator
{
    public void Decompress(Stream newFile, Stream originalStream);
}