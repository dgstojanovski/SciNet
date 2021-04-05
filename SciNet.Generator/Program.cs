using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SciNet;
using SciNet.Core;

namespace SciNet.Generator
{
    public static class Program
    {
        internal const string WorkingDirectory = "working";
        internal const string DocumentationDirectory = "docs";
        internal const string SolutionDirectory = "src";
        internal static DirectoryInfo Working;
        
        public static void Main(string[] args)
        {
            Working = new DirectoryInfo(args.Length > 0 
                ? args[0] 
                : Path.Combine(Environment.CurrentDirectory, WorkingDirectory));
            
            if (Working.Exists) 
                Working.Delete(true);
            
            Working.Create();
            
            WriteHeading("Assemblies");
            var loaded = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                .GetFiles()
                .Where(file => file.Name.StartsWith(nameof(SciNet)) && file.Extension == ".dll")
                .Select(file => AppDomain.CurrentDomain.Load(File.ReadAllBytes(file.FullName)))
                .ToArray();
            WriteList("Loaded", "Discovering types in the following assemblies",
                loaded.Select(a => a.GetName().Name).ToArray());
            
            WriteHeading("Types");
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(nameof(SciNet)))
                .ToArray();
            var factories = types
                .Where(t => t.CustomAttributes
                    .Any(a => a.AttributeType.Name == nameof(FactoryAttribute)))
                .ToArray();
            WriteList("Factories", "Discovered factory types",
                factories.Select(t => t.Name).ToArray());
            var values = types
                .Where(t => t.CustomAttributes
                    .Any(a => a.AttributeType.Name == nameof(ValueTypeAttribute)))
                .ToArray();
            WriteList("Values", "Discovered value types",
                values.Select(t => t.Name).ToArray());
            
            WriteHeading("Generators");
            WriteList("Factories", "Generated factory files", 
                Execute<FactoryAttribute>(types, ReadmeGenerator, ControllerGenerator));
            WriteList("Values", "Generated value type files", 
                Execute<ValueTypeAttribute>(types, ReadmeGenerator));
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
       
        private static string[] Execute<TDefinition>(IEnumerable<Type> types, params Func<Type, FileInfo>[] generators)
            where TDefinition : Attribute
        {
            var files = new List<FileInfo>();
            var matching = types
                .Where(t => t.GetCustomAttributes<TDefinition>().SingleOrDefault() != null)
                .ToArray();

            foreach (var generator in generators)
            {
                foreach (var type in matching)
                {
                    try
                    {
                        var file = generator.Invoke(type);
                        files.Add(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to execute {generator.Method.Name} for {type.FullName}: {e.Message}");
                    }
                }
            }

            return files
                .Select(f => f.FullName)
                .ToArray();
        }
        
        [Generator(typeof(FactoryAttribute), typeof(ValueTypeAttribute))]
        private static FileInfo ReadmeGenerator(Type type)
        {
            var directory = Working
                .CreateSubdirectory(DocumentationDirectory);

            var path = Path.Combine(directory.FullName, $"{type.Name}.md");
            
            using var writer = File.CreateText(path);
            writer.WriteLine($"# {type.FullName}");
            
            return new FileInfo(path);
        }
        
        [Generator(typeof(FactoryAttribute))]
        private static FileInfo ControllerGenerator(Type type)
        {
            var directory = Working
                .CreateSubdirectory(SolutionDirectory)
                .CreateSubdirectory($"{nameof(SciNet)}.Service");

            var path = Path.Combine(directory.FullName, $"{type.Name}Controller.cs");
            
            using var writer = File.CreateText(path);
            writer.WriteLine($"namespace {type.Namespace}");
            writer.WriteLine("{");
            writer.WriteLine("}");
            
            return new FileInfo(path);
        }
    }
}
