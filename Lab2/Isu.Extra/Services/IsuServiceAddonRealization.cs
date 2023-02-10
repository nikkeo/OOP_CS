using CustomExceptions;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public class IsuServiceAddonRealization : IIsuServiceAddon
{
    private List<Group> _groupList = new List<Group>();
    private List<OGNP> _ognpGroups = new List<OGNP>();
    private Dictionary<Student, StudentSchedule> _studentOrganization = new Dictionary<Student, StudentSchedule>();
    private List<GroupWithSchedule> _groupsWithSchedule = new List<GroupWithSchedule>();

    public Group AddGroup(GroupName name, Schedule schedule)
    {
        bool groupAlreadyExists = _groupList.Exists(p => p.Groupname.Equals(name));
        if (groupAlreadyExists)
        {
            throw new GroupAlreadyExistsException();
        }

        var group = new Group(name);
        _groupsWithSchedule.Add(new GroupWithSchedule(group, schedule));
        _groupList.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name, UniqueIdSystem uniqueId)
    {
        var student = new Student(group, name, uniqueId.CreateNewUniqueId());
        _studentOrganization[student] = new StudentSchedule(_groupsWithSchedule.Find(p => p.CurGroup.Groupname.GroupNameString == group.Groupname.GroupNameString) ?? throw new InvalidOperationException(), student);
        return student;
    }

    public OGNP AddOgnp(GroupName name, Schedule schedule)
    {
        OGNP ognp = new OGNP(name, schedule);
        _ognpGroups.Add(ognp);
        return ognp;
    }

    public void AddStudentToOGNP(OGNP ognp, Student student)
    {
        ognp.AddStudentToOGNP(student, _studentOrganization[student].GroupWithSchedule.Schedule);
        _studentOrganization[student].AddOGNPCourseToStudent(ognp);
    }

    public void RemoveStudentFromOGNP(OGNP ognp, Student student)
    {
        ognp.DeleteStudentFromOGNP(student);
        _studentOrganization[student].DeleteStudentFromOgnp(ognp);
    }

    public List<OGNP> GetStudentsOGNP(Student student)
    {
        List<OGNP> ognps = new List<OGNP>();
        OGNP? ognp1 = _studentOrganization[student].Ognp1;
        OGNP? ognp2 = _studentOrganization[student].Ognp2;
        if (ognp1 != null)
        {
            ognps.Add(ognp1);
            if (ognp2 != null)
                ognps.Add(ognp2);
        }

        return ognps;
    }

    public List<Student> GetAllFromMegaFacultyOGNP(char megaFaculty)
    {
        List<OGNP> groups = new List<OGNP>();
        List<Student> students = new List<Student>();
        groups.AddRange(_ognpGroups.FindAll(p => p.MegaFaculty == megaFaculty));
        students.AddRange(groups.SelectMany(p => p.CurGroup.StudentList.ToList()));
        return students;
    }

    public List<Student> GetStudentsFromOGNPGroup(OGNP ognp)
    {
        return ognp.CurGroup.StudentList.ToList();
    }

    public List<Student> GetAllWhoAreNotInBothOGNPCources()
    {
        List<Student> students = new List<Student>();
        foreach (Student student in _studentOrganization.Keys)
        {
            if (!_studentOrganization[student].IsStudentGotIntoBothOGNPs())
            {
                students.Add(student);
            }
        }

        return students;
    }
}