using Antlr4.Runtime;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using System.Collections.Generic;
using Xunit;


namespace StepTests;

public class ASTVisitorTests
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

    [Fact]
    public void VisitAst_IfWithinElseClause_BuildsCorrectAst()
    {
        // Arrange
        const string program = @"setup
                                   if(false)
                                   else
                                     if(true)
                                     end if
                                   end if
                                 end setup";
        var parseTree = GetParseTree(program);

        var innerIf = new IfNode {Condition = new BoolNode {Value = true, NodeType = AstNodeType.BoolNode}};
        var outerIf = new IfNode
        {
            Condition = new BoolNode {Value = false, NodeType = AstNodeType.BoolNode},
            ElseClause = new() {innerIf},
            NodeType = AstNodeType.IfNode
        };
        var setupNode = new SetupNode {Stmts = new() {outerIf}, NodeType = AstNodeType.SetupNode};
        var expectedAst = new ProgNode {SetupBlock = setupNode, NodeType = AstNodeType.ProgNode};

        // Act
        var actualAst = _astBuilder.Visit(parseTree);

        // Assert
        Assert.Equal(expectedAst, actualAst);
    }

    [Fact]
    public void VisitAst_ElseIfChaining_BuildsCorrectAst()
    {
        // Arrange
        const string program = @"setup
                                   if(false)
                                   else if(false)
                                   else if(true)
                                   end if
                                 end setup";
        var parseTree = GetParseTree(program);

        var firstElseIf = new ElseIfNode
        {
            Condition = new BoolNode {Value = false, NodeType = AstNodeType.BoolNode},
            Body = new List<StmtNode>(),
            NodeType = AstNodeType.ElseIfNode
        };
        var lastElseIf = new ElseIfNode
        {
            Condition = new BoolNode {Value = true, NodeType = AstNodeType.BoolNode},
            Body = new List<StmtNode>(),
            NodeType = AstNodeType.ElseIfNode
        };
        var outerIf = new IfNode
        {
            Condition = new BoolNode {Value = false, NodeType = AstNodeType.BoolNode},
            ElseIfClauses = new List<ElseIfNode>() {firstElseIf, lastElseIf},
            NodeType = AstNodeType.IfNode
        };
        var setupNode = new SetupNode {Stmts = new() {outerIf}, NodeType = AstNodeType.SetupNode};
        var expectedAst = new ProgNode {SetupBlock = setupNode, NodeType = AstNodeType.ProgNode};

        // Act
        var actualAst = _astBuilder.Visit(parseTree);

        // Assert
        Assert.Equal(expectedAst, actualAst);
    }
}