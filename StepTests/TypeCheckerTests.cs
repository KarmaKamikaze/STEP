using STEP;
using STEP.AST.Nodes;
using Xunit;
using Moq;
using STEP.AST;

namespace StepTests; 

public class TypeCheckerTests {
    private readonly IVisitor _typeVisitor;
    private readonly Mock<ISymbolTable> _symbolTableMock = new Mock<ISymbolTable>();
    
    public TypeCheckerTests() {
        _typeVisitor = new TypeVisitor(_symbolTableMock.Object);
    }

    #region Expressions

    [Fact]
    public void NumberNode_ShouldBeNumber()
    {
        // Arrange
        var numLiteralNode = new NumberNode();

        // Act
        _typeVisitor.Visit(numLiteralNode);
        
        // Assert
        Assert.Equal(TypeVal.Number, numLiteralNode.TypeVal);
    }
    
    [Fact]
    public void StringNode_ShouldBeString()
    {
        // Arrange
        var strLiteralNode = new StringNode();

        // Act
        _typeVisitor.Visit(strLiteralNode);
       
        // Assert
        Assert.Equal(TypeVal.String, strLiteralNode.TypeVal);
    }
       
    [Fact]
    public void BoolNode_ShouldBeBool()
    {
        // Arrange
        var boolLiteralNode = new BoolNode();

        // Act
        _typeVisitor.Visit(boolLiteralNode);
        
        // Assert
        Assert.Equal(TypeVal.Boolean, boolLiteralNode.TypeVal);
    }

    [Fact]
    public void IdNode_GetsCorrectTypeFromDeclaration()
    {
        // Arrange
        const string id = "x";
        var dclNode = new VarDclNode()
        {
            Left = new IdNode() {Id = id},
            Right = new NumberNode() {Value = 69}
        };
        _typeVisitor.Visit(dclNode);
        var idNode = new IdNode() {Id = id};

        // Act
        _typeVisitor.Visit(idNode);
        
        // Assert
        Assert.Equal(dclNode.Right.ExprType, idNode.ExprType);
    }

    [Fact]
    public void ParenNode_MaintainsInnerType()
    {
        // Arrange
        var exprNode = new NumberNode() {Value = 420};
        var parenNode = new ParenNode() {Left = exprNode};
        
        // Act
         _typeVisitor.Visit(parenNode);
         
        // Assert
        Assert.Equal(exprNode.Type, parenNode.TypeVal);
    }

    [Fact]
    public void NegNode_ExprIsNotBoolean_ReportsTypeError()
    {        
        // Arrange
        var exprNode = new NumberNode() {Value = 420};
        var negNode = new NegNode() {Left = exprNode};
        
        // Act
        _typeVisitor.Visit(negNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, negNode.TypeVal);
    }
    
    [Fact]
    public void NegNode_ExprIsBoolean_HasTypeBoolean()
    {        
        // Arrange
        var exprNode = new BoolNode() {Value = true};
        var negNode = new NegNode() {Left = exprNode};
        
        // Act
        _typeVisitor.Visit(negNode);
         
        // Assert
        Assert.Equal(TypeVal.Boolean, negNode.TypeVal);
    }

    [Theory]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void MultNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var plusNode = new PlusNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, plusNode.TypeVal);
    }

    [Fact]
    public void MultNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var plusNode = new PlusNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, plusNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void DivNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var divNode = new DivNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(divNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, divNode.TypeVal);
    }

    [Fact]
    public void DivNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var divNode = new DivNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(divNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, divNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void PowNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var powNode = new PowNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(powNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, powNode.TypeVal);
    }

    [Fact]
    public void PowNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var powNode = new PowNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(powNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, powNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void MinusNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var minusNode = new MinusNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(minusNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, minusNode.TypeVal);
    }

    [Fact]
    public void MinusNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var minusNode = new MinusNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(minusNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, minusNode.TypeVal);
    }

    [Fact]
    public void PlusNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var plusNode = new PlusNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, plusNode.TypeVal);
    }

    [Fact]
    public void AddNode_EitherOperandIsString_HasTypeString()
    {
        // Arrange
        var leftExpr = new StringNode();
        var rightExpr = new NumberNode();
        var plusNode = new PlusNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Equal(TypeVal.String, plusNode.TypeVal);
    }

    [Fact]
    public void AddNode_EitherOperandIsBoolean_ReportsTypeError()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new BoolNode();
        var plusNode = new PlusNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, plusNode.TypeVal);
    }
    
    //TODO: Function call expression

    [Fact]
    public void AndNode_BothOperandsAreBoolean_HasTypeBoolean()
    {
        // Arrange
        var leftExpr = new BoolNode();
        var rightExpr = new BoolNode();
        var andNode = new AndNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(andNode);
         
        // Assert
        Assert.Equal(TypeVal.Boolean, andNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    public void AndNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var andNode = new AndNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(andNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, andNode.TypeVal);
    }
    
    [Fact]
    public void OrNode_BothOperandsAreBoolean_HasTypeBoolean()
    {
        // Arrange
        var leftExpr = new BoolNode();
        var rightExpr = new BoolNode();
        var orNode = new OrNode()
        {
            Left = leftExpr,
            Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(orNode);
         
        // Assert
        Assert.Equal(TypeVal.Boolean, orNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    public void OrNode_InvalidDomain_ReportsTypeError(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var orNode = new OrNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(orNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, orNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void EqNode_ValidDomain_HasTypeBoolean(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var eqNode = new EqNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(eqNode);
         
        // Assert
        Assert.Equal(TypeVal.Boolean, eqNode.TypeVal);
    }

    [Fact]
    public void EqNode_OperandsAreNotSameType_ReportsTypeError()
    {
        // Arrange
        var eqNode = new EqNode()
        {
            Left = new NumberNode(),
            Right = new StringNode()
        };
        
        // Act
        _typeVisitor.Visit(eqNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, eqNode.TypeVal);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void NeqNode_ValidDomain_HasTypeBoolean(TypeVal type)
    {
        // Arrange
        // Set up a symbol table mock which always returns a SymTableEntry with the specified type.
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var neqNode = new NeqNode()
        {
            Left = new IdNode() { Id = "x" },
            Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(neqNode);
         
        // Assert
        Assert.Equal(TypeVal.Boolean, neqNode.TypeVal);
    }

    [Fact]
    public void NeqNode_OperandsAreNotSameType_ReportsTypeError()
    {
        // Arrange
        var neqNode = new NeqNode()
        {
            Left = new NumberNode(),
            Right = new StringNode()
        };
        
        // Act
        _typeVisitor.Visit(neqNode);
         
        // Assert
        Assert.Equal(TypeVal.Error, neqNode.TypeVal);
    }
    
    #endregion

}