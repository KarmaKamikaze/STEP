using STEP.lexer;
using STEP.node;
using STEP.parser;

namespace STEP;

class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 1)
            Exit("Usage: STEP.exe filename");

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
            
            // Print AST
            AstPrinter printer = new AstPrinter();
            ast.Apply(printer);
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
        Console.Read();
        Environment.Exit(0);
    }
}