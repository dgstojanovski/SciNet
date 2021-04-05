using System;
using System.Collections.Generic;
using System.IO;
using SciNet.Core;
using SciNet.Core.Attributes;

namespace SciNet.Generator.Generators
{
    public class ValueDocumentationGenerator : IGenerator<ValueAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type)
        {
            var directory = Program.Working
                .CreateSubdirectory(DocumentationGenerator.Directory);

            var path = Path.Combine(directory.FullName, $"{type.Name}.md");

            using var writer = File.CreateText(path);
            writer.WriteLine($"# {type.FullName}");

            return new[] {new FileInfo(path)};
        }
    }
}