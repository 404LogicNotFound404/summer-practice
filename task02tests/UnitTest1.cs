using Xunit;

public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Faculty == "ФИТ"));
    }

    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(3.75).ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Grades.Average() >= 3.75));
    }

    [Fact]
    public void GetStudentsOrderedByName_ReturnCorrectStudents()
    {
        var result = _service.GetStudentsOrderedByName().Select(s => s.Name).ToList();
        var expected = new[] {"Анна", "Иван", "Петр"};
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GroupStudentsByFaculty_ReturnCorrectStudents()
    {
        var result = _service.GroupStudentsByFaculty();
        Assert.Equal(2, result.Count);

        var fitStud = result["ФИТ"].Select(s => s.Name).ToList();
        var fitExpected = new[] { "Иван", "Анна"};
        Assert.Equal(fitExpected, fitStud);
        
        var ecoStud = result["Экономика"].Select(s => s.Name).ToList();
        var ecoExpected = new[] { "Петр" };
        Assert.Equal(ecoExpected, ecoStud);
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.Equal("Экономика", result);
    }
}
