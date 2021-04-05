using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SciNet;
using SciNet.Core;
using SciNet.Generator.Generators;

namespace SciNet.Generator
{
    public static class Program
    {
        public const string WorkingDirectory = "working";
        public const string DocumentationDirectory = "docs";
        public const string SolutionDirectory = "src";
        public static DirectoryInfo Working;
        
        public static void Main(string[] args)
        {
            Working = new DirectoryInfo(args.Length > 0 
                ? args[0] 
                : Path.Combine(Environment.CurrentDirectory, WorkingDirectory));
            
            if (Working.Exists) 
                Working.Delete(true);
            
            Working.Create();
            
            WriteHeading("Types");
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(nameof(SciNet)))
                .ToArray();
            
            var factories = types
                .Where(t => t.CustomAttributes
                    .Any(a => a.AttributeType.Name == nameof(FactoryAttribute)))
                .ToArray();
            
            var values = types
                .Where(t => t.CustomAttributes
                    .Any(a => a.AttributeType.Name == nameof(ValueTypeAttribute)))
                .ToArray();
            
            WriteList("Factories", "Discovered factory types",
                factories.Select(t => t.Name).ToArray());
            WriteList("Values", "Discovered value types",
                values.Select(t => t.Name).ToArray());
            
            WriteHeading("Generators");
            var generators = types
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i == typeof(IGenerator)))
                .Select(t => Activator.CreateInstance(t) as IGenerator)
                .ToArray();
            
            WriteList("Factories", "Generated factory files", 
                Execute<FactoryAttribute>(types, generators));
            WriteList("Values", "Generated value type files", 
                Execute<ValueTypeAttribute>(types, generators));
        }

        private static string[] Execute<TDefinition>(IEnumerable<Type> types, params IGenerator[] generators)
            where TDefinition : Attribute
        {
            var files = new List<FileInfo>();
            
            var matchingTypes = types
                .Where(t => t.GetCustomAttributes<TDefinition>().SingleOrDefault() != null)
                .ToArray();
            
            var matchingGenerators = generators
                .Where(g => g.GetType().GetInterfaces()
                    .Any(i => i.GetGenericArguments().Any(t => t == typeof(TDefinition))))
                .ToArray();

            foreach (var generator in matchingGenerators)
            {
                foreach (var type in matchingTypes)
                {
                    try
                    {
                        files.AddRange(generator.Generate(type));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to execute {generator.GetType().Name} for {type.FullName}: {e.Message}");
                    }
                }
            }

            return files
                .Select(f => f.FullName)
                .ToArray();
        }
        
        private static void WriteHeading(string heading)
        {
            Console.WriteLine($"# {heading}");
        }
        
        private static void WriteList(string heading, string subheading, params string[] items)
        {
            Console.WriteLine($"## {heading}");
            Console.WriteLine($"{subheading}:");
            Console.WriteLine($"* {string.Join("\n* ", items)}");
        }
    }
}
