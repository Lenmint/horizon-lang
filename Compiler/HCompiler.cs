using HorizonCompiler.Tokenize;

namespace HorizonCompiler;

public static class HCompiler
{
    public static void Compile(List<FileInfo> files)
    {
        var lexer = new Lexer();

        foreach (var file in files)
        {
            // Get File Content
            var fileContent = File.ReadAllText(file.FullName);

            var tokens = lexer.Tokenize(fileContent);

            // Print tokens
            Console.WriteLine($"({file.Name})");
            foreach (var token in tokens)
            {
                Console.WriteLine($"[{token.kind}]");
                Console.WriteLine($"value: {token.value}");
                Console.WriteLine($"location: {token.start} {token.end}\n");
            }

            // HINT: =================== TEMPORARY ===================
            // We will take the first file only until we add a Linker.
            // =======================================================
            break;
        }
    }
}