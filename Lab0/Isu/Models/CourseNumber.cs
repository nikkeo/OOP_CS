using CustomExceptions;

namespace Isu.Models;

public class CourseNumber
{
    private int _lowerBound = 1;
    private int _highBound = 4;
    public CourseNumber(int number = 0)
    {
        if (number < _lowerBound || number > _highBound)
        {
            throw new NotValidCourseNumberException("Not valid course number");
        }

        Coursenumber = number;
    }

    public int Coursenumber { get; }
}