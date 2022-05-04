using STEP.AST;
using STEP.AST.Nodes;

namespace STEP;

public class DclVisitor : TypeVisitor
{
    public DclVisitor(ISymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;

    public override void Visit(IdNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Name);
        if (symbol is null)
        {
            _symbolTable.EnterSymbol(n);
        }
        else
        {
            throw new SymbolNotDeclaredException("The identifier has not been declared in an active scope", n.SourcePosition, n.Name);
        }
    }
}