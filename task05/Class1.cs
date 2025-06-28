using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
        => _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(s => s.Name);
    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var metods = _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                         .Where(s => s.Name == methodname)
                         .ToList();
        if (metods.Any())
        {
            var metod = metods.First();
            if (metod.GetParameters().Length !=  0)
            {
                return metod.GetParameters().Select(s => $"{s.Name} {s.ParameterType.Name}");
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
        else
        {
            return Enumerable.Empty<string>();
        }
    }

    public IEnumerable<string> GetAllFields() 
        => _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(s => !s.Name.StartsWith("<"))
                .Select(s => s.Name);

    public IEnumerable<string> GetProperties()
        => _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(s => s.Name);

    public bool HasAttribute<T>() where T : Attribute 
        => Attribute.IsDefined(_type, typeof(T));
}
