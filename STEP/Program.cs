using System.Text.RegularExpressions;
using Antlr4.Runtime;
using STEP.AST;
using STEP.AST.Nodes;
using STEP.CodeGeneration;

namespace STEP;

class Program
{
    /* The main function takes a file path as its first argument.
     * It takes additional optional arguments:
     * -pp for pretty-printing the AST */
    private static void Main(string[] args)
    {
        if (args.Length < 1 || args.Contains("-help"))
            Exit("Usage: STEP.exe filename [Optional: -print] [Optional: -upload PORT] [Optional: -output PORT]");
        
        if (args.Length > 1 && args.Contains("-output"))
        {
            int portIndex = 0;

            portIndex = args.ToList().IndexOf("-output") + 1;
            if (portIndex >= args.Length)
                throw new IndexOutOfRangeException("Please supply a port after the -output parameter.");
                
            var port = args[portIndex];
                
            Regex regex = new Regex("(COM\\d+|\\/dev\\/ttyACM\\d+)");
            if (regex.IsMatch(port))
            {
                ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
                arduinoCompiler.Monitor();
            }
            else
            {
                throw new ArgumentException("Please supply a valid port after the -output parameter.");
            }
            
            Exit("Finished!");
        }

        // Stream reader opens source file
        AntlrFileStream streamReader = new AntlrFileStream(args[0]);

        // Read the source code file
        STEPLexer lexer = new STEPLexer(streamReader);

        // Get token stream
        CommonTokenStream tokenStream = new CommonTokenStream(lexer);

        // Parse the source code
        STEPParser parser = new STEPParser(tokenStream);

        try
        {
            STEPParser.ProgramContext tree = parser.program(); // Parse the input starting at the "program" rule.

            // Build AST
            AstBuilderVisitor astBuilder = new AstBuilderVisitor();
            AstNode root = astBuilder.Build(tree);
            // Decorate the AST
            TypeVisitor typeVisitor = new();
            root.Accept(typeVisitor);

            if (args.Length > 1 && args.Contains("-print"))
            {
                // Print AST
                AstPrintVisitor printer = new AstPrintVisitor();
                root.Accept(printer);
            }

            // Rename symbols from the standard environment to their respective Arduino alias
            StandardEnvironmentVisitor stdEnvVisitor = new StandardEnvironmentVisitor();
            root.Accept(stdEnvVisitor);

            // Generate code and output to .c file
            CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
            root.Accept(codeGen);
            codeGen.OutputToBaseFile(Path.GetFileNameWithoutExtension(args[0]));

            if (args.Length > 1 && args.Contains("-upload"))
            {
                int portIndex = 0;

                portIndex = args.ToList().IndexOf("-upload") + 1;
                if (portIndex >= args.Length)
                    throw new IndexOutOfRangeException("Please supply a port after the -upload parameter.");
                
                var port = args[portIndex];
                
                Regex regex = new Regex("(COM\\d+|\\/dev\\/ttyACM\\d+)");
                if (regex.IsMatch(port))
                {
                    // Upload compiled hex program to Arduino board
                    ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
                    arduinoCompiler.Upload(Path.GetFileNameWithoutExtension(args[0]));
                }
                else
                {
                    throw new ArgumentException("Please supply a valid port after the -upload parameter.");
                }
            }
        }
        catch (Exception e)
        {
            Exit(e.ToString());
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