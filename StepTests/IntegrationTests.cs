using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using STEP.CodeGeneration;
using STEP.SemanticAnalysis;
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
        // Tests everything
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

        string expectedFilePath =
            Directory.GetCurrentDirectory() + "\\..\\..\\..\\IntegrationTestPrograms\\" + expectedFile;

        string expected = File.ReadAllText(expectedFilePath);

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void IntegrationTypeVisitorSymbolTable() {
        // Tests TypeVisitor + SymbolTable integration, as well as traversal of more complex tree than unit tests
        // Arrange
        var typeVisitor = new TypeVisitor();
        var exprNode = new PlusNode() {
            Left = new NumberNode() {Value = 2},
            Right = new NumberNode() {Value = 5}
        };
        var idNode = new IdNode() {
            Name = "a",
            Type = new Type() {ActualType = TypeVal.Number}
        };
        var varDcl = new VarDclNode() {
            Left = idNode,
            Right = exprNode
        };
        var boolLeft = new GThanNode() {
            Left = new IdNode(){Name = "a"},
            Right = new NumberNode(){Value = 5}
        };
        var boolRight = new EqNode() {
            Left = new NumberNode(){Value = 10},
            Right = new NumberNode(){Value = 10} 
        };
        var boolCond = new AndNode() {
            Left = boolLeft,
            Right = boolRight
        };
        var ifNode = new IfNode() {
            Condition = boolCond,
            ThenClause = new List<StmtNode>() {new ContNode()}
        };
        var progNode = new ProgNode() {
            SetupBlock = new SetupNode() {
                Stmts = new List<StmtNode>() {
                    varDcl,
                    ifNode
                }
            }
        };
        
        // Act
        progNode.Accept(typeVisitor);
        
        // Assert
        // Is the vardcl ok? requires expr node = number, as var id is number
        Assert.Equal(TypeVal.Ok, varDcl.Type.ActualType);
        Assert.Equal(TypeVal.Number, exprNode.Type.ActualType);
        
        // Is the ifnode ok? requires condition = bool, andnode exprs = bool 
        Assert.Equal(TypeVal.Ok, ifNode.Type.ActualType);
        Assert.Equal(TypeVal.Boolean, boolCond.Type.ActualType);
        Assert.Equal(TypeVal.Boolean, boolLeft.Type.ActualType);
        Assert.Equal(TypeVal.Boolean, boolRight.Type.ActualType);
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
}