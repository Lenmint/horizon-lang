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
    public static void Compile(List<FileInfo> files, CompilerOptions options)
    {
        var lexer = new Lexer();
        var parser = new Parser();
        var mainScope = new Scope();
        var interpreter = new Interpreter(mainScope);

        foreach (var file in files)
        {
            // Get File Content
            var fileContent = File.ReadAllText(file.FullName);

            // Print file name
            if (options.mode == CompilerOptions.Mode.DEBUG)
                Console.WriteLine($"Compiling file: '{file.Name}'");

            // Create tokens from the file content
            var tokens = lexer.Tokenize(fileContent);

            // Print tokens
            if (options is { mode: CompilerOptions.Mode.DEBUG, showTokens: true })
            {
                Console.WriteLine("[Tokens]");
                Console.WriteLine(JsonConvert.SerializeObject(tokens, Formatting.Indented) + "\n");
            }

            // Create tree from tokens
            var tree = parser.ProduceTree(tokens);

            // Print tree
            if (options is { mode: CompilerOptions.Mode.DEBUG, showTrees: true })
            {
                Console.WriteLine("[Tree]");
                Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented) + "\n");
            }

            // Evaluate tree
            List<Value> values = [];
            foreach (var member in tree.body)
            {
                var value = interpreter.Evaluate(member);
                values.Add(value);
            }

            // Print evaluated values
            if (options is { mode: CompilerOptions.Mode.DEBUG, showValues: true })
            {
                Console.WriteLine("[Values]");
                Console.WriteLine(JsonConvert.SerializeObject(values, Formatting.Indented));
            }

            // HINT: =================== TEMPORARY ===================
            // We will take the first file only until we add a Linker.
            // =======================================================
            break;
        }
    }
}

public struct CompilerOptions()
{
    public Mode mode = Mode.NORMAL;

    public bool showTokens = false;
    public bool showTrees = false;
    public bool showValues = false;

    public enum Mode
    {
        NORMAL,
        DEBUG
    }
}