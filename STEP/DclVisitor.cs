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
        _symbolTable.EnterSymbol(n);
    }
}