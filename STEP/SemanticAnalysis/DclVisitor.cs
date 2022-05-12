using STEP.AST.Nodes;
using STEP.Symbols;

namespace STEP.SemanticAnalysis;

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