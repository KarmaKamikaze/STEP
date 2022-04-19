using System;
using System.Collections.Generic;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using Xunit;
using Moq;

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
        Assert.Equal(TypeVal.Number, numLiteralNode.Type);
    }
    
    [Fact]
    public void StringNode_ShouldBeString()
    {
        // Arrange
        var strLiteralNode = new StringNode();

        // Act
        _typeVisitor.Visit(strLiteralNode);
        
        // Assert
        Assert.Equal(TypeVal.String, strLiteralNode.Type);
    }
        
    [Fact]
    public void BoolNode_ShouldBeBool()
    {
        // Arrange
        var boolLiteralNode = new BoolNode();

        // Act
        _typeVisitor.Visit(boolLiteralNode);
        
        // Assert
        Assert.Equal(TypeVal.Boolean, boolLiteralNode.Type);
    }

    [Fact]
    public void IdNode_GetsCorrectTypeFromDeclaration()
    {
        // Arrange
        const string id = "x";
        SymTableEntry symbolTableEntry = new();
        _symbolTableMock.Setup(x => x.EnterSymbol(It.IsAny<string>(), It.IsAny<TypeVal>()))
        .Callback<string, TypeVal>((a, b) => symbolTableEntry = new SymTableEntry(){ Name = a, Type = b});
        _symbolTableMock.Setup(x => x.RetrieveSymbol(id))
        .Returns(symbolTableEntry);
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
        Assert.Equal(dclNode.Right.Type, idNode.Type);
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
        Assert.Equal(exprNode.Type, parenNode.Type);
    }
    
    [Fact]
    public void UMinusNode_TypeMatch_LeftTypeIsNumber()
    {
        // Arrange
        var exprNode = new NumberNode() {Value = 69420};
        var UMinusNode = new UMinusNode() {Left = exprNode};
        // Act
        _typevisitor.Visit(
        
        // Assert
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
        Assert.Equal(TypeVal.Error, negNode.Type);
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
        Assert.Equal(TypeVal.Boolean, negNode.Type);
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
        var multNode = new MultNode()
        {
        Left = new IdNode() { Id = "x" },
        Right = new IdNode() { Id = "y" }
        };
        
        // Act
        _typeVisitor.Visit(multNode);
        
        // Assert
        Assert.Equal(TypeVal.Error, multNode.Type);
    }

    [Fact]
    public void MultNode_BothOperandsAreNumbers_HasTypeNumber()
    {
        // Arrange
        var leftExpr = new NumberNode();
        var rightExpr = new NumberNode();
        var multNode = new MultNode()
        {
        Left = leftExpr,
        Right = rightExpr
        };
        
        // Act
        _typeVisitor.Visit(multNode);
         
        // Assert
        Assert.Equal(TypeVal.Number, multNode.Type);
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
        Assert.Equal(TypeVal.Error, divNode.Type);
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
        Assert.Equal(TypeVal.Number, divNode.Type);
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
        Assert.Equal(TypeVal.Error, powNode.Type);
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
        Assert.Equal(TypeVal.Number, powNode.Type);
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
        Assert.Equal(TypeVal.Error, minusNode.Type);
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
        Assert.Equal(TypeVal.Number, minusNode.Type);
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
        Assert.Equal(TypeVal.Number, plusNode.Type);
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
        Assert.Equal(TypeVal.String, plusNode.Type);
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
        Assert.Equal(TypeVal.Error, plusNode.Type);
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
        Assert.Equal(TypeVal.Boolean, andNode.Type);
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
        Assert.Equal(TypeVal.Error, andNode.Type);
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
        Assert.Equal(TypeVal.Boolean, orNode.Type);
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
        Assert.Equal(TypeVal.Error, orNode.Type);
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
        Assert.Equal(TypeVal.Boolean, eqNode.Type);
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
        Assert.Equal(TypeVal.Error, eqNode.Type);
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
        Assert.Equal(TypeVal.Boolean, neqNode.Type);
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
        Assert.Equal(TypeVal.Error, neqNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void GThanNode_TypeMismatch_IsTypeError(TypeVal type)
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var gThanNode = new GThanNode() {
        Left = new IdNode() {Id = "left"},
        Right = new IdNode() {Id = "right"}
        };
        // Act
        gThanNode.Accept(_typeVisitor);
        // Assert
        Assert.Equal(TypeVal.Error, gThanNode.Type);
    }
    
    [Fact]
    public void GThanNode_TypeMismatch_IsTypeBoolean()
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var gThanNode = new GThanNode() {
        Left = new IdNode() {Id = "left"},
        Right = new IdNode() {Id = "right"}
        };
        // Act
        gThanNode.Accept(_typeVisitor);
        // Assert
        Assert.Equal(TypeVal.Boolean, gThanNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void GThanEqNode_TypeMismatch_IsTypeError(TypeVal type)
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var gThanEqNode = new GThanEqNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };
        // Act
        gThanEqNode.Accept(_typeVisitor);
        // Assert
        Assert.Equal(TypeVal.Error, gThanEqNode.Type);
    }
    
    [Fact]
    public void GThanEqNode_TypeMismatch_IsTypeBoolean()
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var gThanEqNode = new GThanEqNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };
        // Act
        gThanEqNode.Accept(_typeVisitor);
        // Assert
        Assert.Equal(TypeVal.Boolean, gThanEqNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void LThanNode_WrongTypes_IsTypeError(TypeVal type)
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var lThanNode = new LThanNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };

        // Act
        lThanNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Error, lThanNode.Type);
    }
    
    [Fact]
    public void LThanNode_BothNumbers_IsTypeBoolean()
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var lThanNode = new LThanNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };

        // Act
        lThanNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Boolean, lThanNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void LThanEqNode_WrongTypes_IsTypeError(TypeVal type)
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var lThanEqNode = new LThanEqNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };

        // Act
        lThanEqNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Error, lThanEqNode.Type);
    }
    
    [Fact]
    public void LThanEqNode_BothNumbers_IsTypeBoolean()
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var lThanEqNode = new LThanEqNode() {
            Left = new IdNode() {Id = "left"},
            Right = new IdNode() {Id = "right"}
        };

        // Act
        lThanEqNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Boolean, lThanEqNode.Type);
    }
    
    #endregion
    
    #region Declarations
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void VarDclNode_TypeMatch_IsTypeOk(TypeVal type)
    {
    //Arrange
    var symbol = new SymTableEntry() {Type = type};
    _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
        .Returns(symbol);
    var varDclNode = new VarDclNode {
        Left = new IdNode() {Id = "left"},
        Right = new IdNode() {Id = "right"}
    };

    //Act
    varDclNode.Accept(_typeVisitor);

    //Assert
    Assert.Equal(TypeVal.Ok, varDclNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Boolean)]
    [InlineData(TypeVal.String, TypeVal.Number)]
    [InlineData(TypeVal.Boolean, TypeVal.String)]
    public void VarDclNode_TypeMismatch_IsTypeError(TypeVal type1, TypeVal type2)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type1};
        var symbol2 = new SymTableEntry() {Type = type2};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("left"))
        .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
        .Returns(symbol2);
        var varDclNode = new VarDclNode {
        Left = new IdNode() {Id = "left"},
        Right = new IdNode() {Id = "right"}
        };

        //Act
        varDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(TypeVal.Error, varDclNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void VarDclNode_ExprHasError_IsTypeError(TypeVal type)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type};
        var symbol2 = new SymTableEntry() {Type = TypeVal.Error};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("left"))
        .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
        .Returns(symbol2);
        var varDclNode = new VarDclNode {
        Left = new IdNode() {Id = "left"},
        Right = new IdNode() {Id = "right"}
        };

        //Act
        varDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(TypeVal.Error, varDclNode.Type);
    }

    [Fact]
    public void ArrDclNode_TypeMatch_IsTypeOk() {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var arrDclNode = new ArrDclNode() {
            Left = new IdNode() {Id = "left"},
            ArrLitRight = new List<ExprNode>() {new IdNode() {Id = "arrlit1"}}
        };
        
        // Act
        arrDclNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Number, arrDclNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Boolean)]
    [InlineData(TypeVal.String, TypeVal.Number)]
    [InlineData(TypeVal.Boolean, TypeVal.String)]
    public void ArrDclNode_TypeMismatch_IsTypeError(TypeVal type1, TypeVal type2)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type1};
        var symbol2 = new SymTableEntry() {Type = type2};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("left"))
            .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
            .Returns(symbol2);
        var arrDclNode = new ArrDclNode() {
            Left = new IdNode() {Id = "left"},
            ArrLitRight = new List<ExprNode>(){new IdNode(){Id = "right"}}
        };

        //Act
        arrDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(TypeVal.Error, arrDclNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void ArrDclNode_ExprHasError_IsTypeError(TypeVal type)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type};
        var symbol2 = new SymTableEntry() {Type = TypeVal.Error};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("left"))
            .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
            .Returns(symbol2);
        var arrDclNode = new ArrDclNode {
            Left = new IdNode() {Id = "left"},
            ArrLitRight = new List<ExprNode>(){new IdNode() {Id = "right"}}
        };

        //Act
        arrDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(TypeVal.Error, arrDclNode.Type);
    }    
    #endregion
    
    #region Statements
    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Error)]
    [InlineData(TypeVal.Boolean, TypeVal.Ok)]
    [InlineData(TypeVal.String, TypeVal.Error)]
    public void IfNode_ConditionMatches_NodeAccepted(TypeVal type1, TypeVal expectedResult)
    {
        //Arrange
        var symbol = new SymTableEntry() {Type = type1};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("bool"))
        .Returns(symbol);
        var ifNode = new IfNode {
            Condition = new IdNode() {Id = "bool"},
            ThenClause = new List<StmtNode>(){new ContNode()},
            ElseClause = new List<StmtNode>(){new ContNode()}
        };
    
        //Act
        ifNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(expectedResult, ifNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Error)]
    [InlineData(TypeVal.Boolean, TypeVal.Ok)]
    [InlineData(TypeVal.String, TypeVal.Error)]
    public void WhileNode_TypeMatch_TypeOkOrTypeError(TypeVal type, TypeVal expectedResult)
    {
        //Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("cond"))
        .Returns(symbol);
        var whileNode = new WhileNode{
            Condition = new IdNode(){Id = "cond"},
            Body = new List<StmtNode>(){new ContNode()}
        };
        
        //Act
        whileNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(expectedResult, whileNode.Type);
    }

    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Number, TypeVal.Number, TypeVal.Ok)]
    [InlineData(TypeVal.String, TypeVal.Number, TypeVal.Boolean, TypeVal.Error)]
    public void ForNode_TypeMatch_TypeOkOrTypeError(TypeVal type1, TypeVal type2, TypeVal type3, TypeVal type4)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type1};
        var symbol2 = new SymTableEntry() {Type = type2};
        var symbol3 = new SymTableEntry() {Type = type3};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("init"))
            .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("limit"))
            .Returns(symbol2);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("update"))
            .Returns(symbol3);
        var forNode = new ForNode{
            Initializer = new IdNode() {Id = "init"},
            Limit = new IdNode() {Id = "limit"},
            Update = new IdNode() {Id = "update"},
            Body = new List<StmtNode>(){new ContNode()}
        };
        
        //Act
        forNode.Accept(_typeVisitor);
        
        //Assert
        Assert.Equal(type4, forNode.Type);
    }

    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void AssNode_TypeMatch_IsTypeOk(TypeVal type) {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
        .Returns(symbol);
        var assNode = new AssNode() {
        Id = new IdNode() {Id = "left"},
        Expr = new IdNode() {Id = "right"}
        };
        
        // Act
        assNode.Accept(_typeVisitor);
        
        // Assert
        Assert.Equal(TypeVal.Ok, assNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Boolean)]
    [InlineData(TypeVal.String, TypeVal.Number)]
    [InlineData(TypeVal.Boolean, TypeVal.String)]
    public void AssNode_TypeMismatch_IsTypeError(TypeVal type1, TypeVal type2)
    {
        //Arrange
        var symbol1 = new SymTableEntry() {Type = type1};
        var symbol2 = new SymTableEntry() {Type = type2};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("left"))
        .Returns(symbol1);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
        .Returns(symbol2);
        var assNode = new AssNode() {
        Id = new IdNode() {Id = "left"},
        Expr = new IdNode() {Id = "right"}
        };

        //Act
        assNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(TypeVal.Error, assNode.Type);
    }

    #endregion Statements
}