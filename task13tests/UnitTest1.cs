using task13;

namespace task13tests
{
    public class UnitTest1
    {
        Student test = new Student
        {
            FirstName = "Андрей",
            LastName = "Россинский",
            BirthDate = new DateTime(2006, 06, 25),
            Grades = new List<Subject> 
            {
                new Subject{Name = "Математика", Grade = 4},
            }
        };

        [Fact]
        public void SerializationStudent_ReturnJson()
        {
            Serializer serializer = new Serializer();
            var json = serializer.SerializationStudent(test);
            Assert.Contains("\"FirstName\":\"Андрей\"", json);
            Assert.Contains("\"LastName\":\"Россинский\"", json);
            Assert.Contains("\"BirthDate\":\"25.06.2006\"", json);
            Assert.Contains("\"Grades\":[{", json);
            Assert.Contains("\"Name\":\"Математика\"", json);
            Assert.Contains("\"Grade\":4", json);
        }

        [Fact]
        public void SerializationStudentCheckNullProperties_ReturnJson()
        {
            Student nullTest = new Student
            {
                FirstName = "Иван",
                LastName = "Сидоров",
                BirthDate = new DateTime(1111, 11, 11),
                Grades = null
            };
            Serializer serializer = new Serializer();
            var json = serializer.SerializationStudent(nullTest);
            Assert.DoesNotContain("Grades", json);
        }

        [Fact]
        public void DeserializationStudent_ReturnStudent()
        {
            Serializer serializer = new Serializer();
            var json = serializer.SerializationStudent(test);

            var TestDeserializationStudent = serializer.DeserializationStudent(json);
            Assert.Equal(test.FirstName, TestDeserializationStudent.FirstName);
            Assert.Equal(test.LastName, TestDeserializationStudent.LastName);
            Assert.Equal(test.BirthDate, TestDeserializationStudent.BirthDate);
            Assert.Equal(test.Grades![0].Name, TestDeserializationStudent.Grades![0].Name);
            Assert.Equal(test.Grades[0].Grade, TestDeserializationStudent.Grades[0].Grade);
        }

        [Fact]
        public void SaveFileJsonAndLoadFileJson_CreateFileWithCorrectJsonAndLoad()
        {
            Serializer serializer = new Serializer();
            string pathTest = "test.json";

            serializer.SaveFileJson(test, pathTest);

            var LoadJson = serializer.LoadFileJson(pathTest);
            Assert.Equal(test.FirstName, LoadJson.FirstName);
            Assert.Equal(test.LastName, LoadJson.LastName);
            Assert.Equal(test.BirthDate, LoadJson.BirthDate);
            Assert.Equal(test.Grades![0].Name, LoadJson.Grades![0].Name);
            Assert.Equal(test.Grades![0].Grade, LoadJson.Grades![0].Grade);
        }
    }
}