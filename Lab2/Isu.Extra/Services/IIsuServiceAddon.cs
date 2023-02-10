using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuServiceAddon
{
    OGNP AddOgnp(GroupName name, Schedule schedule);

    void AddStudentToOGNP(OGNP ognp, Student student);

    void RemoveStudentFromOGNP(OGNP ognp, Student student);

    List<Student> GetAllFromMegaFacultyOGNP(char megaFaculty);

    List<Student> GetStudentsFromOGNPGroup(OGNP ognp);

    List<Student> GetAllWhoAreNotInBothOGNPCources();
}