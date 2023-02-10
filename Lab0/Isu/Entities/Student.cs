using CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private Group _group;

    public Student(Group group, string name, int id)
    {
        _group = group;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NotValidStudentNameException();
        }

        StudentName = name;
        UniqueId = id;
        group.AddStudent(this);
    }

    public int UniqueId { get; }
    public string StudentName { get; }
    public GroupName GroupName { get => _group.Groupname; }

    public void ChangeGroup(Group group)
    {
        if (group == null)
        {
            throw new NotFoundGroupException();
        }

        group.AddStudent(this);
        _group.DeleteStudent(this);
        _group = group;
    }
}