using STEP.AST.Nodes;

namespace STEP.AST;

/// <summary>
/// An interface for a Symbol Table for use in contextual analysis.
/// </summary>
public interface ISymbolTable {
    
    /// <summary>
    /// Opens a new scope.
    /// </summary>
    void OpenScope();
    
    /// <summary>
    /// Closes the current (innermost) scope.
    /// </summary>
    /// <exception cref="CloseGlobalScopeException">Thrown when the global scope is closed.</exception>
    void CloseScope();
    
    /// <summary>
    /// Retrieves a symbol from any of the currently active scopes, if found.
    /// </summary>
    /// <param name="id">The name of the symbol to retrieve.</param>
    /// <returns>The symbol, if it exists in any of the currently active scopes, otherwise null.</returns>
    SymTableEntry RetrieveSymbol(string id);

    /// <summary>
    /// Enters the given symbol in the current scope if not already declared within the scope.
    /// </summary>
    /// <param name="node">The symbol to enter.</param>
    /// <exception cref="DuplicateDeclarationException">Thrown if the given name is already declared in the current scope.</exception>
    void EnterSymbol(IdNode node);

    /// <summary>
    /// Enters a symbol for the given function definition in the current scope if not already declared within the scope.
    /// </summary>
    /// <param name="node">The function definition node to enter.</param>
    /// <exception cref="DuplicateDeclarationException">Thrown if the given name is already declared in the current scope.</exception>
    void EnterSymbol(FuncDefNode node);
    
    /// <summary>
    /// Tests whether a symbol with the given <paramref name="id"/> exists in the current (innermost) scope.
    /// </summary>
    /// <param name="id">The name of the symbol to test for.</param>
    /// <returns>True if the symbol already exists, otherwise false.</returns>
    bool IsDeclaredLocally(string id);
}