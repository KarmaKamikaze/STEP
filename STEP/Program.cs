﻿using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using STEP.AST;
using STEP.AST.Nodes;

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
            
            if (args.Length > 1 && args.Contains("-pp"))
            {
                // Print parse tree
                PrettyPrinter listener = new PrettyPrinter();
                ParseTreeWalker treeWalker = new ParseTreeWalker();
                treeWalker.Walk(listener, tree);
            }

            // Build AST
            AstBuilderVisitor astBuilder = new AstBuilderVisitor();
            AstNode root = astBuilder.Build(tree);
            TypeVisitor typeVisitor = new();
            root.Accept(typeVisitor);

            Printer(root);
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

    public static void Printer(AstNode node)
    {
        if (node == null)
            return;
        Console.WriteLine($"Parent: {node.Parent}");
        Console.WriteLine($"Type: {node.GetType()}");
        Console.WriteLine($"Node: {node.NodeType}");
        Console.WriteLine($"Left child: {node.LeftmostChild}");
        Console.WriteLine($"Leftmost sibling: {node.LeftmostSibling}");
        Console.WriteLine($"Right sibling: {node.RightSibling}");
        Console.WriteLine();

        Printer(node.LeftmostChild);
        if (node.RightSibling != null)
            Printer(node.RightSibling);

    }
}