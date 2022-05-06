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

    public override void Visit(FuncsNode n)
    {
        foreach (var dcl in n.FuncDcls)
        {
            dcl.Accept(this);
        }
    }

    public override void Visit(VarDclNode n)
    {
        n.Left.Accept(this);
    }

    public override void Visit(VarsNode n)
    {
        foreach (var dcl in n.Dcls)
        {
            dcl.Accept(this);
        }
    }

    public override void Visit(FuncDefNode n)
    {
        n.Type = n.ReturnType;
        _symbolTable.EnterSymbol(n);
    }
}