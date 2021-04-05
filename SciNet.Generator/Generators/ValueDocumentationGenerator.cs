using System;
using System.Collections.Generic;
using System.IO;
using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Generator.Generators.DocumentationGenerator;

namespace SciNet.Generator.Generators
{
    public class ValueDocumentationGenerator : IGenerator<ValueAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type, DirectoryInfo destination)
        {
            var directory = destination.CreateSubdirectory(OutputFolder).CreateSubdirectory(ValuesFolder);
            var path = Path.Combine(directory.FullName, $"{type.Name}.md");

            using var writer = File.CreateText(path);
            writer.WriteHeading(type.FullName);

            return new[] {new FileInfo(path)};
        }
    }
}
