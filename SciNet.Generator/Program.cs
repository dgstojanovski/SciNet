using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SciNet;
using SciNet.Mathematics;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;

namespace SciNet.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var loaded = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                .GetFiles()
                .Where(file => file.Name.StartsWith(nameof(SciNet)) && file.Extension == ".dll")
                .Select(file => AppDomain.CurrentDomain.Load(File.ReadAllBytes(file.FullName)))
                .ToArray();
            
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(nameof(SciNet)))
                .ToArray();
            
            var factories = types
                .Where(type => type.CustomAttributes
                    .Any(attribute => attribute.AttributeType.FullName == typeof(FactoryAttribute).FullName))
                .ToArray();
            
            var valueTypes = types
                .Where(type => type.CustomAttributes
                    .Any(attribute => attribute.AttributeType.FullName == typeof(ValueTypeAttribute).FullName))
                .ToArray();
            
            Console.WriteLine($"Exported types: {types.Length}");
            Console.WriteLine($"Factory types: {factories.Length}");
            Console.WriteLine($"Value types: {valueTypes.Length}");
        }
    }
}
