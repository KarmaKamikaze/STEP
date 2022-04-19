using STEP.AST;
using STEP.AST.Nodes;

namespace STEP; 

public class TypeVisitor : IVisitor {
    public TypeVisitor() {
        _symbolTable = new SymbolTable();
    }

    public TypeVisitor(ISymbolTable symbolTable) {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;
    
    // Logic nodes
    
    public void Visit(AndNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Boolean && n.Right.Type == TypeVal.Boolean) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(OrNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Boolean && n.Right.Type == TypeVal.Boolean) {
                n.Type = TypeVal.Boolean;
        } else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(EqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(NeqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(GThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(GThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(LThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(LThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(NegNode n) {
        n.Left.Accept(this);
        if (n.Left.Type == TypeVal.Boolean) {
            n.Type = TypeVal.Boolean;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }
    
    // Expression nodes

    public void Visit(NumberNode n) {
        n.Type = TypeVal.Number;
    }

    public void Visit(StringNode n) {
        n.Type = TypeVal.String;
    }

    public void Visit(BoolNode n) {
        n.Type = TypeVal.Boolean;
    }

    public void Visit(ArrDclNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        n.Type = (n.Left.Type != n.Right.Type) 
            ? TypeVal.Error 
            : n.Left.Type;
    }

    public void Visit(ArrLiteralNode n)
    {
        // Check if all elements have the same type
        var firstType = n.Elements.FirstOrDefault()?.Type;
        foreach (var expr in n.Elements)
        {
            expr.Accept(this);
            if (expr.Type != firstType)
            {
                n.Type = TypeVal.Error;
                break;
            }
        }
    }

    public void Visit(ArrayAccessNode n) {
        n.Array.Accept(this);
        // Index is expression node, should we "calculate" the expression if possible to check if
        // index is >= 0 and < ArrSize?
        n.Type = n.Array.Type;
    }

    public void Visit(VarDclNode n) {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(AssNode n) { // Add constant check, needs to be in ST (i think)
        AssVisitor assVisitor = new AssVisitor(_symbolTable);
        n.Id.Accept(assVisitor);
        n.Expr.Accept(this);
        if (n.Id.Type == n.Expr.Type) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public virtual void Visit(IdNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        n.Type = symbol?.Type ?? TypeVal.Error;
    }

    public void Visit(PlusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number)
        {
            n.Type = TypeVal.Number;
        }
        else if (n.Left.Type == TypeVal.String || n.Right.Type == TypeVal.String) {
            n.Type = TypeVal.String;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(MinusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Number;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(MultNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Number;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(DivNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Number;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(PowNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == TypeVal.Number && n.Right.Type == TypeVal.Number) {
            n.Type = TypeVal.Number;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(ParenNode n) {
        n.Left.Accept(this);
        n.Type = n.Left.Type;
    }

    public void Visit(UMinusNode n) {
        n.Left.Accept(this);
        if (n.Left.Type == TypeVal.Number) {
            n.Type = TypeVal.Number;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }
    
    // Loop nodes

    public void Visit(WhileNode n) {
        _symbolTable.OpenScope();
        n.Condition.Accept(this);
        if (n.Condition.Type == TypeVal.Boolean) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
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
        if (n.Initializer.Type == TypeVal.Number && n.Limit.Type == TypeVal.Number && n.Update.Type == TypeVal.Number) { // If it's a literal, it's number.. If it's vardcl, it's Ok??? 
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
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
        _symbolTable.EnterSymbol(n.Name.Id, n.Type);
        n.Type = ((AstNode) n.ReturnType).Type; // ?
    }

    public void Visit(FuncExprNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        n.Type = symbol?.Type ?? TypeVal.Error;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type == TypeVal.Error) { // Add param and formal param type comparison later 
                n.Type = TypeVal.Error;
            }
        }
    }

    public void Visit(FuncStmtNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        n.Type = symbol?.Type ?? TypeVal.Error;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type == TypeVal.Error) { // Add param and formal param type comparison later 
                n.Type = TypeVal.Error;
            }
        }
    }

    public void Visit(FuncsNode n) {
        foreach (var funcdcl in n.FuncDcls) {
            funcdcl.Accept(this);
            _symbolTable.EnterSymbol(funcdcl.Name.Id, funcdcl.Type);
        }
    }

    public void Visit(RetNode n) {
        var parentFunc = n.Parent;
        while (parentFunc is not FuncDefNode or null) {
            parentFunc = parentFunc.Parent;
        }

        if (parentFunc.Type == TypeVal.Blank) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.RetVal.Accept(this);
            if (parentFunc.Type == n.RetVal.Type) {
                n.Type = TypeVal.Ok;
            }
            else {
                n.Type = TypeVal.Error;
            }
        }
    }

    public void Visit(IfNode n) {
        n.Condition.Accept(this);
        if (n.Condition.Type == TypeVal.Boolean) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
        }
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
        n.VarsBlock?.Accept(this);
        n.FuncsBlock?.Accept(this);
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