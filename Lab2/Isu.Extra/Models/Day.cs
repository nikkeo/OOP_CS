using System.Collections.Immutable;

namespace Isu.Extra.Models;

public class Days
{
    private List<Lecture> _lectures = new List<Lecture>();

    public Days(DaysOfTheWeek day)
    {
        Day = day;
    }

    public DaysOfTheWeek Day { get; }

    public ImmutableList<Lecture> WeekSchedule { get => _lectures.ToImmutableList(); }

    public void AddLectures(List<Lecture> lectures)
    {
        _lectures.AddRange(lectures);
    }

    public void RemoveLectures(List<Lecture> lectures)
    {
        lectures.ForEach(p => _lectures.Remove(p));
    }
}