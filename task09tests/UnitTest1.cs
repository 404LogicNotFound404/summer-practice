namespace task09tests
{
    public class UnitTest1
    {
        [Fact]
        public void Task09Main_OutputVerification()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            string ex = Environment.NewLine; ;

            string expected =  $"Класс: DisplayNameAttribute{ex}" +
                $"{ex}" +
                $"Конструкторы класса DisplayNameAttribute{ex}" +
                $"Конструктор DisplayNameAttribute{ex}" +
                $"Параметры конструктора DisplayNameAttribute{ex}" +
                $"Параметр: name тип: String{ex}" +
                $"{ex}" +
                $"Класс: VersionAttribute{ex}" +
                $"{ex}" +
                $"Методы класса VersionAttribute{ex}" +
                $"Метод ToString{ex}" +
                $"{ex}" +
                $"Конструкторы класса VersionAttribute{ex}" +
                $"Конструктор VersionAttribute{ex}" +
                $"Параметры конструктора VersionAttribute{ex}" +
                $"Параметр: major тип: Int32{ex}" +
                $"Параметр: minor тип: Int32{ex}" +
                $"{ex}" +
                $"Класс: SampleClass{ex}" +
                $"{ex}" +
                $"Атрибуты класса SampleClass{ex}" +
                $"DisplayNameAttribute{ex}" +
                $"1.0{ex}" +
                $"{ex}" +
                $"Методы класса SampleClass{ex}" +
                $"Метод TestMethod{ex}" +
                $"Атрибуты метода TestMethod{ex}" +
                $"DisplayNameAttribute{ex}" +
                $"{ex}" +
                $"Конструкторы класса SampleClass{ex}" +
                $"Конструктор SampleClass{ex}" +
                $"{ex}" +
                $"Класс: ReflectionHelper{ex}" +
                $"{ex}" +
                $"Методы класса ReflectionHelper{ex}" +
                $"Метод PrintTypeInfo{ex}" +
                $"Параметры метода PrintTypeInfo{ex}" +
                $"Параметр: type тип: Type{ex}{ex}";

            Program.Main(new[] { "task07.dll" });
            Assert.Equal(expected, output.ToString());
        }
    }
}
