using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Project;
public sealed class Student :
    IEquatable<Student>,
    IEnumerable

{
    private readonly string _name;
    private readonly string _surname;
    private readonly string _patronymic;
    public readonly string _group;
    private readonly int _course;
    private readonly string _recordBookNumber;
    public Student(
        string name,
        string surname,
        string patronymic,
        string group,
        int course,
        string recordBookNumber)
    {
        List<Exception> exceptions = new List<Exception>();

        try
        {
            Name = name;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            Surname = surname;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            Patronymic = patronymic;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            Group = group;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            Course = course;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            RecordBookNumber = recordBookNumber;
        }
        catch (ArgumentException ex)
        {
            exceptions.Add(ex);
        }

        if (exceptions.Count == 0)
        {
            return;
        }

        if (exceptions.Count == 1)
        {
            throw exceptions[0];
        }

        throw new AggregateException(exceptions);
    }
    public string Name
    {
        get =>
            _name;

        private init =>
            _name = ThrowIfNullOrEmpty(value);
    }

    public string Surname
    {
        get =>
            _surname;

        private init =>
            _surname = ThrowIfNullOrEmpty(value);
    }

    public string Patronymic
    {
        get =>
            _patronymic;

        private init
        {
            ThrowIfNull(value);
            _patronymic = value;
        }
    }

    public string Group
    {

        get =>
           _group;

        private init =>
            _group = ThrowIfNullOrEmpty(value);
    }
    public int Course
    {
        get =>
            _course;

        private init
        {
            
            if (value < 1 || value > 4)
            {
                throw new ArgumentException("Course must be GT or EQ 1 AND LT or EQ 4.", nameof(value));
            }

            _course = value;
        }
    }

    public string RecordBookNumber
    {
        get =>
            _recordBookNumber;

        private init =>
            _recordBookNumber = ThrowIfNullOrEmpty(value, nameof(RecordBookNumber));
    }
    private string ThrowIfNullOrEmpty(
        string @string,
        [CallerMemberName] string? paramName = null)
    {
        this.ThrowIfNull(@string, paramName)
            .ThrowIfEmpty(@string, paramName);
        return @string;
    }
    private Student ThrowIfNull(
        string @string,
        [CallerMemberName] string? paramName = null)
    {
        if (@string is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return this;
    }
    private Student ThrowIfEmpty(
        string @string,
        [CallerMemberName] string? paramName = null)
    {
        if (@string.Equals(string.Empty))
        {
            throw new ArgumentException("Name can't be an empty string.", paramName);
        }

        return this;
    }
    public override string ToString()
    {
        return $"Name: {_name}, Surname: {_surname}, Group: {_group}, Course: {_course}";
    }
    public override int GetHashCode()
    {
        var result = new HashCode();
        result.Add(_name);
        result.Add(_surname);
        result.Add(_patronymic);
        result.Add(_group);
        result.Add(_course);
        result.Add(_recordBookNumber);
        return result.ToHashCode();
    }
    public bool Equals(
        object? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other is Student student)
        {
            return Equals(student);
        }

        return false;
    }
    public bool Equals(
        Student? student)
    {
        if (student is null)
        {
            return false;
        }

        return _name.Equals(student._name)
               && _surname.Equals(student._surname)
               && _patronymic.Equals(student._patronymic)
               && _group.Equals(student._group)
               && _course.Equals(student._course)
               && _recordBookNumber.Equals(student._recordBookNumber);
    }
    public IEnumerator GetEnumerator()
    {
        yield return _name;
        yield return _surname;
        yield return _patronymic;
        yield return _group;
        yield return _course;
        yield return _recordBookNumber;
    }

}


class Progaram
    {
        static void Main()
        {
            Student student1 = new Student("Ivan", "Ivanov","Ivanovich", "101", 2 ,"12345");
            Student student2 = new Student("Petr", "Petrov","Petrovich", "102", 1, "54321");
            Console.WriteLine(student1);
            Console.WriteLine(student2._group);
            Console.WriteLine($"Students are equal: {student1.Equals(student2)}");
    }
        
    }
   
