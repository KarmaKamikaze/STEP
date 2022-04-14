using STEP.AST.Nodes;

namespace STEP.AST;
//THe Symbol Table for the AST

class SymTableEntry
{
    public string symbol_name { get; set; }
    public TypeVal Type { get; set; }
}

public interface ISymbolTable {
    void OpenScope();
    void CloseScope();
    SymTableEntry RetriveSymbol(string id);
    void EnterSymbol(SymTableEntry sym);
    bool IsDeclaredLocally(string id);
}

public class SymbolTable
{
    private Dictionary<string, List<SymTableEntry>> symbolDict = new();
    private Stack<Dictionary<string, SymTableEntry>> scopeStack = new();
    private int depth = 0;

    public void OpenScope()
    {
        this.depth++;
        scopeStack.Push(new SymbolTable());
    }

    public void CloseScope()
    {
        var prevSymTable = scopeStack.Pop();
        foreach(var sym in prevSymTable) 
        {
            
        }
        
        SymTableEntry previousSymbol;
        foreach(var symbol in scopeDisplay[depth])
        {
            previousSymbol = symbol.var;
            Delete(symbol);
            if (previousSymbol != null)
            {
                Add(previousSymbol);
            }
        }
        this.depth--;
    }
    
    private void Add(SymTableEntry sym) 
    {
        if (tableDict.GetValueOrDefault(sym.var) is List<SymTableEntry> collisionChain)
        {
            collisionChain.Add(sym);
        }
    }

    private void Delete(SymTableEntry sym)
    {
        if (tableDict.GetValueOrDefault(sym.var) is List<SymTableEntry> collisionChain)
        {
            collisionChain.Remove(sym);
        }
    }
    public SymTableEntry RetriveSymbol(string id)
    {
        //symbol = HashTable.get(name)
        //while sumbol not null do
            //if symbol.name == name
                //then return symbol
                //symbol = symbol.hash
        return null;
    }

    public void EnterSymbol(SymTableEntry sym)
    {
        // Ét symbol table: 
        // - Level som attribute på SymTableEntry
        // - Values i vores Dictionary er en liste af SymTableEntries med forskellige scopes (og samme id)
        
        // ét symbol table pr. level:
        // - 
    }

    public IsDeclaredLocally(string id)
    {
        //if declaredlocally
            //return true
        //elif
            //return false
    }
}