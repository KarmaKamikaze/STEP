using STEP.AST.Nodes;
using STEP.Exceptions;
using STEP.Symbols;

namespace STEP.SemanticAnalysis;

public class AssVisitor : TypeVisitor
{
    public AssVisitor(ISymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;

    public override void Visit(IdNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Name);
        if (symbol is not null)
        {
            n.Type = symbol.Type;
        }
        else
        {
            throw new SymbolNotDeclaredException("The identifier has not been declared in an active scope", n.SourcePosition, n.Name);
        }
    }
}