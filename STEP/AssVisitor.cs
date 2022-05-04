using STEP.AST;
using STEP.AST.Nodes;

namespace STEP;

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
            n.Type.ActualType = TypeVal.Error;
        }
    }
}