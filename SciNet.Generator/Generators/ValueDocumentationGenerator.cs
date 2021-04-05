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
    public class ValueDocumentationGenerator : IGenerator<ValueAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type, DirectoryInfo destination)
        {
            var metadata = type.GetCustomAttribute<ValueAttribute>()
                ?? throw new InvalidOperationException($"Type {type.FullName} missing {nameof(ValueAttribute)}");

            var properties = type.GetMembers()
                .Where(m => m.GetCustomAttribute<PropertyAttribute>() != null)
                .Select(p => new { Member = p, Metadata = p.GetCustomAttribute<PropertyAttribute>()})
                .ToArray();

            var directory = destination.CreateSubdirectory(OutputFolder).CreateSubdirectory(ValuesFolder);
            var path = Path.Combine(directory.FullName, $"{type.Name}.md");
            
            using var writer = File.CreateText(path);
            writer.WriteChapter($"Value {type.FullName}");

            writer.WriteSection(PropertiesSecton);
            
            foreach (var property in properties)
            {
                writer.WriteSubSection(property.Member.Name);
                writer.WriteLine(property.Metadata.Description);
            }
            
            return new[] { new FileInfo(path) };
        }
    }
}
