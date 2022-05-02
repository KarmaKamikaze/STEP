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
            Exit("Usage: STEP.exe filename [Optional: -print] [Optional: -upload]");

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
            Console.WriteLine("Performing syntactic analysis...");
            STEPParser.ProgramContext tree = parser.program(); // Parse the input starting at the "program" rule.

            // Build AST
            AstBuilderVisitor astBuilder = new AstBuilderVisitor();
            AstNode root = astBuilder.Build(tree);

            Console.WriteLine("Performing contextual analysis...");
            // Decorate the AST
            TypeVisitor typeVisitor = new();
            root.Accept(typeVisitor);

            if (args.Length > 1 && args.Contains("-print"))
            {
                Console.WriteLine("Pretty-printing the source program...");
                // Print AST
                AstPrintVisitor printer = new AstPrintVisitor();
                root.Accept(printer);
            }

            // Rename symbols from the standard environment to their respective Arduino alias
            StandardEnvironmentVisitor stdEnvVisitor = new StandardEnvironmentVisitor();
            root.Accept(stdEnvVisitor);

            Console.WriteLine("Performing code generation...");
            // Generate code and output to .c file
            CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
            root.Accept(codeGen);
            codeGen.OutputToBaseFile(Path.GetFileNameWithoutExtension(args[0]));

            if (args.Length > 1 && args.Contains("-upload"))
            {
                Console.WriteLine("Uploading program to Arduino on port ..."); // TODO: add port and other relevant stuffs
                // Upload compiled hex program to Arduino board
                ArduinoCompiler arduinoCompiler = new ArduinoCompiler();
                arduinoCompiler.Upload(Path.GetFileNameWithoutExtension(args[0]));
            }
        }
        catch (TypeMismatchException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Expected {string.Join(", ", e.Expected)}, actual was {e.Actual})");
        }
        catch (ArraySizeMismatchException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Destination array {e.DestinationArray.Name} has size {e.DestinationArray.Type.ArrSize}, but {e.SourceArray.Name} has size {e.SourceArray.Type.ArrSize})");
        }
        catch (DuplicatePinDeclarationException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier {e.VariableId}, pin {e.Pin})"); // TODO: Distinction between analog and digital pins?
        }
        catch (DuplicateDeclarationException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier {e.VariableId})");
        }
        catch (PinTableUnexpectedTypeException e)
        {
#if DEBUG
            Exit($"An internal error occurred: {e.Message} (Expected {string.Join(", ", e.Expected)}, actual was {e.Actual})");
#else 
            Exit("An unexpected error occurred");
#endif
        }
        catch (ParameterCountMismatchException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier {e.VariableId}. Expected {e.ExpectedCount}, actual was {e.ActualCount})");
        }
        catch (SymbolNotDeclaredException e)
        {
            Exit($"{GetErrorPrefix(e.SourcePosition)} {e.Message} (Identifier {e.VariableId})");
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