using System.Data;
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
        if (n.Left.Type.ActualType == TypeVal.Boolean && n.Right.Type.ActualType == TypeVal.Boolean) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(OrNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Boolean && n.Right.Type.ActualType == TypeVal.Boolean) {
                n.Type.ActualType = TypeVal.Boolean;
        } else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(EqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(NeqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(GThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(GThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(LThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(LThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(NegNode n) {
        n.Left.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Boolean) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Left.Type.ActualType);
        }
    }
    
    // Expression nodes

    public void Visit(NumberNode n) {
        n.Type.ActualType = TypeVal.Number;
    }

    public void Visit(StringNode n) {
        n.Type.ActualType = TypeVal.String;
    }

    public void Visit(BoolNode n) {
        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(ArrDclNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        n.Type = (n.Left.Type.ActualType != n.Right.Type.ActualType) 
            ? throw new TypeException(n.Left.Type.ActualType, n.Right.Type.ActualType)
            : n.Left.Type;
    }

    public void Visit(ArrLiteralNode n)
    {
        // Check if all elements have the same type
        n.Elements.FirstOrDefault()?.Accept(this);
        var firstType = n.Elements.FirstOrDefault()?.Type.ActualType;
        foreach (var expr in n.Elements.Skip(1))
        {
            expr.Accept(this);
            if (expr.Type.ActualType != firstType)
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(firstType!.Value, expr.Type.ActualType);
            }
        }

        n.Type.ActualType = (TypeVal) firstType; // Hmm
    }

    public void Visit(ArrayAccessNode n) {
        n.Array.Accept(this);
        n.Index.Accept(this);
        if (n.Index.Type.ActualType == TypeVal.Number) {
            n.Type = n.Array.Type;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Index);
        }
        // Index is expression node, should we "calculate" the expression if possible to check if
        // index is >= 0 and < ArrSize?
    }

    public void Visit(VarDclNode n) {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == n.Right.Type.ActualType) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(AssNode n) { // Add constant check, needs to be in ST (i think)
        AssVisitor assVisitor = new AssVisitor(_symbolTable);
        n.Id.Accept(assVisitor);
        n.Expr.Accept(this);
        if (n.Id.Type == n.Expr.Type) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.Id.Type.ActualType, n.Expr.Type.ActualType);
        }
    }

    public virtual void Visit(IdNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        n.Type.ActualType = symbol?.Type ?? throw new TypeException(n.Id);
    }

    public void Visit(PlusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else if (n.Left.Type.ActualType == TypeVal.String || n.Right.Type.ActualType == TypeVal.String) {
            n.Type.ActualType = TypeVal.String;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, TypeVal.String, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(MinusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(MultNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(DivNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(PowNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(ParenNode n) {
        n.Left.Accept(this);
        n.Type = n.Left.Type;
    }

    public void Visit(UMinusNode n) {
        n.Left.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left);
        }
    }
    
    // Loop nodes

    public void Visit(WhileNode n) {
        _symbolTable.OpenScope();
        n.Condition.Accept(this);
        if (n.Condition.Type.ActualType == TypeVal.Boolean) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n);
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
        if (n.Initializer.Type.ActualType == TypeVal.Number && n.Limit.Type.ActualType == TypeVal.Number && n.Update.Type.ActualType == TypeVal.Number) { // If it's a literal, it's number.. If it's vardcl, it's Ok??? 
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Initializer.Type.ActualType, n.Limit.Type.ActualType, n.Update);
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
        bool typeMismatch = false;
        RetNode offendingTypeRetNode = null;
        foreach (var retNode in n.Stmts.OfType<RetNode>()) {
            if (retNode.RetVal.Type != n.ReturnType.Type) {
                typeMismatch = true;
                offendingTypeRetNode = retNode;
            }
        }

        if (!typeMismatch) {
            n.Type = n.ReturnType.Type;
            _symbolTable.EnterSymbol(n);
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.ReturnType.Type.ActualType, offendingTypeRetNode.RetVal);
        }
    }

    public void Visit(FuncExprNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id) as FunctionSymTableEntry;
        if (symbol is null) { // Should this be here?
            throw new NoNullAllowedException("The retrieved symbol was not a function symbol table entry");
        }
        n.Type.ActualType = symbol.Type;
        var parameterTypes = symbol.Parameters.Values.ToArray();
        int i = 0;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type.ActualType != parameterTypes[i] || param.Type.ActualType == TypeVal.Error) {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(parameterTypes[i], param);
            }
            i++;
        }
    }

    public void Visit(FuncStmtNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id) as FunctionSymTableEntry;
        if (symbol is null) { // Should this be here?
            throw new NoNullAllowedException("The retrieved symbol was not a function symbol table entry");
        }
        n.Type.ActualType = symbol.Type;
        var parameterTypes = symbol.Parameters.Values.ToArray();
        int i = 0;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type.ActualType != parameterTypes[i] || param.Type.ActualType == TypeVal.Error) {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(parameterTypes[i], param);
            }
            i++;
        }
    }

    public void Visit(FuncsNode n) {
        foreach (var funcdcl in n.FuncDcls) {
            funcdcl.Accept(this);
            _symbolTable.EnterSymbol(funcdcl.Name.Id, funcdcl.Type.ActualType);
        }
    }

    public void Visit(RetNode n) {
        var parentFunc = n.Parent;
        while (parentFunc is not FuncDefNode or null) {
            parentFunc = parentFunc.Parent;
        }
        if (parentFunc.Type.ActualType == TypeVal.Blank) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.RetVal.Accept(this);
            if (parentFunc.Type == n.RetVal.Type) {
                n.Type.ActualType = TypeVal.Ok;
            }
            else {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(parentFunc.Type.ActualType, n.RetVal);
            }
        }
    }

    public void Visit(IfNode n) {
        n.Condition.Accept(this);
        if (n.Condition.Type.ActualType == TypeVal.Boolean) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Condition);
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
            _symbolTable.EnterSymbol(node.Left.Id, node.Type.ActualType);
        }
    }

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