using task05;
using Xunit;

public class TestClass
{
    public int PublicField;
    private string _privateField = string.Empty;        
    public int Property { get; set; }

    public void Method() { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var result = analyzer.GetMethodParams(nameof(TestClass.Method));

        Assert.Empty(result);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var result = analyzer.GetProperties();

        Assert.Contains("Property", result);
    }
    

    [Fact]
    public void HasAttribute_ReturnsCorrectAnswer()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        var result = analyzer.HasAttribute<SerializableAttribute>();

        Assert.True(result);
    }
}
