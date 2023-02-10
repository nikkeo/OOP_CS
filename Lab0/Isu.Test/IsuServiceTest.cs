using CustomExceptions;
using Isu.Entities;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        UniqueIdSystem uniqueId = new UniqueIdSystem();
        GroupName groupName = new GroupName("M3106");
        Group group = new Group(groupName);
        Student student = new Student(group, "Arnold", uniqueId.CreateNewUniqueId());

        Assert.True(student.GroupName.Equals(groupName) && group.StudentList.Find(p => p.UniqueId == student.UniqueId) != null, "Error, student is not in group");
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        UniqueIdSystem uniqueId = new UniqueIdSystem();
        GroupName groupName = new GroupName("M3106");
        Group group = new Group(groupName);
        try
        {
            for (int i = 0; i < (group.MaxGroupCapacity + 1); ++i)
            {
                var student = new Student(group, "Arnold", uniqueId.CreateNewUniqueId());
            }
        }
        catch (GroupIsOverloadedException)
        {
            Assert.True(true);
            return;
        }

        Assert.True(false, "Exception not working");
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        bool exception = false;

        // Invalid Faculty
        try
        {
            UniqueIdSystem uniqueId = new UniqueIdSystem();
            GroupName groupName = new GroupName("M0106");
            Group group = new Group(groupName);
        }
        catch (CustomExceptions.NotValidFacultyInfoException)
        {
            exception = true;
        }

        if (!exception)
        {
            throw new MissedException("Not valid faculty exception missed");
        }

        // Invalid Course Number
        exception = false;
        try
        {
            UniqueIdSystem uniqueId = new UniqueIdSystem();
            GroupName groupName = new GroupName("M3806");
            Group group = new Group(groupName);
        }
        catch (CustomExceptions.NotValidCourseNumberException)
        {
            exception = true;
        }

        if (!exception)
        {
            throw new MissedException("Not valid course number exception missed");
        }

        // Invalid Group Name
        exception = false;
        try
        {
            UniqueIdSystem uniqueId = new UniqueIdSystem();
            GroupName groupName = new GroupName("M31060");
            Group group = new Group(groupName);
        }
        catch (CustomExceptions.NotValidGroupNameException)
        {
            exception = true;
        }

        if (!exception)
        {
            throw new MissedException("Not valid group name exception missed");
        }
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        UniqueIdSystem uniqueId = new UniqueIdSystem();
        GroupName groupName1 = new GroupName("M3106");
        GroupName groupName2 = new GroupName("M3107");
        Group group1 = new Group(groupName1);
        Group group2 = new Group(groupName2);
        Student student = new Student(group1, "Arnold", uniqueId.CreateNewUniqueId());

        student.ChangeGroup(group2);

        Assert.True(student.GroupName.Equals(groupName2) && group2.IsStudentInGroup(student.UniqueId), "Error, student haven`t changed group");
    }
}