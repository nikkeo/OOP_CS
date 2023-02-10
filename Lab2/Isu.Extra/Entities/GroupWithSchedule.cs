using System.Collections.Immutable;
using System.Reflection;
using CustomExceptions;
using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class GroupWithSchedule
{
    public GroupWithSchedule(Group group, Schedule schedule)
    {
        CurGroup = group;
        Schedule = schedule;
    }

    public Group CurGroup { get; }
    public Schedule Schedule { get; }
    public List<Lecture> GetList(DaysOfTheWeek dayOfTheWeek)
    {
        return Schedule.WeekSchedule[Schedule.FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule.ToList();
    }

    public void AddPairToGroup(DaysOfTheWeek dayOfTheWeek, List<Lecture> lecture)
    {
        if (Schedule.WeekSchedule[Schedule.FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule.All(p => lecture.Find(pa => pa.Time == p.Time) == null))
            throw new TimeOfPairAlreadyClaimedException();

        Schedule.AddLectures(dayOfTheWeek, lecture);
    }
}