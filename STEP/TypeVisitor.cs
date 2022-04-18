using STEP.AST;
using STEP.AST.Nodes;

namespace STEP; 

public class TypeVisitor : IVisitor {
    public TypeVisitor() {
        _symbolTable = new SymbolTable();
    }

    public TypeVisitor(ISymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;
    
    // Logic nodes
    
    public void Visit(AndNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean && n.Right.ExprType == TypeVal.Boolean) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(OrNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean && n.Right.ExprType == TypeVal.Boolean) {
                n.ExprType = TypeVal.Boolean;
        } else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(EqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == n.Right.ExprType && n.Left.ExprType != TypeVal.String) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(NeqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == n.Right.ExprType && n.Left.ExprType != TypeVal.String) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(GThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(GThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(LThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(LThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(NegNode n) {
        n.Left.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }
    
    // Expression nodes

    public void Visit(NumberNode n) { }

    public void Visit(StringNode n) { }

    public void Visit(BoolNode n) { }

    public void Visit(ArrDclNode n) {
        n.Left.Accept(this);
        foreach (var node in n.ArrLitRight) {
            node.Accept(this);
            if (node.ExprType != n.Left.Type) {
                n.Type = TypeVal.Error;
            }
        }
    }

    public void Visit(ArrayAccessNode n) {
        n.Array.Accept(this);
        // Index is expression node, should we "calculate" the expression if possible to check if
        // index is >= 0 and < ArrSize?
        n.TypeVal = n.Array.Type;
    }

    public void Visit(VarDclNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.ExprType) {
            n.Type = TypeVal.Ok;
            _symbolTable.EnterSymbol(n.Left.Id, n.Left.Type);
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(AssNode n) {
        n.Expr.Left.Accept(this);
        n.Expr.Right.Accept(this);
        if (n.Id.Type == n.Expr.ExprType) {
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }

    public void Visit(IdNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        n.ExprType = symbol?.Type ?? TypeVal.Error;
    }

    public void Visit(PlusNode n) {
         n.Left.Accept(this);
         n.Right.Accept(this);
         if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number)
         {
             n.ExprType = TypeVal.Number;
         }
         else if (n.Left.ExprType == TypeVal.String || n.Right.ExprType == TypeVal.String) {
             n.ExprType = TypeVal.String;
         }
         else {
             n.ExprType = TypeVal.Error;
         }
    }

    public void Visit(MinusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(MultNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(DivNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(PowNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(ParenNode n) {
        n.Left.Accept(this);
        n.ExprType = n.Left.ExprType;
    }

    public void Visit(UMinusNode n) {
        n.Left.Accept(this);
        if (n.Left.TypeVal == TypeVal.Number) {
            n.TypeVal = TypeVal.Number;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }
    
    // Loop nodes

    public void Visit(WhileNode n) {
        _symbolTable.OpenScope();
        n.Condition.Accept(this);
        if (n.Condition.TypeVal == TypeVal.Boolean) {
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this); // i sure hope dynamic dispatch works monkaW
        }
        _symbolTable.CloseScope();
    }

    public void Visit(ForNode n) {
        _symbolTable.OpenScope();
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this);
        }

        n.Initializer.Accept(this);
        n.Limit.Accept(this);
        n.Update.Accept(this);
        if (n.Initializer.TypeVal == TypeVal.Number && n.Limit.TypeVal == TypeVal.Number && n.Update.TypeVal == TypeVal.Number) { // If it's a constant, it's number.. If it's vardcl, it's Ok??? 
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
        _symbolTable.CloseScope();
    }

    public void Visit(ContNode n) { } // ?

    public void Visit(BreakNode n) { } // ?
    public void Visit(LoopNode n) {
        _symbolTable.OpenScope();
        foreach (var stmt in n.Stmts) {
            stmt.Accept(this);
        }
        _symbolTable.CloseScope();
    }
    
    // General nodes

    public void Visit(FuncDefNode n) {
        n.Name.Accept(this);
        foreach (var stmtNode in n.Stmts) {
            stmtNode.Accept(this);
        }
        n.ReturnType.Accept(this);
        _symbolTable.EnterSymbol(n.Name.Id, n.TypeVal);
        n.TypeVal = n.ReturnType.TypeVal; // ?
    }

    public void Visit(FuncExprNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        n.ExprType = symbol?.Type ?? TypeVal.Error;
    }

    public void Visit(FuncStmtNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        n.TypeVal = symbol?.Type ?? TypeVal.Error;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.ExprType == TypeVal.Error) { // Add param and formal param type comparison later 
                n.TypeVal = TypeVal.Error;
            }
        }
    }

    public void Visit(FuncsNode n) {
        foreach (var funcdcl in n.FuncDcls) {
            funcdcl.Accept(this);
            _symbolTable.EnterSymbol(funcdcl.Name.Id, funcdcl.TypeVal);
        }
    }

    public void Visit(RetNode n) {
        n.RetVal.Accept(this);
        var parentFunc = n.Parent;
        while (parentFunc is not FuncDefNode or null) {
            parentFunc = parentFunc.Parent;
        }

        if (parentFunc.TypeVal == n.RetVal.TypeVal) {
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }

    public void Visit(IfNode n) {
        n.Condition.Accept(this);
        _symbolTable.OpenScope();
        foreach (var node in n.ThenClause) {
            node.Accept(this);
        }
        _symbolTable.CloseScope();
        _symbolTable.OpenScope();
        foreach (var node in n.ElseClause) {
            node.Accept(this);
        }
        _symbolTable.CloseScope();
    }

    public void Visit(VarsNode n) {
        foreach (var node in n.Dcls) {
            node.Accept(this);
            _symbolTable.EnterSymbol(node.Left.Id, node.Type);
        }
    }

    public void Visit(NullNode n) { }

    public void Visit(ProgNode n) {
        n.VarBlock?.Accept(this);
        n.FuncBlock?.Accept(this);
        n.SetupBlock?.Accept(this);
        n.LoopBlock?.Accept(this);
    }

    public void Visit(SetupNode n) {
        _symbolTable.OpenScope();
        foreach (var node in n.Stmts) {
            node.Accept(this);
        }
        _symbolTable.CloseScope();
    }
}