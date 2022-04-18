using System;
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
            Condition = new IdNode() {Id = "bool"}
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
            Condition = new IdNode(){Id = "cond"}
        };
        
        //Act
        whileNode.Accept(_typeVisitor);

        //Assert
        Assert.Equal(expectedResult, whileNode.Type);
    }

    // [Theory]
    // [InlineData(TypeVal.Number, TypeVal.Number, TypeVal.Number, TypeVal.Ok)]
    // [InlineData(TypeVal.String, TypeVal.Number, TypeVal.Boolean, TypeVal.Error)]
    // public void ForNode_TypeMatch_TypeOkOrTypeError(TyTypeVal type1, TypeVal type2, TypeVal type3, TypeVal type4)
    // {
    //     //Arrange
    //     var forNode = new ForNode{
    //         Initializer = { }
    //     };
    //     //Act
    //     
    //     //Assert
    // }

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