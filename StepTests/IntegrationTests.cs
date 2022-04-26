using System.Collections.Generic;
using Antlr4.Runtime;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using STEP.CodeGeneration;
using Xunit;

namespace StepTests;

public class IntegrationTests
{
    private AstBuilderVisitor _astBuilder = new();
    
    private STEPParser.ProgramContext GetParseTree(string input) 
    { 
        AntlrInputStream streamReader = new(input);
        STEPLexer lexer = new(streamReader);
        CommonTokenStream tokenStream = new(lexer);
        STEPParser parser = new(tokenStream);
        return parser.program();
    }

    [Theory]
    [MemberData(nameof(programStrings))]
    public void IntegrationTest1(string source, string expected)
    {
        STEPParser.ProgramContext parseTree = GetParseTree(source);
        
        AstBuilderVisitor astBuilder = new AstBuilderVisitor();
        AstNode root = astBuilder.Build(parseTree);
        
        TypeVisitor typeVisitor = new();
        root.Accept(typeVisitor);
        
        CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
        root.Accept(codeGen);

        string actual = codeGen.OutputToString();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<string[]> programStrings =>
        new List<string[]>()
        {
            new string[] {@"setup
                           if(false)
                           else
                             if(true)
                             end if
                           end if
                         end setup",
                           "void setup() {\r\n"+ 
                             "if(false) {\r\n"+
                             "}\r\n"+
                             "else {\r\n"+
                                "if(true) {\r\n"+
                                "}\r\n"+
                             "}\r\n"+
                           "}\r\n" 
            },
            
        };

}
