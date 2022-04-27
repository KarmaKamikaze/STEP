using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using STEP.CodeGeneration;
using Xunit;

namespace StepTests;

public class IntegrationTests
{

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
    public void IntegrationTest1(string sourceFile, string expectedFile)
    {
        string sourceFilePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\IntegrationTestPrograms\\" + sourceFile;
        string sourceText = File.ReadAllText(sourceFilePath);
        STEPParser.ProgramContext tree = GetParseTree(sourceText);
        
        AstBuilderVisitor astBuilder = new AstBuilderVisitor();
        AstNode root = astBuilder.Build(tree);
        
        TypeVisitor typeVisitor = new TypeVisitor();
        root.Accept(typeVisitor);
        
        CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
        root.Accept(codeGen);

        string actual = codeGen.OutputToString();

        string expectedFilePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\IntegrationTestPrograms\\" + expectedFile;
        string expected = File.ReadAllText(expectedFilePath);
        
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<string[]> programStrings =>
        new List<string[]>()
        {
            new string[]
            {
                "printArraySource.step" , "printArrayExpected.ino"  
            },
            new string[]
            {
                "int2Source.step" , "int2Expected.ino"
            }
            
        };

}
