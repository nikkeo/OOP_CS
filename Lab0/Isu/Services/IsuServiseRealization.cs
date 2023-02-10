using System.Linq.Expressions;
using CustomExceptions;
using Isu.Entities;
using Isu.Models;
using Isu.Services;

namespace Isu.Services;

public class IsuServiceRealization : IIsuService
{
    private List<Group> _groupList = new List<Group>();

    public Group AddGroup(GroupName name)
    {
        var groupAlreadyExists = _groupList.Where(p => p.Groupname.Equals(name));
        if (groupAlreadyExists != null)
        {
            throw new GroupAlreadyExistsException();
        }

        var group = new Group(name);
        _groupList.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name, UniqueIdSystem uniqueId)
    {
        var student = new Student(group, name, uniqueId.CreateNewUniqueId());
        return student;
    }

    public Student GetStudent(int id)
    {
        if (_groupList.Find(p => p.IsStudentInGroup(id)) == null)
        {
            throw new StudentWasNotFoundException();
        }

        return FindStudent(id) ?? throw new InvalidOperationException();
    }

    public Student? FindStudent(int id)
    {
        return _groupList.First(p => p.IsStudentInGroup(id)).StudentList.Find(p => p.UniqueId == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group? group = FindGroup(groupName);

        return group != null ? group.StudentList.ToList() : new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return FindGroups(courseNumber).SelectMany(u => u.StudentList).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groupList.Find(p => p.Groupname.Equals(groupName));
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groupList.Where(p => p.Groupname.Coursenumber.Coursenumber == courseNumber.Coursenumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student == null || _groupList.Find(p => p.IsStudentInGroup(student.UniqueId)) != null)
        {
            throw new NotFoundStudentException();
        }

        if (newGroup == null || _groupList.Find(g => g == newGroup) != null)
        {
            throw new GroupAlreadyExistsException();
        }

        student.ChangeGroup(newGroup);
    }
}