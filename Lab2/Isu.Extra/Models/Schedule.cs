using System.Collections.Immutable;
using CustomExceptions;
using Isu.Models;

namespace Isu.Extra.Models;

public class Schedule
{
    private List<Days> _days = new List<Days>();

    public Schedule()
    {
        _days.Add(new Days(DaysOfTheWeek.Monday));
        _days.Add(new Days(DaysOfTheWeek.Tuesday));
        _days.Add(new Days(DaysOfTheWeek.Wednesday));
        _days.Add(new Days(DaysOfTheWeek.Thursday));
        _days.Add(new Days(DaysOfTheWeek.Friday));
        _days.Add(new Days(DaysOfTheWeek.Saturday));
    }

    public ImmutableList<Days> WeekSchedule { get => _days.ToImmutableList(); }

    public int FindIndexOfTheDay(DaysOfTheWeek dayOfTheWeek)
    {
        return _days.FindIndex(p => p.Day == dayOfTheWeek);
    }

    public void AddLectures(DaysOfTheWeek dayOfTheWeek, List<Lecture> lectures)
    {
        _days[FindIndexOfTheDay(dayOfTheWeek)].AddLectures(lectures);
    }

    public void RemoveLectures(DaysOfTheWeek dayOfTheWeek, List<Lecture> lectures)
    {
        _days[FindIndexOfTheDay(dayOfTheWeek)].RemoveLectures(lectures);
    }

    public bool HasSamePairsAtTheSameTime(DaysOfTheWeek dayOfTheWeek, Schedule weekSchedule)
    {
        foreach (Lecture lecture in weekSchedule.WeekSchedule[weekSchedule.FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule)
        {
            foreach (Lecture lecture2 in WeekSchedule[FindIndexOfTheDay(dayOfTheWeek)].WeekSchedule)
            {
                if (lecture.Time == lecture2.Time)
                    return true;
                if (lecture.Time < lecture2.Time
                     && lecture.NextLectureTime() > lecture2.Time)
                    return true;
                if (lecture2.Time < lecture.Time
                     && lecture2.NextLectureTime() > lecture.Time)
                    return true;
            }
        }

        return false;
    }

    public bool HasSamePairsAtTheSameTime(Schedule weekSchedule)
    {
        return weekSchedule.WeekSchedule.Exists(p => HasSamePairsAtTheSameTime(p.Day, weekSchedule));
    }
}