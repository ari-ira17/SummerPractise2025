namespace Plugins;      

public interface IPlugin
{
    void Execute();
}

public class PluginLoad : Attribute
{
    public string[] DependsOn = Array.Empty<string>();

    public PluginLoad(string[] depends_on)
    {
        DependsOn = depends_on;
    }
}
