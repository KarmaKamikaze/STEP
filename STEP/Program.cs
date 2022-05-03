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
        STEPErrorListener errorListener = new STEPErrorListener();
        // Remove default console error reporter
        parser.RemoveErrorListener(ConsoleErrorListener<IToken>.Instance);
        // Add new error reporter (throws exception, ending compilation on parser error
        parser.AddErrorListener(errorListener);

        try {
            Console.WriteLine("Performing syntactic analysis...");
            STEPParser.ProgramContext tree = parser.program(); // Parse the input starting at the "program" rule.

            // TODO: error listener

            // Build AST
            AstBuilderVisitor astBuilder = new AstBuilderVisitor();
            AstNode root = astBuilder.Build(tree);

            Console.WriteLine("Performing contextual analysis...");
            // Decorate the AST
            TypeVisitor typeVisitor = new();
            root.Accept(typeVisitor);

            if (args.Length > 1 && args.Contains("-print")) {
                Console.WriteLine("Pretty-printing the source program...");
                // Print AST
                AstPrintVisitor printer = new AstPrintVisitor();
                root.Accept(printer);
            }

            // Rename symbols from the standard environment to their respective Arduino alias
            StandardEnvironmentVisitor stdEnvVisitor = new StandardEnvironmentVisitor();
            root.Accept(stdEnvVisitor);

            Console.WriteLine("Performing code generation...");
            // Generate code and output to .ino file
            CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
            root.Accept(codeGen);
            codeGen.OutputToBaseFile(Path.GetFileNameWithoutExtension(args[0]));

            // Upload compiled hex program to Arduino board
            if (args.Length > 1 && args.Contains("-upload")) {
                Console.WriteLine($"Uploading program to Arduino {(port == null ? $"on port {port}" : "")}...");
                ArduinoCompiler arduinoCompiler = new ArduinoCompiler(port);
                arduinoCompiler.Upload(Path.GetFileNameWithoutExtension(args[0]));

                if (args.Contains("-output"))
                    arduinoCompiler.Monitor();
            }
        }
        catch (TypeMismatchException e) {
            Exit(
                $"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Expected {string.Join(", ", e.Expected)}, actual was {e.Actual})");
        }
        catch (ArraySizeMismatchException e) {
            Exit(
                $"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Destination array \"{e.DestinationArray.Name}\" has size {e.DestinationArray.Type.ArrSize}, but \"{e.SourceArray.Name}\" has size {e.SourceArray.Type.ArrSize})");
        }
        catch (DuplicatePinDeclarationException e) {
            Exit(
                $"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier \"{e.VariableId}\", pin {e.Pin})"); // TODO: Distinction between analog and digital pins?
        }
        catch (DuplicateDeclarationException e) {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier \"{e.VariableId}\")");
        }
        catch (PinTableUnexpectedTypeException e) {
#if DEBUG
            Exit(
                $"An internal error occurred: {e.Message} (Expected {string.Join(", ", e.Expected)}, actual was {e.Actual})");
#else
            Exit("An unexpected error occurred");
#endif
        }
        catch (ParameterCountMismatchException e) {
            Exit(
                $"{GetErrorPrefix(e.SourcePosition)} {e.Message} ({(e.VariableId != null ? $"Identifier \"{e.VariableId}\"." : "")}Expected {e.ExpectedCount}, actual was {e.ActualCount})");
        }
        catch (SymbolNotDeclaredException e) {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier \"{e.VariableId}\")");
        }
        catch (SemanticException e) {
            Exit($"Semantic error occurred on line {e.Line}, position {e.CharPositionInLine}: {e.Message}");
        }
        catch (Exception e)
        {
#if DEBUG
            Exit(e.ToString());
#else
            Exit(e.Message);
#endif
        }

        Exit("Finished successfully!");
    }

    private static string GetErrorPrefix(SourcePosition sourcePosition)
    {
        return $"Error in line {sourcePosition.Line}, position {sourcePosition.Index}:";
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