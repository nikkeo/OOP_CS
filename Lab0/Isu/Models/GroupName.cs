using CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int LowerBoundNum = 1;
    private const int HighBoundNum = 4;
    private const int SizeOfGroupNumber = 5;
    private const int DoubleDigitCheck = 9;
    private int _groupNumber;
    public GroupName(string name)
    {
        if (!char.IsUpper(name[0]) || (name[1] - '0') < LowerBoundNum || (name[1] - '0') > HighBoundNum)
        {
            throw new NotValidFacultyInfoException("Not correct faculty info");
        }

        if (name.Length > SizeOfGroupNumber || name.Length < SizeOfGroupNumber)
        {
            throw new NotValidGroupNameException("The group name size is not valid");
        }

        GroupNameString = name;
        Coursenumber = new CourseNumber(name[2] - '0');
        _groupNumber = (name.Length == 5) ? ((name[3] - '0') * 10) + (name[4] - '0') : (name[3] - '0');
    }

    public CourseNumber Coursenumber { get; }

    public string GroupNameString { get; }

    public override bool Equals(object? obj) => Equals(obj as GroupName);
    public bool Equals(GroupName? other)
    {
        if (other == null)
            return false;
        if (Coursenumber.Coursenumber != (other.GroupNameString[2] - '0'))
        {
            return false;
        }

        if (_groupNumber > DoubleDigitCheck ? _groupNumber != ((other.GroupNameString[3] * 10) + other.GroupNameString[4])
                : _groupNumber != (other.GroupNameString[4] - '0'))
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return GroupNameString.GetHashCode();
    }
}