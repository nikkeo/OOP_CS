using System.Collections.Immutable;
using CustomExceptions;

namespace Banks.Entities;

public interface INotifiction
{
    string PercentChangeNotifiction();
    string LimitChangeNotifiction();
}