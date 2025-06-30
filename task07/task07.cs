using System.Reflection;

namespace task07;

public class DisplayNameAttribute : Attribute
{
    public string DisplayName = string.Empty;

    public DisplayNameAttribute(string display_name)
    {
        DisplayName = display_name;
    }
}   

public class VersionAttribute : Attribute
{
    public int Major;
    public int Minor;

    public VersionAttribute(string version)
    {
        string[] version_split = version.Split('.');
        Major = Convert.ToInt32(version_split[0]);
        Minor = Convert.ToInt32(version_split[1]); 
    }
}

[DisplayName("Пример класса")]
[Version("1.0")]
public class SampleClass
{
    [DisplayName("Числовое свойство")]
    public int Number { get; }


    [DisplayName("Тестовый метод")]
    public void TestMethod() { }
}

public class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var display_name_attribute = type.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault();
        if (display_name_attribute != null)
        {
            Console.WriteLine(display_name_attribute.DisplayName);
        }

        var version_attribute = type.GetCustomAttributes<VersionAttribute>().FirstOrDefault();
        if (version_attribute != null)
        {
            Console.WriteLine("{0}.{1}", version_attribute.Major, version_attribute.Minor);
        }

        var get_methods_with_display_name = type
            .GetMethods()
            .Where(method => method.GetCustomAttributes<DisplayNameAttribute>().Any())
            .Select(method => method.Name);

        foreach(var method in get_methods_with_display_name)
        {
            Console.WriteLine(method);
        }

        var get_properties_with_display_name = type
                .GetProperties()
                .Where(property => property.GetCustomAttributes<DisplayNameAttribute>().Any())
                .Select(property => property.Name);
                
        foreach(var property in get_properties_with_display_name)
        {
            Console.WriteLine(property);
        }
    }
}
