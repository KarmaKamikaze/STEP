using Antlr4.Runtime;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
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
    public void NumLiteral()
    {
        // Arrange
        string program = "setup\n" +
                         "number x = 1\n" +
                         "end setup\n";
        var parseTree = GetParseTree(program);

        var idNode = new IdNode() { Id = "x" };
        var numNode = new NumberNode() { Value = 1 };
        var varDclNode = new VarDclNode() 
        {
            Left = idNode,
            Right = numNode
        };
        var setupNode = new SetupNode() 
        {
            Stmts = new() { varDclNode }
        };
        var expectedAst = new ProgNode() { SetupBlock = setupNode };

        // Act
        // Get AST from the AstBuilderVisitor
        var actualAst = _astBuilder.Visit(parseTree);

        // Assert
        // Check if the actual AST is equivalent to the expected AST
        //Dam and Casper Died right about here
        
        ////////////////-|
        //     ||     //-|
        //  ||||||||  //-|
        //     ||     //-|
        //     ||     //-|
        //            //-|
        // HOLY BIBLE //-|
        ////////////////-|
    }

    #endregion
}