using CustomExceptions;
using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class StudentSchedule
{
    public StudentSchedule(GroupWithSchedule groupWithSchedule, Student student)
    {
        GroupWithSchedule = groupWithSchedule;
        CurStudent = student;
    }

    public GroupWithSchedule GroupWithSchedule { get; }

    public Student CurStudent { get;  }

    public OGNP? Ognp1 { get; private set; }
    public OGNP? Ognp2 { get; private set; }

    public void AddOGNPCourseToStudent(OGNP ognp)
    {
        if (ognp == null)
            throw new NotCorrectOGNPException();
        if (Ognp1 != null && Ognp2 != null)
            throw new StudentHasMaximumOfAmountOGNPsException();
        if (Ognp1 == null)
            Ognp1 = ognp;
        else
            Ognp2 = ognp;
    }

    public void DeleteStudentFromOgnp(OGNP ognp)
    {
        if (ognp != Ognp1 && ognp != Ognp2)
            throw new NotCorrectOGNPException();
        if (ognp == Ognp1)
            Ognp1 = null;
        else
            Ognp2 = null;
    }

    public bool IsStudentGotIntoBothOGNPs()
    {
        return Ognp1 != null && Ognp2 != null;
    }
}