using STEP.AST;
using STEP.AST.Nodes;

namespace STEP; 

public class DclVisitor : TypeVisitor {
    public DclVisitor(ISymbolTable symbolTable) {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;
    public override void Visit(IdNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        if (symbol is null) {
            _symbolTable.EnterSymbol(n);
        }
        else {
            n.Type.ActualType = TypeVal.Error;
        }
    }
}