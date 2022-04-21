using System;
using System.Collections.Generic;
using System.Data;
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
        
        // Intercept symbol table entry request, save locally
       SymTableEntry symbolTableEntry = new();
        _symbolTableMock.Setup(x => x.EnterSymbol(It.IsAny<string>(), It.IsAny<TypeVal>()))
            .Callback<string, TypeVal>((a, b) => symbolTableEntry = new SymTableEntry(){ Name = a, Type = b});
        
        var dclNode = new VarDclNode()
        {
            Left = new IdNode() {Id = id, Type = TypeVal.Number},
            Right = new NumberNode() {Value = 50 }
        };
        var idNode = new IdNode() {Id = id};

        // Act
        _typeVisitor.Visit(dclNode);
        // Intercept symbol table receive request, use local
        _symbolTableMock.Setup(x => x.RetrieveSymbol(id))
            .Returns(symbolTableEntry);
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
    public void UMinusNode_LeftIsNumber_IsTypeNumber()
    {
        // Arrange
        var exprNode = new NumberNode() {Value = 69420};
        var uMinusNode = new UMinusNode() {Left = exprNode};
        
        // Act
        uMinusNode.Accept(_typeVisitor);
        
        // Assert
        Assert.Equal(TypeVal.Number, uMinusNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Error)]
    public void UMinusNode_LeftIsNotNumber_ThrowsTypeException(TypeVal type)
    {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var exprNode = new IdNode() {Id = "val"};
        var uMinusNode = new UMinusNode() {Left = exprNode};
        
        // Act
        var test = () => uMinusNode.Accept(_typeVisitor);
        
        // Assert
        Assert.Throws<TypeException>(test);
    }

    [Fact]
    public void NegNode_ExprIsNotBoolean_ThrowsTypeException()
    {        
        // Arrange
        var exprNode = new NumberNode() {Value = 420};
        var negNode = new NegNode() {Left = exprNode};
        
        // Act
        var test = () => _typeVisitor.Visit(negNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void MultNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(multNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void DivNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(divNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void PowNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(powNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void MinusNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(minusNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void AddNode_EitherOperandIsBoolean_ThrowsTypeException()
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
        var test = () => _typeVisitor.Visit(plusNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void AndNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(andNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void OrNode_InvalidDomain_ThrowsTypeException(TypeVal type)
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
        var test = () => _typeVisitor.Visit(orNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void EqNode_OperandsAreNotSameType_ThrowsTypeException()
    {
        // Arrange
        var eqNode = new EqNode()
        {
            Left = new NumberNode(),
            Right = new StringNode()
        };
        
        // Act
        var test = () => _typeVisitor.Visit(eqNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void NeqNode_OperandsAreNotSameType_ThrowsTypeException()
    {
        // Arrange
        var neqNode = new NeqNode()
        {
            Left = new NumberNode(),
            Right = new StringNode()
        };
        
        // Act
        var test = () => _typeVisitor.Visit(neqNode);
         
        // Assert
        Assert.Throws<TypeException>(test);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void GThanNode_TypeMismatch_ThrowsTypeException(TypeVal type)
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
        var test = () => gThanNode.Accept(_typeVisitor);
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void GThanEqNode_TypeMismatch_ThrowsTypeException(TypeVal type)
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
        var test = () => gThanEqNode.Accept(_typeVisitor);
        // Assert
        Assert.Throws<TypeException>(test);
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
    public void LThanNode_WrongTypes_ThrowsTypeException(TypeVal type)
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
        var test = () => lThanNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
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
    public void LThanEqNode_WrongTypes_ThrowsTypeException(TypeVal type)
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
        var test = () => lThanEqNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
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

    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void ArrayAccessNode_IndexIsNumber_AssignsArrayType(TypeVal type) {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("array"))
            .Returns(symbol);
        var symbol2 = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("index"))
            .Returns(symbol2);
        var arrAccNode = new ArrayAccessNode() {
            Array = new IdNode() {Id = "array"},
            Index = new IdNode() {Id = "index"}
        };
        // Act
        arrAccNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(type, arrAccNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.Error)]
    public void ArrayAccessNode_IndexIsNotNumber_ThrowsTypeException(TypeVal type) {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("array"))
            .Returns(symbol);
        var symbol2 = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("index"))
            .Returns(symbol2);
        var arrAccNode = new ArrayAccessNode() {
            Array = new IdNode() {Id = "array"},
            Index = new IdNode() {Id = "index"}
        };
        // Act
        var test = () => arrAccNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
    }

    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void FuncExprNode_ParamsMatch_IsTypeMatch(TypeVal type) {
        // Arrange
        var symbol = new FunctionSymTableEntry() {
            Type = type,
            Parameters = new Dictionary<string, TypeVal>() {
                {"a", TypeVal.Number},
                {"b", TypeVal.String},
                {"c", TypeVal.Boolean}
            }
        };
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("a"))
            .Returns(new SymTableEntry(){Type = TypeVal.Number});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("b"))
            .Returns(new SymTableEntry(){Type = TypeVal.String});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("c"))
            .Returns(new SymTableEntry(){Type = TypeVal.Boolean});
        var funcExprNode = new FuncExprNode() {
            Id = new IdNode() {Id = "func"},
            Params = new List<ExprNode>() {
                new IdNode() {Id = "a"},
                new IdNode() {Id = "b"},
                new IdNode() {Id = "c"}
            }
        };

        // Act
        funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(type, funcExprNode.Type);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void FuncExprNode_ParamsMismatch_ThrowsTypeException(TypeVal type) {
        // Arrange
        var symbol = new FunctionSymTableEntry() {
            Type = type,
            Parameters = new Dictionary<string, TypeVal>() {
                {"a", TypeVal.Number},
                {"b", TypeVal.String},
                {"c", TypeVal.Boolean}
            }
        };
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("a"))
            .Returns(new SymTableEntry(){Type = TypeVal.Number});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("b"))
            .Returns(new SymTableEntry(){Type = TypeVal.String});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("c"))
            .Returns(new SymTableEntry(){Type = TypeVal.Boolean});
        var funcExprNode = new FuncExprNode() {
            Id = new IdNode() {Id = "func"},
            Params = new List<ExprNode>() {
                new IdNode() {Id = "c"},
                new IdNode() {Id = "a"},
                new IdNode() {Id = "b"}
            }
        };

        // Act
        var test = () => funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
    }

    [Fact]
    public void FuncExprNode_IsNotFunc_ThrowsException() {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        var funcExprNode = new FuncExprNode() {
            Id = new IdNode() {Id = "func"},
        };
        
        // Act
        var action = () => funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<NoNullAllowedException>(action);
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
    _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
        .Returns(symbol);
    var varDclNode = new VarDclNode {
        Left = new IdNode() {
            Id = "left",
            Type = type
        },
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
    public void VarDclNode_TypeMismatch_ThrowsTypeException(TypeVal type1, TypeVal type2)
    {
        //Arrange
        var symbol = new SymTableEntry() {Type = type2};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
            .Returns(symbol);
        var varDclNode = new VarDclNode {
            Left = new IdNode() {
                Id = "left",
                Type = type1
            },
            Right = new IdNode() {Id = "right"}
        };

        //Act
        var test = () => varDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void VarDclNode_ExprHasError_ThrowsTypeException(TypeVal type)
    {
        //Arrange
        var symbol2 = new SymTableEntry() {Type = TypeVal.Error};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("right"))
            .Returns(symbol2);
        var varDclNode = new VarDclNode {
            Left = new IdNode() {
                Id = "left",
                Type = type
            },
            Right = new IdNode() {Id = "right"}
        };

        //Act
        var test = () => varDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }

    [Fact]
    public void ArrDclNode_TypeMatch_IsTypeOk() {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var arrDclNode = new ArrDclNode() {
            Left = new IdNode() {Id = "left"},
            Right = new ArrLiteralNode(){Elements = new List<ExprNode>(){new IdNode(){Id = "right"}}}
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
    public void ArrDclNode_TypeMismatch_ThrowsTypeException(TypeVal type1, TypeVal type2)
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
            Right = new ArrLiteralNode(){Elements = new List<ExprNode>(){new IdNode(){Id = "right"}}}
        };

        //Act
        var test = () => arrDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.Boolean)]
    [InlineData(TypeVal.String)]
    public void ArrDclNode_ExprHasError_ThrowsTypeException(TypeVal type)
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
            Right = new ArrLiteralNode(){Elements = new List<ExprNode>(){new IdNode(){Id = "right"}}}
        };

        //Act
        var test = () => arrDclNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }

    [Fact]
    public void FuncDefNode_IsTypeOk_CorrectlyEnteredInSymbolTable() {
        // Arrange
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(new SymTableEntry() {Type = TypeVal.Number});
        var funcDefNode = new FuncDefNode() {
            Name = new IdNode() {Id = "Add2"},
            Stmts = new List<StmtNode>() {
                new AssNode() {
                    Id = new IdNode(){Id = "a"},
                    Expr = new PlusNode() {
                        Left = new IdNode(){Id = "a", Type = TypeVal.Number},
                        Right = new NumberNode(){Value = 2}
                    }
                },
                new RetNode() {
                    Parent = new FuncDefNode(){ReturnType = new IdNode(){Type = TypeVal.Number}},
                    RetVal = new IdNode(){Id = "return1", Type = TypeVal.Number}
                }
            },
            FormalParams = new Dictionary<IdNode, (TypeVal, bool)>() {
                {new IdNode(){Id = "a"}, (TypeVal.Number, false)}
            },
            ReturnType = new IdNode() {Id = "return2"}
        };

        // Act
        funcDefNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Number, funcDefNode.Type);
        _symbolTableMock.Verify(x => x.EnterSymbol(funcDefNode), Times.Once);
    }
    
    [Fact]
    public void FuncDefNode_IsTypeError_NotEnteredInSymbolTable() {
        // Arrange
        _symbolTableMock.Setup(x => x.RetrieveSymbol("return"))
            .Returns(new SymTableEntry() {Type = TypeVal.Boolean});
        var funcDefNode = new FuncDefNode() {
            Name = new IdNode() {Id = "Add2"},
            Stmts = new List<StmtNode>() {
                new AssNode() {
                    Id = new IdNode(){Id = "a"},
                    Expr = new PlusNode() {
                        Left = new IdNode(){Id = "a", Type = TypeVal.Number},
                        Right = new NumberNode(){Value = 2}
                    }
                },
                new RetNode() {
                    Parent = new FuncDefNode(){ReturnType = new IdNode(){Type = TypeVal.Number}},
                    RetVal = new IdNode(){Id = "a"}
                }
            },
            FormalParams = new Dictionary<IdNode, (TypeVal, bool)>() {
                {new IdNode(){Id = "a"}, (TypeVal.Number, false)}
            },
            ReturnType = new IdNode() {Id = "return"}
        };

        // Act
        var test = () => funcDefNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
        _symbolTableMock.Verify(x => x.EnterSymbol(funcDefNode), Times.Never);
    }
    #endregion
    
    #region Statements
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    public void IfNode_ConditionTypeMismatch_ThrowsTypeException(TypeVal type)
    {
        //Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("bool"))
            .Returns(symbol);
        var ifNode = new IfNode {
            Condition = new IdNode() {Id = "bool"},
            ThenClause = new List<StmtNode>(){new ContNode()},
            ElseClause = new List<StmtNode>(){new BreakNode()}
        };
    
        //Act
        var test = () => ifNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }
    
    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    public void WhileNode_ConditionTypeMismatch_ThrowsTypeException(TypeVal type)
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
        var test = () => whileNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }

    [Theory]
    [InlineData(TypeVal.String, TypeVal.Number, TypeVal.Boolean)]
    public void ForNode_TypeMismatch_ThrowsTypeException(TypeVal type1, TypeVal type2, TypeVal type3)
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
        var test = () => forNode.Accept(_typeVisitor);
        
        //Assert
        Assert.Throws<TypeException>(test);
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
    public void AssNode_TypeMismatch_ThrowsTypeException(TypeVal type1, TypeVal type2)
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
        var test = () => assNode.Accept(_typeVisitor);

        //Assert
        Assert.Throws<TypeException>(test);
    }

    [Theory]
    [InlineData(TypeVal.Number)]
    [InlineData(TypeVal.String)]
    [InlineData(TypeVal.Boolean)]
    public void RetNode_TypeMatch_IsTypeOk(TypeVal type) {
        // Arrange
        var symbol = new SymTableEntry() {Type = type};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var retNode = new RetNode() {
            Parent = new IfNode() {
                Parent = new FuncDefNode() {Type = type},
                
            },
            RetVal = new IdNode() {Id = "type"}
        };
        
        // Act
        retNode.Accept(_typeVisitor);
        
        // Assert
        Assert.Equal(TypeVal.Ok, retNode.Type);
    }

    [Theory]
    [InlineData(TypeVal.Number, TypeVal.Boolean)]
    [InlineData(TypeVal.String, TypeVal.Number)]
    [InlineData(TypeVal.Boolean, TypeVal.String)]
    public void RetNode_TypeMismatch_ThrowsTypeException(TypeVal type1, TypeVal type2) {
        // Arrange
        var symbol = new SymTableEntry() {Type = type2};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(It.IsAny<string>()))
            .Returns(symbol);
        var retNode = new RetNode() {
            Parent = new FuncDefNode() {Type = type1},
            RetVal = new IdNode() {Id = "type2"}
        };
        
        // Act
        var test = () => retNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
    }

    [Fact]
    public void RetNode_NullExprParentIsBlank_IsTypeOk() {
        // Arrange
        var retNode = new RetNode() {
            Parent = new FuncDefNode() {Type = TypeVal.Blank},
            RetVal = null
        };
        
        // Act
        retNode.Accept(_typeVisitor);
        
        // Assert
        Assert.Equal(TypeVal.Ok, retNode.Type);
    }
    
    [Fact]
    public void FuncStmtNode_ParamsMatch_IsTypeMatch() {
        // Arrange
        var symbol = new FunctionSymTableEntry() {
            Type = TypeVal.Blank,
            Parameters = new Dictionary<string, TypeVal>() {
                {"a", TypeVal.Number},
                {"b", TypeVal.String},
                {"c", TypeVal.Boolean}
            }
        };
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("a"))
            .Returns(new SymTableEntry(){Type = TypeVal.Number});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("b"))
            .Returns(new SymTableEntry(){Type = TypeVal.String});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("c"))
            .Returns(new SymTableEntry(){Type = TypeVal.Boolean});
        var funcExprNode = new FuncStmtNode() {
            Id = new IdNode() {Id = "func"},
            Params = new List<ExprNode>() {
                new IdNode() {Id = "a"},
                new IdNode() {Id = "b"},
                new IdNode() {Id = "c"}
            }
        };

        // Act
        funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Equal(TypeVal.Blank, funcExprNode.Type);
    }
    
    [Fact]
    public void FuncStmtNode_ParamsMismatch_ThrowsTypeException() {
        // Arrange
        var symbol = new FunctionSymTableEntry() {
            Type = TypeVal.Blank,
            Parameters = new Dictionary<string, TypeVal>() {
                {"a", TypeVal.Number},
                {"b", TypeVal.String},
                {"c", TypeVal.Boolean}
            }
        };
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        _symbolTableMock.Setup(x => x.RetrieveSymbol("a"))
            .Returns(new SymTableEntry(){Type = TypeVal.Number});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("b"))
            .Returns(new SymTableEntry(){Type = TypeVal.String});
        _symbolTableMock.Setup(x => x.RetrieveSymbol("c"))
            .Returns(new SymTableEntry(){Type = TypeVal.Boolean});
        var funcExprNode = new FuncStmtNode() {
            Id = new IdNode() {Id = "func"},
            Params = new List<ExprNode>() {
                new IdNode() {Id = "c"},
                new IdNode() {Id = "a"},
                new IdNode() {Id = "b"}
            }
        };

        // Act
        var test = () => funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<TypeException>(test);
    }
    
    [Fact]
    public void FuncsTMTNode_IsNotFunc_ThrowsException() {
        // Arrange
        var symbol = new SymTableEntry() {Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol("func"))
            .Returns(symbol);
        var funcExprNode = new FuncStmtNode() {
            Id = new IdNode() {Id = "func"},
        };
        
        // Act
        var test = () => funcExprNode.Accept(_typeVisitor);

        // Assert
        Assert.Throws<NoNullAllowedException>(test);
    }

    #endregion Statements

    #region Traversal

    [Fact]
    public void ASTTraversalTest() {
        // Arrange
        var symbol = new SymTableEntry() {Name = "b", Type = TypeVal.Number};
        _symbolTableMock.Setup(x => x.RetrieveSymbol(symbol.Name)) 
            .Returns(symbol); // Simulate existing number b
        var exprNode = new PlusNode() {
            Left = new NumberNode() {Value = 2},
            Right = new NumberNode() {Value = 5}
        };
        var idNode = new IdNode() {
            Id = "a",
            Type = TypeVal.Number
        };
        var varDcl = new VarDclNode() {
            Left = idNode,
            Right = exprNode
        };
        var boolLeft = new GThanNode() {
            Left = new IdNode(){Id = "b"},
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
        progNode.Accept(_typeVisitor);
        
        // Assert
        // Is the vardcl ok? requires expr node = number, as var id is number
        Assert.Equal(TypeVal.Ok, varDcl.Type);
        Assert.Equal(TypeVal.Number, exprNode.Type);
        
        // Is the ifnode ok? requires condition = bool, andnode exprs = bool 
        Assert.Equal(TypeVal.Ok, ifNode.Type);
        Assert.Equal(TypeVal.Boolean, boolCond.Type);
        Assert.Equal(TypeVal.Boolean, boolLeft.Type);
        Assert.Equal(TypeVal.Boolean, boolRight.Type);
    }
    #endregion
}