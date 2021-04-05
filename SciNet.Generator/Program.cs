﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SciNet.Core;
using SciNet.Core.Attributes;

namespace SciNet.Generator
{
    public static class Program
    {
        public static DirectoryInfo Working;

        public static void Main(string[] args)
        {
            Working = new DirectoryInfo(args.Length > 0
                ? args[0]
                : Path.Combine(Environment.CurrentDirectory, nameof(Working)));

            if (Working.Exists)
                Working.Delete(true);

            Working.Create();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(nameof(SciNet)))
                .ToArray();

            var generators = types
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
                var generatorName = generator.GetType().FullName;
                var typeName = type.FullName;

                try
                {
                    var files = generator.Generate(type).Select(f => f.FullName);
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