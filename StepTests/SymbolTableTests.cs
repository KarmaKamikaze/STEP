using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using Xunit;

namespace StepTests;

public class SymbolTableTests
{
    private ISymbolTable symbolTable;

    public SymbolTableTests()
    { 
        symbolTable = new SymbolTable();
    }
    
    [Fact]
    public void EnterSymbol_DuplicateDeclaration_ThrowsException()
    {
        // Arrange
        string expectedName = "Teststring";
        Type expectedType = new Type() { ActualType = TypeVal.String };
        var idNode1 = new IdNode { Id = expectedName, Type = expectedType };
        var idNode2 = new IdNode { Id = expectedName, Type = expectedType };
        symbolTable.EnterSymbol(idNode1);
        
        // Act / Assert
        //expect that true is returned on the local declaration, furthermore the 
        //exception should follow when entering the new symbol
        Assert.True(symbolTable.IsDeclaredLocally(expectedName));
        Assert.Throws<DuplicateDeclarationException>(() => symbolTable.EnterSymbol(idNode2));
    }
    
    [Fact]
    public void RetrieveSymbol_OnlyExistsInOuterScope_ReturnsCorrectSymbol() 
    {
        // Arrange
        string expectedName = "Teststring";
        Type expectedType = new Type() {ActualType = TypeVal.String};
        var idNode = new IdNode { Id = expectedName, Type = expectedType };
        symbolTable.EnterSymbol(idNode);
        symbolTable.OpenScope();
        
        // Act
        var result = symbolTable.RetrieveSymbol(expectedName);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedName, result.Name);
    }
    
    [Fact]
    public void RetrieveSymbol_IsRedeclaredInInnerScope_ReturnsInnermostSymbol() 
    {
        // Arrange
        string expectedName = "Teststring";
        Type type = new Type() {ActualType = TypeVal.Boolean};
        var idNode1 = new IdNode { Id = expectedName, Type = type };
        symbolTable.EnterSymbol(idNode1);
        symbolTable.OpenScope();
        
        // Redeclaration of Teststring with another type
        Type expectedType = new Type() {ActualType = TypeVal.Number};
        var idNode2 = new IdNode { Id = expectedName, Type = expectedType };
        symbolTable.EnterSymbol(idNode2);

        // Act
        Type actual = symbolTable.RetrieveSymbol(expectedName).Type;
        
        // Assert
        Assert.Equal(expectedType, actual);
    }
    
    [Fact]
    public void CloseScope_ThenOpenNewScope_HasRemovedAllDclInOldScope()
    {
        // Arrange
        symbolTable.OpenScope();
        string expectedName = "Teststring";
        Type expectedType = new Type() {ActualType = TypeVal.String};
        var idNode = new IdNode { Id = expectedName, Type = expectedType };
        symbolTable.EnterSymbol(idNode);
        symbolTable.CloseScope();
        symbolTable.OpenScope();
        
        // Act
        var result = symbolTable.RetrieveSymbol(expectedName);
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void CloseScope_CloseTheGlobalScope_ThrowsException()
    {
        // Arrange, act & assert
        Assert.Throws<CloseGlobalScopeException>(() => symbolTable.CloseScope());
    }
}