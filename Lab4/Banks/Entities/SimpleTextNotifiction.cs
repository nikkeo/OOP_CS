using System.Collections.Immutable;
using CustomExceptions;

namespace Banks.Entities;

public class SimpleTextNotifiction : INotifiction
{
    public string PercentChangeNotifiction()
    {
        return "Percents in your bank have been changed, check your account for more information.";
    }

    public string LimitChangeNotifiction()
    {
        return "Limit in your bank has been changed, check your account for more information.";
    }
}