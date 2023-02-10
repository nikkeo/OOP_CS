using System.Text.RegularExpressions;
using CustomExceptions;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;
using Group = Isu.Entities.Group;
using TimeOnly = System.TimeOnly;

namespace Isu.Exra.Test;

public class IsuServiceTest
{
    [Fact]
    public void CantAddStudentToOgnpCheck_ThrowException()
    {
        IsuServiceAddonRealization newIsuServiceAddonRealization = new IsuServiceAddonRealization();
        UniqueIdSystem uniqueIdSystem = new UniqueIdSystem();
        GroupName groupName = new GroupName("M3106");
        GroupName groupName2 = new GroupName("Q3136");
        TimeOnly time = new TimeOnly(8, 20);
        Lecture lecture = new Lecture(time, groupName, "Tim", 10);
        List<Lecture> lectures = new List<Lecture>();
        List<Lecture> lectures2 = new List<Lecture>();
        List<Lecture> lectures3 = new List<Lecture>();
        List<Lecture> lectures4 = new List<Lecture>();
        lectures.Add(lecture);
        Schedule schedule = new Schedule();
        schedule.AddLectures(DaysOfTheWeek.Monday, lectures);
        Group group = newIsuServiceAddonRealization.AddGroup(groupName, schedule);
        Student student = newIsuServiceAddonRealization.AddStudent(group, "Arnold", uniqueIdSystem);
        OGNP ognp = new OGNP(groupName2, schedule);
        Assert.Throws<AlreadyHavePairsPreventingOGNPException>(() => newIsuServiceAddonRealization.AddStudentToOGNP(ognp, student));

        Lecture lecture2 = new Lecture(new TimeOnly(10, 0), groupName2, "tim", 10);
        lectures2.Add(lecture2);
        Lecture lecture3 = new Lecture(new TimeOnly(11, 40), groupName2, "tim", 10);
        lectures3.Add(lecture3);
        Lecture lecture4 = new Lecture(new TimeOnly(13, 30), groupName2, "tim", 10);
        lectures4.Add(lecture4);
        Schedule schedule2 = new Schedule();
        schedule2.AddLectures(DaysOfTheWeek.Monday, lectures2);
        Schedule schedule3 = new Schedule();
        schedule3.AddLectures(DaysOfTheWeek.Monday, lectures3);
        Schedule schedule4 = new Schedule();
        schedule4.AddLectures(DaysOfTheWeek.Monday, lectures4);
        OGNP ognp2 = new OGNP(groupName2, schedule2);
        OGNP ognp3 = new OGNP(groupName2, schedule3);
        OGNP ognp4 = new OGNP(groupName2, schedule4);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp2, student);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp3, student);
        Assert.Throws<StudentHasMaximumOfAmountOGNPsException>(() => newIsuServiceAddonRealization.AddStudentToOGNP(ognp4, student));
    }

    [Fact]
    public void StudentJoinedOGNP_OGNPHasStudent()
    {
        IsuServiceAddonRealization newIsuServiceAddonRealization = new IsuServiceAddonRealization();
        UniqueIdSystem uniqueIdSystem = new UniqueIdSystem();
        GroupName groupName = new GroupName("M3106");
        GroupName groupName2 = new GroupName("Q3136");
        TimeOnly time = new TimeOnly(8, 20);
        Lecture lecture = new Lecture(time, groupName, "Tim", 10);
        Lecture lecture2 = new Lecture(new TimeOnly(10, 0), groupName2, "tim", 10);
        List<Lecture> lectures = new List<Lecture>();
        List<Lecture> lectures2 = new List<Lecture>();
        Schedule schedule = new Schedule();
        schedule.AddLectures(DaysOfTheWeek.Monday, lectures);
        Schedule schedule2 = new Schedule();
        schedule2.AddLectures(DaysOfTheWeek.Monday, lectures2);
        lectures.Add(lecture);
        lectures2.Add(lecture2);
        Group group = newIsuServiceAddonRealization.AddGroup(groupName, schedule2);
        Student student = newIsuServiceAddonRealization.AddStudent(group, "Arnold", uniqueIdSystem);
        OGNP ognp = new OGNP(groupName2, schedule);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp, student);

        Assert.True(
            newIsuServiceAddonRealization.GetStudentsFromOGNPGroup(ognp).Count == 1
                    && newIsuServiceAddonRealization.GetStudentsFromOGNPGroup(ognp)[0] == student
                    && newIsuServiceAddonRealization.GetStudentsOGNP(student).Count == 1
                    && newIsuServiceAddonRealization.GetStudentsOGNP(student)[0] == ognp,
            "Student not in OGNO Group");
    }

    [Fact]
    public void StudentJoinedAndLeftOGNP_NoStudentInOGNOAndStudentDontHaveOGNP()
    {
        IsuServiceAddonRealization newIsuServiceAddonRealization = new IsuServiceAddonRealization();
        UniqueIdSystem uniqueIdSystem = new UniqueIdSystem();
        GroupName groupName = new GroupName("M3106");
        GroupName groupName2 = new GroupName("Q3136");
        TimeOnly time = new TimeOnly(8, 20);
        Lecture lecture = new Lecture(time, groupName, "Tim", 10);
        Lecture lecture2 = new Lecture(new TimeOnly(10, 0), groupName2, "tim", 10);
        List<Lecture> lectures = new List<Lecture>();
        List<Lecture> lectures2 = new List<Lecture>();
        lectures.Add(lecture);
        lectures.Add(lecture2);
        Schedule schedule1 = new Schedule();
        schedule1.AddLectures(DaysOfTheWeek.Monday, lectures);
        Schedule schedule2 = new Schedule();
        schedule2.AddLectures(DaysOfTheWeek.Monday, lectures2);
        Group group = newIsuServiceAddonRealization.AddGroup(groupName, schedule2);
        Student student = newIsuServiceAddonRealization.AddStudent(group, "Arnold", uniqueIdSystem);
        OGNP ognp = new OGNP(groupName2, schedule1);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp, student);
        newIsuServiceAddonRealization.RemoveStudentFromOGNP(ognp, student);

        Assert.True(
            newIsuServiceAddonRealization.GetStudentsFromOGNPGroup(ognp).Count == 0
            && newIsuServiceAddonRealization.GetStudentsOGNP(student).Count == 0,
            "Student not left OGNP group");
    }

    [Fact]
    public void FindAllStudentNotInBothOGNPs_AllStudentWereFound()
    {
        IsuServiceAddonRealization newIsuServiceAddonRealization = new IsuServiceAddonRealization();
        UniqueIdSystem uniqueIdSystem = new UniqueIdSystem();
        GroupName groupName1 = new GroupName("M3106");
        GroupName groupName2 = new GroupName("A3136");
        GroupName groupName3 = new GroupName("Q3104");
        GroupName groupName4 = new GroupName("B3112");
        TimeOnly time = new TimeOnly(8, 20);
        List<Lecture> lectures = new List<Lecture>();
        List<Lecture> lectures2 = new List<Lecture>();
        List<Lecture> lectures3 = new List<Lecture>();
        List<Lecture> lectures4 = new List<Lecture>();
        Lecture lecture = new Lecture(time, groupName1, "Tim", 10);
        Lecture lecture2 = new Lecture(new TimeOnly(10, 0), groupName2, "tim", 10);
        Lecture lecture3 = new Lecture(new TimeOnly(11, 40), groupName2, "tim", 10);
        Lecture lecture4 = new Lecture(new TimeOnly(13, 30), groupName2, "tim", 10);
        lectures.Add(lecture);
        lectures2.Add(lecture2);
        lectures3.Add(lecture3);
        lectures4.Add(lecture4);
        Schedule schedule1 = new Schedule();
        schedule1.AddLectures(DaysOfTheWeek.Monday, lectures);
        Schedule schedule2 = new Schedule();
        schedule2.AddLectures(DaysOfTheWeek.Monday, lectures2);
        Schedule schedule3 = new Schedule();
        schedule3.AddLectures(DaysOfTheWeek.Monday, lectures3);
        Schedule schedule4 = new Schedule();
        schedule4.AddLectures(DaysOfTheWeek.Monday, lectures4);
        Group group1 = newIsuServiceAddonRealization.AddGroup(groupName1, schedule1);
        Group group2 = newIsuServiceAddonRealization.AddGroup(groupName3, schedule2);
        Group group3 = newIsuServiceAddonRealization.AddGroup(groupName4, schedule3);
        Student student1 = newIsuServiceAddonRealization.AddStudent(group1, "Arnold", uniqueIdSystem);
        Student student2 = newIsuServiceAddonRealization.AddStudent(group2, "Arnold", uniqueIdSystem);
        Student student3 = newIsuServiceAddonRealization.AddStudent(group3, "Arnold", uniqueIdSystem);
        OGNP ognp1 = new OGNP(groupName1, schedule1);
        OGNP ognp2 = new OGNP(groupName2, schedule2);
        OGNP ognp3 = new OGNP(groupName3, schedule3);
        OGNP ognp4 = new OGNP(groupName4, schedule4);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp2, student1);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp3, student1);
        newIsuServiceAddonRealization.AddStudentToOGNP(ognp1, student2);

        Assert.True(
            newIsuServiceAddonRealization.GetAllWhoAreNotInBothOGNPCources().Contains(student2)
                    && newIsuServiceAddonRealization.GetAllWhoAreNotInBothOGNPCources().Contains(student3),
            "Not All were found");
    }
}