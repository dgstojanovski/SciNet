using System.IO;

namespace SciNet.Generator.Generators.Markdown
{
    public static class MarkdownGenerator
    {
        public const string OutputFolder = "Documentation";
        
        public const string DefinitionsFolder = "Definitions";
        
        public const string ValuesFolder = "Values";
        
        public const string MethodsSection = "Methods";

        public const string PropertiesSection = "Properties";

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
