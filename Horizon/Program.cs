using HorizonCompiler;

namespace Horizon;

public abstract class Program
{
    public const string VERSION = "1.0";

    public static void Main(string[] args)
    {
        // No arguments
        if (args.Length == 0)
        {
            Console.WriteLine($"Horizon v{VERSION}");
            return;
        }

        // Handle arguments as files and options
        List<FileInfo> files = [];
        var options = new CompilerOptions();
        foreach (var arg in args)
        {
            if (arg.StartsWith('-'))
            {
                // == Options ==

                switch (arg)
                {
                    case "--debug":
                        options.mode = CompilerOptions.Mode.DEBUG;
                        break;

                    case "--show-tokens":
                        options.showTokens = true;
                        break;

                    case "--show-trees":
                        options.showTrees = true;
                        break;

                    case "--show-values":
                        options.showValues = true;
                        break;

                    default:
                        Console.WriteLine($"unknown option provided: '{arg}'");
                        return;
                }

                continue;
            }

            if (!File.Exists(arg))
            {
                Console.WriteLine($"Invalid file path: {arg}");
                return;
            }

            files.Add(new FileInfo(arg));
        }

        try
        {
            HCompiler.Compile(files, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}