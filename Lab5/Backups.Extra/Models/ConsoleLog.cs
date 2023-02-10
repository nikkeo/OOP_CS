using Backups.Models;

namespace Backups.Extra.Models;

public class ConsoleLog : ILog
{
    public ConsoleLog(bool prefix = false)
    {
        Prefix = prefix;
    }

    public bool Prefix { get; }

    public void SendMessage(string message)
    {
        string final_message;
        if (Prefix)
            final_message = DateTime.Now + message;
        else
            final_message = message;

        Console.WriteLine(final_message);
    }
}