using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;


namespace task13
{
    public class Subject
    {
        public required string Name { get; set; }
        public int Grade { get; set; }
    }

    public class Student
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [JsonIgnore]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("BirthDate")]
        public string BirthDateString
        {
            get => BirthDate.ToString("dd.MM.yyyy");
            set => BirthDate = DateTime.ParseExact(value, "dd.MM.yyyy", null);
        }
        public List<Subject> ?Grades { get; set; }
    }

    public class Serializer
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        public string SerializationStudent(Student student)
            => JsonSerializer.Serialize(student, options);

        public Student DeserializationStudent(string JsonString)
            => JsonSerializer.Deserialize<Student>(JsonString, options)!;

        public void SaveFileJson(Student student, string Path)
            => File.WriteAllText(Path, SerializationStudent(student));

        public Student LoadFileJson(string Path)
            => DeserializationStudent(File.ReadAllText(Path));
    }
}
