using CustomExceptions;
using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Models;

public class Lecture
{
    private const int LowerTimeBound = 8;
    private const int HighTimeBound = 20;
    private const int LowerClassBound = 0;
    private const int AmountOfAuditories = 1000;
    private TimeOnly _time;
    private GroupName _group;
    private string _teacher;
    private int _classNumber;

    public Lecture(TimeOnly time, GroupName group, string teacher, int classNumber)
    {
        if (time.Hour < LowerTimeBound || time.Hour > HighTimeBound)
            throw new NotCorrectTimeException();
        _time = time;
        _group = group;
        _teacher = teacher;
        _classNumber = classNumber;
    }

    public TimeOnly Time
    {
        get => _time;
        private set => _time = value;
    }

    public GroupName Group
    {
        get => _group;
        private set => _group = value;
    }

    public string Teacher
    {
        get => _teacher;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NotCorrectTeacherSurnameException();
            _teacher = value;
        }
    }

    public int ClassNumber
    {
        get => _classNumber;
        private set
        {
            if (value < LowerClassBound | value > AmountOfAuditories)
                throw new NotCorrectClassNumberException();
            _classNumber = value;
        }
    }

    public TimeOnly NextLectureTime()
    {
        return new TimeOnly(Time.Hour + 1, Time.Minute + 30);
    }
}