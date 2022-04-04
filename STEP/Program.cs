using STEP.lexer;
using STEP.node;
using STEP.parser;

namespace STEP;

class Program
{
    /* The main function takes a file path as its first argument.
     * It takes additional optional arguments:
     * -pp for pretty-printing the AST */
    private static void Main(string[] args)
    {
        if (args.Length < 1)
            Exit("Usage: STEP.exe filename [Optional: -pp]");

        using (StreamReader streamReader = new StreamReader(File.Open(args[0], FileMode.Open)))
        {
            // Read the source code file
            Lexer lexer = new Lexer(streamReader);
            
            // Parse the source code
            Parser parser = new Parser(lexer);
            Start ast = null;

            try
            {
                ast = parser.Parse();
            }
            catch (Exception e)
            {
                Exit(e.ToString());
            }

            if (args.Length > 1 && args.Contains("-pp"))
            {
                // Print AST
                AstPrinter printer = new AstPrinter();
                if (ast != null) ast.Apply(printer);
            }
        }
        
        Exit("Finished!");
    }

    private static void Exit(string message)
    {
        if (message != null)
            Console.WriteLine(message);
        else
            Console.WriteLine();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }
}