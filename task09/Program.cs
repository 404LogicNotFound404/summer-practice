using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        string dll = args[0];

        Assembly assembly = Assembly.LoadFrom(dll);

        var AllClass = assembly.GetTypes()
                               .Where(s => s.IsClass && !s.Name.Contains("<"))
                               .ToList();

        foreach(Type type in AllClass)
        {
            Console.WriteLine($"Класс: {type.FullName}");

            var AllClassAttribute = type.GetCustomAttributes()
                                        .Where(s => s.GetType().Namespace == null || !s.GetType().Namespace!.StartsWith("System"))
                                        .ToList();

            if (AllClassAttribute.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Атрибуты класса {type}");
                AllClassAttribute.ForEach(s => Console.WriteLine(s));
            }

            var AllMethods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                 .Where(s => !s.IsSpecialName)
                                 .ToList();

            if (AllMethods.Count > 0) 
            {
                Console.WriteLine();
                Console.WriteLine($"Методы класса {type}");
                foreach (MethodInfo method in AllMethods) 
                {
                    Console.WriteLine($"Метод {method.Name}");

                    var AllParameters = method.GetParameters().ToList();
                    if (AllParameters.Count > 0)
                    {
                        Console.WriteLine($"Параметры метода {method.Name}");
                        foreach (ParameterInfo parameter in AllParameters) 
                        {
                            Console.WriteLine($"Параметр: {parameter.Name} тип: {parameter.ParameterType.Name}");
                        }
                    }

                    var AllAttributesMethod = method.GetCustomAttributes()
                                                    .Where(s => s.GetType().Namespace == null || !s.GetType().Namespace!.StartsWith("System"))
                                                    .ToList();
                    if (AllAttributesMethod.Count > 0)
                    {
                        Console.WriteLine($"Атрибуты метода {method.Name}");
                        AllAttributesMethod.ForEach(s => Console.WriteLine(s));
                    }
                }
            }

            var AllConstructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();
            if (AllConstructors.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Конструкторы класса {type}");
                foreach (ConstructorInfo constructor in AllConstructors)
                {
                    Console.WriteLine($"Конструктор {type.Name}");

                    var AllParameters = constructor.GetParameters().ToList();
                    if (AllParameters.Count > 0)
                    {
                        Console.WriteLine($"Параметры конструктора {type.Name}");
                        foreach (ParameterInfo parameter in AllParameters)
                        {
                            Console.WriteLine($"Параметр: {parameter.Name} тип: {parameter.ParameterType.Name}");
                        }
                    }

                    var AllAttributesConstructor = constructor.GetCustomAttributes()
                                                              .Where(s => s.GetType().Namespace == null || !s.GetType().Namespace!.StartsWith("System"))
                                                              .ToList();
                    if (AllAttributesConstructor.Count > 0)
                    {
                        Console.WriteLine($"Атрибуты метода {constructor.Name}");
                        AllAttributesConstructor.ForEach(s => Console.WriteLine(s));
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
