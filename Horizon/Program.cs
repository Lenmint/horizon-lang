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

        // Handle arguments as files
        List<string> files = [];
        foreach (var arg in args)
        {
            if (!File.Exists(arg))
            {
                Console.WriteLine($"Invalid file path: {arg}");
                return;
            }
            
            files.Add(File.ReadAllText(arg));
        }
        
        HCompiler.Compile(files);
    }
}