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
        Type expectedType = new Type(){ActualType = TypeVal.String};
        symbolTable.EnterSymbol(expectedName, expectedType);
        
        // Act / Assert
        //expect that true is returned on the local declaration, furthermore the 
        //exception should follow when entering the new symbol
        Assert.True(symbolTable.IsDeclaredLocally(expectedName));
        Assert.Throws<DuplicateDeclarationException>(() => symbolTable.EnterSymbol(expectedName, expectedType));
    }
    
    [Fact]
    public void RetrieveSymbol_OnlyExistsInOuterScope_ReturnsCorrectSymbol() 
    {
        // Arrange
        string expectedName = "Teststring";
        Type expectedType = new Type() {ActualType = TypeVal.String};
        symbolTable.EnterSymbol(expectedName, expectedType);
        symbolTable.OpenScope();
        
        //Act
        var result = symbolTable.RetrieveSymbol(expectedName);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedName, result.Name);
    }
    
    [Fact]
    public void RetrieveSymbol_IsRedeclaredInInnerScope_ReturnsInnermostSymbol() 
    {
        // Arrange
        string expectedName = "Teststring";
        Type type = new Type() {ActualType = TypeVal.Boolean};
        symbolTable.EnterSymbol(expectedName, type);
        symbolTable.OpenScope();
        
        // Redeclaration of Teststring with another type
        Type expectedType = new Type() {ActualType = TypeVal.Number};
        symbolTable.EnterSymbol(expectedName, expectedType);

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
        symbolTable.EnterSymbol(expectedName, expectedType);
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