using System.Reflection;

class task09
{
    static void Main(string[] path)
    {
        if (path.Length == 0)
        {
            Console.WriteLine("Укажите путь к .dll");
        }

        string dll_path = path[0];

        if (!File.Exists(dll_path))
        {
            Console.WriteLine("Файл не найден");
        }


        Assembly assembly = Assembly.LoadFrom(dll_path);     

         foreach (Type type in assembly.GetTypes())
        {
            Console.WriteLine($"Класс: {type.FullName}");

            var class_attributes = type.GetCustomAttributes();
            foreach (var attribute in class_attributes)
            {
                Console.WriteLine($"Атрибут класса: {attribute.GetType().Name}");
            }

            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                Console.WriteLine($"Конструктор:");
                foreach (var parameter in constructor.GetParameters())
                {
                    Console.WriteLine($"Имя параметра: {parameter.Name}, тип параметра: {parameter.ParameterType.Name}");
                }
            }

            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine($"Метод: {method.Name}");

                foreach (var parametr in method.GetParameters())
                {
                    Console.WriteLine($"Имя параметра: {parametr.Name}, тип параметра: {parametr.ParameterType.Name}");
                }
            }

            Console.WriteLine();
        }  
    }
}
