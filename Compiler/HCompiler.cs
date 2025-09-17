using HorizonCompiler.Tokenize;
using HorizonCompiler.Parse;

#if DEBUG
using Newtonsoft.Json;
#endif

namespace HorizonCompiler;

public static class HCompiler
{
    public static void Compile(List<FileInfo> files)
    {
        var lexer = new Lexer();
        var parser = new Parser();

        foreach (var file in files)
        {
            // Get File Content
            var fileContent = File.ReadAllText(file.FullName);

            // Create tokens from the file content
            var tokens = lexer.Tokenize(fileContent);

            // Create tree from tokens
            var tree = parser.ProduceTree(tokens);

            // TODO: Evaluate tree

#if DEBUG
            // Print tree
            Console.WriteLine($"({file.Name})");
            Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
#endif

            // HINT: =================== TEMPORARY ===================
            // We will take the first file only until we add a Linker.
            // =======================================================
            break;
        }
    }
}