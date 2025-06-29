using System.Reflection.Metadata;
using System.Runtime.Serialization;
using Xunit;

public class TestClass
{
    public int PublicField;
    private string _privateField = string.Empty;
    public int Property { get; set; }

    public void Method() { }
    public void MethodCheckParams(string name, int year) { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods().ToList();

        Assert.Contains("Method", methods);
        Assert.Contains("MethodCheckParams", methods);
    }

    [Fact]
    public void GetMethodParams_MethodCheckParamsReturnCorrectParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var parametrs = analyzer.GetMethodParams("MethodCheckParams").ToList();

        Assert.Equal(2, parametrs.Count);
        Assert.Contains("name String", parametrs);
        Assert.Contains("year Int32", parametrs);
    }

    [Fact]
    public void GetMethodParams_MethodReturnEmptyString()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var parametrs = analyzer.GetMethodParams("Method").ToList();
        var parametrsNonExistentMethod = analyzer.GetMethodParams("Method123").ToList();

        var expected = Enumerable.Empty<string>();
        Assert.Equal(expected, parametrs);
        Assert.Equal(expected, parametrsNonExistentMethod);
    }

    [Fact]
    public void GetAllFields_IncludesPublicFieldsAndPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields().ToList();

        Assert.Equal(2, fields.Count);
        Assert.Contains("PublicField", fields);
        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_IncludesProperty()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties().ToList();

        Assert.Single(properties);
        Assert.Equal("Property", properties.First());
    }

    [Fact]
    public void HasAttribute_ReturnTrue()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        bool hasattribute = analyzer.HasAttribute<SerializableAttribute>();
        Assert.True(hasattribute);
    }
}
