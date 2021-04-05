using System.IO;

namespace SciNet.Generator.Generators
{
    public static class DocumentationGenerator
    {
        public const string OutputFolder = "Documentation";
        
        public const string DefinitionsFolder = "Definitions";
        
        public const string ValuesFolder = "Values";

        public static void WriteHeading(this StreamWriter writer, string text)
        {
            writer.WriteLine($"# {text}");
        }
    }
}
