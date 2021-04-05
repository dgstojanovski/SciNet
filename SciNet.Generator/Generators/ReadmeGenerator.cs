using System;
using System.Collections.Generic;
using System.IO;
using SciNet.Core;

namespace SciNet.Generator.Generators
{
    public class ReadmeGenerator : IGenerator<FactoryAttribute>, IGenerator<ValueTypeAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type)
        {
            var directory = Program.Working
                .CreateSubdirectory(Program.DocumentationDirectory);

            var path = Path.Combine(directory.FullName, $"{type.Name}.md");
            
            using var writer = File.CreateText(path);
            writer.WriteLine($"# {type.FullName}");
            
            return new[] { new FileInfo(path) };
        }
    }
}
