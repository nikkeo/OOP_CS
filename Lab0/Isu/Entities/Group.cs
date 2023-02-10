using System.Collections.Immutable;
using System.Collections.ObjectModel;
using CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private List<Student> _studentlist;
    public Group(GroupName name)
    {
        Groupname = name;
        _studentlist = new List<Student>();
    }

    public int MaxGroupCapacity { get; } = 30;

    public GroupName Groupname { get; }

    public ImmutableList<Student> StudentList { get => _studentlist.ToImmutableList(); }

    public void AddStudent(Student student)
    {
        if (_studentlist.Count == MaxGroupCapacity)
        {
            throw new GroupIsOverloadedException("Group is overloaded");
        }

        if (_studentlist.Contains(student))
        {
            throw new StudentAlreadyInGroupException();
        }

        _studentlist.Add(student);
    }

    public void DeleteStudent(Student student)
    {
        if (student == null)
        {
            throw new NotFoundStudentException();
        }

        if (!_studentlist.Contains(student))
        {
            throw new StudentWasNotFoundException();
        }

        _studentlist.Remove(student);
    }

    public bool IsStudentInGroup(int id)
    {
        if (_studentlist.Find(p => p.UniqueId == id) != null)
        {
            return true;
        }

        return false;
    }

    public override bool Equals(object? obj) => Equals(obj as Group);
    public bool Equals(Group? other)
    {
        if (other == null)
            return false;
        if (Groupname.Equals(other.Groupname))
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Groupname.GetHashCode();
    }
}