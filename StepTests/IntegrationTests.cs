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
        string sourceFilePath =
            Directory.GetCurrentDirectory() + "\\..\\..\\..\\IntegrationTestPrograms\\" + sourceFile;
        string sourceText = File.ReadAllText(sourceFilePath);
        STEPParser.ProgramContext tree = GetParseTree(sourceText);

        AstBuilderVisitor astBuilder = new AstBuilderVisitor();
        AstNode root = astBuilder.Build(tree);

        TypeVisitor typeVisitor = new TypeVisitor();
        root.Accept(typeVisitor);

        CodeGenerationVisitor codeGen = new CodeGenerationVisitor();
        root.Accept(codeGen);

        string actual = codeGen.OutputToString();

        string expectedFilePath =
            Directory.GetCurrentDirectory() + "\\..\\..\\..\\IntegrationTestPrograms\\" + expectedFile;

        string expected = File.ReadAllText(expectedFilePath);

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<string[]> programStrings =>
        new List<string[]>()
        {
            new string[]
            {
                "printArraySource.step", "printArrayExpected.ino"
            },
            new string[]
            {
                "int2Source.step", "int2Expected.ino"
            }
        };

    [Fact]
    public void TypeVisitor_Function1CallsFunction2WhichIsDeclaredLater_WorksFlawlessly()
    {
        // The scope rules of STEP state that any function must be able to call other functions,
        // regardless of the order in which they are declared (to avoid the need for prototypes)

        /* blank function funcA()
         *   funcB()
         * end function
         * 
         * blank function funcB()
         * end function
         */

        // Arrange
        var typeVisitor = new TypeVisitor();
        var funcB = new FuncDefNode
        {
            FormalParams = new(),
            Id = new IdNode { Name = "funcB" },
            ReturnType = new Type { ActualType = TypeVal.Blank },
            Type = new Type { ActualType = TypeVal.Blank },
            Stmts = new()
        };
        var funcA = new FuncDefNode
        {
            FormalParams = new(),
            Id = new IdNode { Name = "funcA" },
            ReturnType = new Type { ActualType = TypeVal.Blank },
            Type = new Type { ActualType = TypeVal.Blank },
            Stmts = new()
            {
                new FuncStmtNode
                {
                    Id = new IdNode { Name = funcB.Id.Name },
                    Params = new()
                }
            }
        };
        var funcBlock = new FuncsNode
        {
            FuncDcls = new List<FuncDefNode> { funcA, funcB }
        };

        // Act
        var action = () => typeVisitor.Visit(funcBlock);

        // Assert - should run without errors
        action();
    }
}