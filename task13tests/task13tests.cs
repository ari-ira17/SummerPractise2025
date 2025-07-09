using System.Text.Json;
using System.Text.Json.Serialization;
using task13;

public class task13tests
{
    public JsonSerializerOptions options;
    public task13tests()
    {
        options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        options.Converters.Add(new DateConverter());
    }

    [Fact]
    public void Serialization_ShouldContainCorrectDate_AndIgnoreNulls()
    {
        var student = new Student
            {
                FirstName = "I",
                LastName = null,
                BirthDate = new DateTime(2006, 4, 17),
                Grades = new List<Subject>
                {
                    new Subject { Name = "Math", Grade = 5 },
                    new Subject { Name = "English", Grade = 5 }
                }
            };

        string json = JsonSerializer.Serialize(student, options);  

        Assert.Contains("17.04.2006", json);
        Assert.DoesNotContain("LastName", json);
        Assert.Contains("Math", json);
        Assert.Contains("English", json);
    }

    [Fact]
    public void Deserialization_ShouldParseWithCustomDateFormat()
    {
        var student = new Student
            {
                FirstName = "I",
                LastName = "A",
                BirthDate = new DateTime(2006, 4, 17),
                Grades = null
            };

        string json = JsonSerializer.Serialize(student, options);
        Student? deserialized_student = JsonSerializer.Deserialize<Student>(json, options);

        Assert.Equal(new DateTime(2006, 4, 17), deserialized_student!.BirthDate);
        Assert.Equal("I", deserialized_student.FirstName);
        Assert.Equal("A", deserialized_student.LastName);
        Assert.Null(deserialized_student.Grades);
    }
}
