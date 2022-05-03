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
     * -print for pretty-printing the AST
     * -upload to upload the compiled STEP program directly to an Arduino board
     * -output to monitor the serial communications port for terminal output */
    private static void Main(string[] args)
    {
        if (args.Length < 1 || args.Contains("-help"))
            Exit("Usage: STEP.exe filename " +
                 "[Optional: -print] " +
                 "[Optional: -ports] " +
                 "[Optional: -upload PORT] " +
                 "[Optional: -output PORT]");

        string port = CheckPort(args);
        
        if (args.Length is 1 or 2 && args.Contains("-ports"))
        {
            ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
            arduinoCompiler.ListPorts();
            
            Exit("End of port list!");
        }
        if (args.Length is 1 or 2 && args.Contains("-output"))
        {
            ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
            arduinoCompiler.Monitor();
            
            Exit("End of output!");
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

            // Generate code and output to .ino file
            CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
            root.Accept(codeGen);
            codeGen.OutputToBaseFile(Path.GetFileNameWithoutExtension(args[0]));

            // Upload compiled hex program to Arduino board
            if (args.Length > 1 && args.Contains("-upload"))
            {
                ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
                arduinoCompiler.Upload(Path.GetFileNameWithoutExtension(args[0]));

                if (args.Contains("-output"))
                    arduinoCompiler.Monitor();
            }
        }
        catch (Exception e)
        {
            Exit(e.ToString());
        }

        Exit("Finished!");
    }
    
    private static string CheckPort(string[] applicationArgs)
    {
        // Pattern matches COM[number] (windows) and /dev/ttyACM[number] (linux)
        Regex regex = new Regex("(COM\\d+|\\/dev\\/ttyACM\\d+)");
        return applicationArgs.FirstOrDefault(arg => regex.IsMatch(arg));
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