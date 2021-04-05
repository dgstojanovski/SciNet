using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Generator.Generators.DocumentationGenerator;

namespace SciNet.Generator.Generators
{
    public class DefinitionDocumentationGenerator : IGenerator<DefinitionAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type, DirectoryInfo destination)
        {
            var metadata = type.GetCustomAttribute<DefinitionAttribute>()
                ?? throw new InvalidOperationException($"Type {type.FullName} missing {nameof(DefinitionAttribute)}");
            
            var methods = type.GetMethods()
                .Where(m => m.GetCustomAttribute<FactoryAttribute>() != null)
                .Select(p => (p, p.GetCustomAttribute<FactoryAttribute>()))
                .ToArray();
            
            var properties = type.GetProperties()
                .Where(m => m.GetCustomAttribute<FactoryAttribute>() != null)
                .Select(p => (p, p.GetCustomAttribute<FactoryAttribute>()))
                .ToArray();
            
            var directory = destination.CreateSubdirectory(OutputFolder).CreateSubdirectory(DefinitionsFolder);
            var path = Path.Combine(directory.FullName, $"{type.Name}.md");

            using var writer = File.CreateText(path);
            writer.WriteChapter($"Definition {type.FullName} <{metadata.ValueType.FullName}>");
            writer.WriteLine(metadata.Description);

            writer.WriteSection(MethodsSection);
            foreach (var definition in methods)
            {
                var (method, meta) = definition;
                var parameters = method.GetParameters()
                    .Select(p => $"{p.ParameterType.Name} {p.Name}")
                    .ToArray();
                var signature = $"{method.Name}({string.Join(", ", parameters)})";
                
                writer.WriteSubSection(signature);
                writer.WriteLine(meta.Description);
            }
            
            writer.WriteSection(PropertiesSecton);
            foreach (var definition in properties)
            {
                var (property, meta) = definition;
                
                writer.WriteSubSection(property.Name);
                writer.WriteLine(meta.Description);
            }

            return new[] { new FileInfo(path) };
        }
    }
}
