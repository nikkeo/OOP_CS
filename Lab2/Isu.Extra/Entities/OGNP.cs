using System.Collections.Immutable;
using CustomExceptions;
using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class OGNP
{
    private Schedule _schedule;

    public OGNP(GroupName groupName, Schedule schedule)
    {
        CurGroup = new Group(groupName);
        _schedule = schedule;
        MegaFaculty = groupName.GroupNameString[0];
    }

    public Group CurGroup { get; }

    public char MegaFaculty { get; }

    public List<Lecture> GetList(DaysOfTheWeek dayOfTheWeek)
    {
        return _schedule.WeekSchedule[_schedule.FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule.ToList();
    }

    public void AddPairToGroup(DaysOfTheWeek dayOfTheWeek, List<Lecture> lectures)
    {
        if (_schedule.WeekSchedule[_schedule.FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule.All(p => lectures.Find(pa => pa.Time == p.Time) == null))
            throw new TimeOfPairAlreadyClaimedException();
        _schedule.AddLectures(dayOfTheWeek, lectures);
    }

    public void AddStudentToOGNP(Student student, Schedule schedule)
    {
        if (student.GroupName.GroupNameString[0] == MegaFaculty)
            throw new StudentCantChooseOGNPFromHisFacultyException();
        if (_schedule.HasSamePairsAtTheSameTime(schedule))
            throw new AlreadyHavePairsPreventingOGNPException();
        CurGroup.AddStudent(student);
    }

    public void DeleteStudentFromOGNP(Student student)
    {
        if (!CurGroup.IsStudentInGroup(student.UniqueId))
            throw new NotFoundStudentException();
        CurGroup.DeleteStudent(student);
    }
}