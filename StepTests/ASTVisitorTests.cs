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

    #region Literals

    // Assume that the ANTLR-generated parser works correctly (it should have its own unit tests)
    // These tests are for checking that the ASTs built by the AstBuilderVisitor from parse trees are correct.
    // So these are integration tests that should check the generated ASTs for larger programs.
    [Fact]
    public void VisitAst_SetupWithNumLiteralDeclaration_BuildsCorrectAst()
    {
        // Arrange
        const string program = @"setup
                                     number x = 1
                                 end setup";

        var parseTree = GetParseTree(program);

        var idNode = new IdNode() {Name = "x", NodeType = AstNodeType.IdNode};
        var numNode = new NumberNode() {Value = 1, NodeType = AstNodeType.NumberNode};
        var varDclNode = new VarDclNode()
        {
            Left = idNode,
            Right = numNode,
            NodeType = AstNodeType.VarDclNode
        };
        var setupNode = new SetupNode()
        {
            Stmts = new() {varDclNode},
            NodeType = AstNodeType.SetupNode
        };
        var expectedAst = new ProgNode() {SetupBlock = setupNode, NodeType = AstNodeType.ProgNode};

        // Act
        // Get AST from the AstBuilderVisitor
        var actualAst = _astBuilder.Visit(parseTree);

        // Assert
        Assert.Equal(expectedAst, actualAst);

        // Check if the actual AST is equivalent to the expected AST
        //Dam and Casper Died right about here

        ////////////////-|
        //            //-|
        //     ||     //-|
        //  ||||||||  //-|
        //     ||     //-|
        //     ||     //-|
        //            //-|
        //            //-|
        // HOLY BIBLE //-|
        ////////////////-|
    }

    #endregion

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