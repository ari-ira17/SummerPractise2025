using System.Reflection;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()       
    {
        return _type
                .GetMethods()
                .Where(member => member.IsPublic)
                .Select(member => member.Name);
    }

    public IEnumerable<string> GetMethodParams(string methodname)      
    {
        return _type
                .GetMethod(methodname)?
                .GetParameters()
                .Select(member => member.Name)!;
    }

    public IEnumerable<string> GetAllFields()     
    {
        return _type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Select(member => member.Name);
    }

    public IEnumerable<string> GetProperties()  
    {
        return _type
                .GetProperties()
                .Select(member => member.Name);
    }

    public bool HasAttribute<T>() where T : Attribute       
    {
        return _type
                .GetCustomAttribute<T>() != null;
    }
}
