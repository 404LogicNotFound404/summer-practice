using System.ComponentModel;
using System.Reflection;
using Xunit;

public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = method?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = prop?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }

    [Fact]
    public void ReflectionHelperPrintTypeInfo_CheckCorrectOutput() 
    {
        var output = new StringWriter();
        Console.SetOut(output);

        string expected = "Пример класса\r\n1.0\r\nTestMethod Тестовый метод\r\nNumber Числовое свойство\r\n";

        ReflectionHelper.PrintTypeInfo(typeof(SampleClass));
        Assert.Equal(expected, output.ToString());
    }
}
