using STEP.AST.Nodes;

namespace STEP.AST;

/// <summary>
/// A stack-based implementation of a Symbol Table for contextual analysis.
/// </summary>
public class SymbolTable : ISymbolTable
{
    private Stack<Dictionary<string, SymTableEntry>> _scopeStack = new();
    private int _depth = 0;

    public SymbolTable() 
    {
        // Push the global scope onto the stack 
        _scopeStack.Push(new Dictionary<string, SymTableEntry>());
        AddStandardEnvironment();
    }

    private void EnterSymbol(SymTableEntry symbol) => _scopeStack.Peek().Add(symbol.Name, symbol);

    private void AddStandardEnvironment()
    {
        // Constants
        EnterSymbol(StandardEnvironment.High);
        EnterSymbol(StandardEnvironment.Low);
        // I/O
        EnterSymbol(StandardEnvironment.DigitalRead);
        EnterSymbol(StandardEnvironment.DigitalWrite);
        EnterSymbol(StandardEnvironment.AnalogRead);
        EnterSymbol(StandardEnvironment.AnalogWrite);
        EnterSymbol(StandardEnvironment.Print);
        // Math
        EnterSymbol(StandardEnvironment.Min);
        EnterSymbol(StandardEnvironment.Max);
        EnterSymbol(StandardEnvironment.Constrain);
        EnterSymbol(StandardEnvironment.Sine);
        EnterSymbol(StandardEnvironment.Cosine);
        EnterSymbol(StandardEnvironment.Squared);
        EnterSymbol(StandardEnvironment.SquareRoot);
        EnterSymbol(StandardEnvironment.Abs);
        EnterSymbol(StandardEnvironment.Power);
        // Random
        EnterSymbol(StandardEnvironment.Random);
        EnterSymbol(StandardEnvironment.RandomSeed);
        // Time
        EnterSymbol(StandardEnvironment.Delay);
    }

    public void OpenScope()
    {
        this._depth++;
        _scopeStack.Push(new Dictionary<string, SymTableEntry>());
    }
    
    public void CloseScope()
    {
        if(this._depth == 0) 
        {
            throw new CloseGlobalScopeException("Cannot close the global scope (at depth 0)");
        }
        this._depth--;
        _scopeStack.Pop();
    }
    
    /// <remarks>In order to search all scopes in the stack, each one is popped and added to a temporary local list.
    /// Afterwards, each scope in this list is pushed back onto the stack to restore the scopes.</remarks>
    public SymTableEntry RetrieveSymbol(string id)
    {
        List<Dictionary<string, SymTableEntry>> scopes = new();
        SymTableEntry output = null;
        
        // Search for the id in each scope, from innermost to outermost
        while(_scopeStack.Count > 0) 
        {
            var scope = _scopeStack.Pop();
            // Save the scope so we can add it back onto the stack
            scopes.Add(scope);
            if(scope.TryGetValue(id, out SymTableEntry value))
            {
                output = value;
                break;
            }
        }

        scopes.Reverse();
        
        // adds the scope from scopes list back in to the scope stack
        foreach(var scope in scopes) 
        {
            _scopeStack.Push(scope);
        }
        return output;
    }
    
    public void EnterSymbol(string name, Type type)
    {
        //exception to check if a symbol is declared locally more than once
        if(IsDeclaredLocally(name))
        {
            throw new DuplicateDeclarationException("An id of this name have already been declared", name);
        }
        var symbolEntry = new SymTableEntry 
        {
            Name = name,
            Type = type
        };
        // Add the symbol to the innermost scope (top of the scopeStack)
        _scopeStack.Peek().Add(name, symbolEntry);
    }

    public void EnterSymbol(FuncDefNode node)
    {
        string name = node.Name.Id;
        //exception to check if a symbol is declared locally more than once
        if(IsDeclaredLocally(name))
        {
            throw new DuplicateDeclarationException("An id of this name have already been declared", name);
        }

        // Convert formal parameters into a dictionary of strings and TypeVals
        var parameters = new Dictionary<string, Type>();
        foreach (var param in node.FormalParams)
        {
            parameters.Add(param.Id, param.Type);
        }
        
        var symbolEntry = new FunctionSymTableEntry() 
        {
            Name = name,
            Type = node.Type,
            Parameters = parameters
        };
        // Add the symbol to the innermost scope (top of the scopeStack)
        _scopeStack.Peek().Add(name, symbolEntry);
    }

    public bool IsDeclaredLocally(string id)
    {
        return _scopeStack.Peek().ContainsKey(id);
    }
}