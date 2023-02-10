namespace CustomExceptions;

public class GroupNameIsNotCorrectException : Exception
{
    public GroupNameIsNotCorrectException() { }
    public GroupNameIsNotCorrectException(string message)
        : base(message) { }
    public GroupNameIsNotCorrectException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotValidGroupNameException : Exception
{
    public NotValidGroupNameException() { }
    public NotValidGroupNameException(string message)
        : base(message) { }
    public NotValidGroupNameException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotValidCourseNumberException : Exception
{
    public NotValidCourseNumberException() { }
    public NotValidCourseNumberException(string message)
        : base(message) { }
    public NotValidCourseNumberException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotValidFacultyInfoException : Exception
{
    public NotValidFacultyInfoException() { }
    public NotValidFacultyInfoException(string message)
        : base(message) { }
    public NotValidFacultyInfoException(string message, Exception inner)
        : base(message, inner) { }
}

public class NotValidStudentNameException : Exception
{
    public NotValidStudentNameException() { }
    public NotValidStudentNameException(string message)
        : base(message) { }
    public NotValidStudentNameException(string message, Exception inner)
        : base(message, inner) { }
}