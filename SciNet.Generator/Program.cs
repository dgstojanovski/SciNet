using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MetaNet.Core;
using MetaNet.Generators;
using MetaNet.Core.Attributes;
using SciNet;
using SciNet.Mathematics;

namespace SciNet.Generator
{
    public static class Program
    {
        private static DirectoryInfo _working;

        public static void Main(string[] args)
        {
            // Because lazy loading
            Vector.Random(10);

            _working = new DirectoryInfo(args.Length > 0
                ? args[0]
                : Path.Combine(Environment.CurrentDirectory, nameof(_working)));
            if (_working.Exists)
                _working.Delete(true);

            _working.Create();

            var exports = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetExportedTypes())
                .ToArray();

            var types = exports
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(nameof(SciNet)))
                .ToArray();

            var generators = exports
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i == typeof(IGenerator)))
                .Select(t => Activator.CreateInstance(t) as IGenerator)
                .ToArray();

            Execute<DefinitionAttribute>(types, generators);
            Execute<ValueAttribute>(types, generators);
        }

        private static void Execute<TDefinition>(IEnumerable<Type> types, params IGenerator[] generators)
            where TDefinition : Attribute
        {
            types = types
                .Where(t => t.GetCustomAttributes<TDefinition>().SingleOrDefault() != null)
                .ToArray();

            generators = generators
                .Where(g => g.GetType().GetInterfaces()
                    .Any(i => i.GetGenericArguments().Any(t => t == typeof(TDefinition))))
                .ToArray();

            foreach (var generator in generators)
            foreach (var type in types)
            {
                var generatorName = generator.GetType().Name;
                var typeName = type.FullName;

                try
                {
                    var files = generator.Generate(type, _working).Select(f => f.FullName);
                    Console.WriteLine($"Executed {generatorName} for {typeName}: {string.Join(", ", files)}");
                }
                catch (Exception e)
                {
                    throw new AggregateException(
                        $"Failed to execute {generatorName} for {typeName}: {e.Message}", e);
                }
            }
        }
    }
}