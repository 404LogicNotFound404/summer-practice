using System.Diagnostics;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName;
    public DisplayNameAttribute(string name)
    {
        DisplayName = name;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    public int Major;
    public int Minor;
    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
    public override string ToString() => $"{Major}.{Minor}";
}

[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    [DisplayName("Тестовый метод")]
    public void TestMethod() { }

    [DisplayName("Числовое свойство")]
    public int Number { get; set; }

}

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type) 
    {
        var DisplayNameClass = type.GetCustomAttribute<DisplayNameAttribute>();
        if (DisplayNameClass != null) 
        {
            Console.WriteLine(DisplayNameClass.DisplayName);
        }

        var VersionClass = type.GetCustomAttribute<VersionAttribute>();
        if (VersionClass != null)
        {
            Console.WriteLine(VersionClass.ToString());
        }

        var allMethods = type.GetMethods();
        allMethods.Where(s => s.GetCustomAttribute<DisplayNameAttribute>() != null)
                .Select(s => $"{s.Name} {s.GetCustomAttribute<DisplayNameAttribute>()}")
                .ToList()
                .ForEach(s => Console.WriteLine(s));


        var allProperties = type.GetProperties();
        allProperties.Where(s => s.GetCustomAttribute<DisplayNameAttribute>() != null)
                    .Select(s => $"{s.Name} {s.GetCustomAttribute<DisplayNameAttribute>()}")
                    .ToList()
                    .ForEach(s => Console.WriteLine(s));
    }
}
