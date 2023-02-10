using CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class UniqueIdSystem
{
    private int _uniqueId = 100000;

    public UniqueIdSystem() { }

    public int CreateNewUniqueId()
    {
        return _uniqueId++;
    }
}