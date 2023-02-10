using System.Text;
using Backups.Models;
using Backups.Services;
using CustomExceptions;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public class FileLog : ILog
{
    public FileLog(string filepath, Repository repository, bool prefix = false)
    {
        if (string.IsNullOrWhiteSpace(filepath))
            throw new NotCorrectFilePathException();
        File = repository.OpenOrCreateFile(filepath);
        Prefix = prefix;
    }

    public Stream File { get; }
    public bool Prefix { get; }

    public void SendMessage(string message)
    {
        string final_message;
        if (Prefix)
            final_message = DateTime.Now + message;
        else
            final_message = message;

        File.Write(Encoding.ASCII.GetBytes(final_message));
    }
}