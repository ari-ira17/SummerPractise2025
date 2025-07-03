using System.Reflection;
using Plugins;

namespace PluginLoader;

public class Loader
{
    private string _path = String.Empty;
    public Loader(string plugins_folder_path)
    {
        _path = plugins_folder_path;
    }

    public void FindPluginLoaderAndLoad()
    {
        var plugin_types = new List<Type>();
        
        foreach (var dll_path in Directory.GetFiles(_path, "*.dll"))
        {
            Assembly assembly = Assembly.LoadFrom(dll_path);

            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoad>() != null)
                {
                    plugin_types.Add(type);
                }
            }   
        }

        var plugins = plugin_types
            .Select(type => new 
        { 
            Type = type, 
            Name = type.Name, 
            DependsOn = type.GetCustomAttribute<PluginLoad>()?.DependsOn 
        })  .ToList();

        var loaded = new HashSet<string>();

        while (loaded.Count < plugins.Count)
        {
            foreach (var plugin in plugins)
            {
                if (loaded.Contains(plugin.Name))
                    continue;

                if (plugin.DependsOn.All(loaded.Contains))
                {
                    if (Activator.CreateInstance(plugin.Type) is IPlugin pluginInstance)
                    {
                        pluginInstance.Execute();
                        loaded.Add(plugin.Name);
                    }
                }
            }  
        }
    }
}
