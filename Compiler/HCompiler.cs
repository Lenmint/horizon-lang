using HorizonCompiler.Evaluate;
using HorizonCompiler.Evaluate.Values;
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
        var interpreter = new Interpreter();

        foreach (var file in files)
        {
            // Get File Content
            var fileContent = File.ReadAllText(file.FullName);

            // Create tokens from the file content
            var tokens = lexer.Tokenize(fileContent);

            // Create tree from tokens
            var tree = parser.ProduceTree(tokens);

            // Evaluate tree
            List<Value> values = [];
            foreach (var member in tree.body)
            {
                var value = interpreter.Evaluate(member);
                values.Add(value);
            }

#if DEBUG
            // ========= Print values ==========
            // Print file name
            Console.WriteLine($"({file.Name})");

            // Print tokens
            //Console.WriteLine(JsonConvert.SerializeObject(tokens, Formatting.Indented) + "\n");

            // Print tree
            //Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented) + "\n");

            // Print evaluated values
            Console.WriteLine(JsonConvert.SerializeObject(values, Formatting.Indented));
#endif

            // HINT: =================== TEMPORARY ===================
            // We will take the first file only until we add a Linker.
            // =======================================================
            break;
        }
    }
}