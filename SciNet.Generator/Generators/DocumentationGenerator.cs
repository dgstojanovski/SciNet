using System.IO;
using SciNet.Core;

namespace SciNet.Generator.Generators
{
    public static class DocumentationGenerator
    {
        public const string OutputFolder = "Documentation";
        
        public const string DefinitionsFolder = "Definitions";
        
        public const string ValuesFolder = "Values";
        
        public const string MethodsSection = "Methods";

        public const string PropertiesSecton = "Properties";

        public static void WriteChapter(this StreamWriter writer, string text)
        {
            writer.WriteLine($"# {text}");
        }
        
        public static void WriteSection(this StreamWriter writer, string text)
        {
            writer.WriteLine($"## {text}");
        }
        
        public static void WriteSubSection(this StreamWriter writer, string text)
        {
            writer.WriteLine($"### {text}");
        }
        
        public static void WriteList<T>(this StreamWriter writer, params T[] values)
        {
            writer.WriteLine(string.Concat("* ", string.Join("\n* ", values)));
        }
    }
}
