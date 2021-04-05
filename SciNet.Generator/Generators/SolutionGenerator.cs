using System;
using System.Collections.Generic;
using System.IO;
using SciNet.Core;

namespace SciNet.Generator.Generators
{
    public class SolutionGenerator : IGenerator<FactoryAttribute>
    {
        public IEnumerable<FileInfo> Generate(Type type)
        {
            var directory = Program.Working
                .CreateSubdirectory(Program.SolutionDirectory)
                .CreateSubdirectory($"{nameof(SciNet)}.Service");

            var path = Path.Combine(directory.FullName, $"{type.Name}Controller.cs");
            
            using var writer = File.CreateText(path);
            writer.WriteLine($"namespace {type.Namespace}");
            writer.WriteLine("{");
            writer.WriteLine("}");
            
            return new[] { new FileInfo(path) };
        }
    }
}
