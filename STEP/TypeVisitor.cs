using System.Data;
using STEP.AST;
using STEP.AST.Nodes;

namespace STEP; 

public class TypeVisitor : IVisitor {
    public TypeVisitor() {
        _symbolTable = new SymbolTable();
        _pinTable = new PinTable();
    }

    public TypeVisitor(ISymbolTable symbolTable) : this() {
        _symbolTable = symbolTable;
    }

    public TypeVisitor(IPinTable pinTable) : this() {
        _pinTable = pinTable;
    }

    public TypeVisitor(ISymbolTable symbolTable, IPinTable pinTable) : this(symbolTable) {
        _pinTable = pinTable;
    }

    private readonly ISymbolTable _symbolTable;
    private readonly IPinTable _pinTable;
    
    // Logic nodes
    
    public void Visit(AndNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
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
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
            n.Type.ActualType = TypeVal.Boolean;
        } 
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(EqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type && !n.Left.Type.IsArray) {
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
        if (n.Left.Type == n.Right.Type && !n.Left.Type.IsArray) {
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
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
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
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
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
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
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
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType) {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Number, n.Left.Type.ActualType, n.Right.Type.ActualType);
        }
    }

    public void Visit(NegNode n) {
        n.Left.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType) {
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
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        n.Type = (n.Left.Type != n.Right.Type)
            ? throw new TypeException($"Type mismatch, type of left: {n.Left.Type}, type of right: {n.Right.Type}")
            : n.Left.Type;
    }

    public void Visit(ArrLiteralNode n)
    {
        // Check if all elements have the same type
        n.Elements.FirstOrDefault()?.Accept(this);
        var firstType = n.Elements.FirstOrDefault()?.Type;
        foreach (var expr in n.Elements.Skip(1))
        {
            expr.Accept(this);
            if (expr.Type == firstType) continue;
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(firstType!.ActualType, expr.Type.ActualType); // Type will never be null here
        }
        n.Type = firstType;
        n.Type.IsArray = true;
    }

    public void Visit(ArrayAccessNode n) {
        n.Array.Accept(this);
        n.Index.Accept(this);
        if (n.Index.Type.ActualType == TypeVal.Number) {
            n.Type = n.Array.Type;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException($"Type mismatch, expected array index to be of type {TypeVal.Number}, actual type is {n.Index.Type}");
        }
        // Index is expression node, should we "calculate" the expression if possible to check if
        // index is >= 0 and < ArrSize?
    }

    public void Visit(VarDclNode n) {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType is TypeVal.Analogpin or TypeVal.Digitalpin) { // Should this be a separate method or PinVisitor?
            int pinVal = (int) ((NumberNode) n.Right).Value;
            switch (n.Left.Type.ActualType) {
                case TypeVal.Analogpin when pinVal is < 0 or > 5:
                    throw new ArgumentOutOfRangeException(nameof(pinVal), "Analog pins must be in range 0-5");
                case TypeVal.Digitalpin when pinVal is < 0 or > 13:
                    throw new ArgumentOutOfRangeException(nameof(pinVal), "Digital pins must be in range 0-13");
                default:
                    _pinTable.RegisterPin(n.Left.Type.ActualType, pinVal);
                    break;
            }
        }
        else {
            if (n.Left.Type == n.Right.Type) {
                n.Type.ActualType = TypeVal.Ok;
            }
            else {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(n.Left.Type.ActualType, n.Right.Type.ActualType);
            }
        }
    }

    public void Visit(AssNode n) { // Add constant check, needs to be in ST (i think)
        AssVisitor assVisitor = new AssVisitor(_symbolTable);
        n.Id.Accept(assVisitor);
        n.Expr.Accept(this);
        if (n.Id.Type.ActualType is TypeVal.Analogpin or TypeVal.Digitalpin) {
            throw new TypeException("Cannot reassign values to pin variables");
        } if (n.Id.Type.IsConstant) { 
            throw new TypeException("Cannot reassign values to constant variables");
        }
        if (n.Id.Type.ActualType == n.Expr.Type.ActualType) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n.Id.Type.ActualType, n.Expr.Type.ActualType);
        }
    }

    public virtual void Visit(IdNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        n.Type = symbol?.Type ?? throw new SymbolNotDeclaredException(n.Id);
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
        n.Type.ActualType = n.Left.Type.ActualType;
    }

    public void Visit(UMinusNode n) {
        n.Left.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number) {
            n.Type.ActualType = TypeVal.Number;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException($"Type mismatch, expected value to be of type {TypeVal.Number}, actual type is {n.Left.Type.ActualType}");
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
            throw new TypeException($"Type mismatch, expected condition to be of type {TypeVal.Boolean}, actual type is {n.Condition.Type.ActualType}");
        }
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this); // i sure hope dynamic dispatch works monkaW
        }
        _symbolTable.CloseScope();
    }

    public void Visit(ForNode n) {
        _symbolTable.OpenScope();
        n.Initializer.Accept(this);
        n.Limit.Accept(this);
        n.Update.Accept(this);
        if (n.Initializer.Type.ActualType == TypeVal.Number && n.Limit.Type.ActualType == TypeVal.Number && n.Update.Type.ActualType == TypeVal.Number) { // If it's a literal, it's number.. If it's vardcl, it's Ok??? 
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException($"Type mismatch, expected for-parameters to be of types (Number, Number, Number), actual types are ({n.Initializer.Type.ActualType}, {n.Limit.Type.ActualType}, {n.Update.Type.ActualType})");
        }
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this);
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
        if (_symbolTable.IsDeclaredLocally(n.Name.Id))
        {
            throw new DuplicateDeclarationException("An id of this name have already been declared", n.Name.Id);
        }

        _symbolTable.OpenScope();
        foreach (var param in n.FormalParams) {
            _symbolTable.EnterSymbol(param.Id, param.Type);
        }
        foreach (var stmtNode in n.Stmts) {
            stmtNode.Accept(this); // Should throw exception if return doesn't match type
        }
        _symbolTable.CloseScope();

        n.Type = n.ReturnType;
        _symbolTable.EnterSymbol(n);
        // bool typeMismatch = false;
        // RetNode offendingTypeRetNode = null;
        // foreach (var retNode in n.Stmts.OfType<RetNode>()) { // Doesn't check nested returns
        //     if (retNode.RetVal.Type.ActualType != n.ReturnType.Type.ActualType) {
        //         typeMismatch = true;
        //         offendingTypeRetNode = retNode;
        //     }
        // }
        //
        // if (!typeMismatch) {
        //     n.Type = n.ReturnType.Type.ActualType;
        //     _symbolTable.EnterSymbol(n);
        // }
        // else {
        //     n.Type = TypeVal.Error;
        //     throw new TypeException($"Type mismatch, expected returns to be of type {n.ReturnType.ActualType}, actual type is {offendingTypeRetNode.Type.ActualType}");
        // }
    }

    public void Visit(FuncExprNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id) as FunctionSymTableEntry;
        if (symbol is null) { // Should this be here?
            throw new NoNullAllowedException("The retrieved symbol was not a function symbol table entry");
        }
        n.Type = symbol.Type;
        var parameterTypes = symbol.Parameters.Values.ToArray();
        int i = 0;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error) {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException($"Type mismatch, expected parameter {i} to be of type {parameterTypes[i]}, actual type is {param.Type.ActualType}");
            }
            i++;
        }
    }

    public void Visit(FuncStmtNode n) {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id) as FunctionSymTableEntry;
        if (symbol is null) { // Should this be here?
            throw new NoNullAllowedException("The retrieved symbol was not a function symbol table entry");
        }
        n.Type = symbol.Type;
        var parameterTypes = symbol.Parameters.Values.ToArray();
        int i = 0;
        foreach (var param in n.Params) {
            param.Accept(this);
            if (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error) {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException($"Type mismatch, expected parameter {i} to be of type {parameterTypes[i]}, actual type is {param.Type.ActualType}");
            }
            i++;
        }
    }

    public void Visit(FuncsNode n) {
        foreach (var funcdcl in n.FuncDcls) {
            funcdcl.Accept(this);
        }
    }

    public void Visit(RetNode n) {
        if (n.SurroundingFuncType.ActualType == TypeVal.Blank) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.RetVal.Accept(this);
            if (n.SurroundingFuncType == n.RetVal.Type) {
                n.Type.ActualType = TypeVal.Ok;
            }
            else {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException($"Type mismatch, expected return value to be of type {n.SurroundingFuncType}, actual type is {n.RetVal.Type}");
            }
        }
    }

    public void Visit(IfNode n) {
        n.Condition.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Condition.Type == expectedType) {
            n.Type.ActualType = TypeVal.Ok;
        }
        else {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException($"Type mismatch, expected condition to be of type {TypeVal.Boolean}, actual type is {n.Condition.Type}");
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

    public void Visit(ElseIfNode n)
    {
        n.Condition.Accept(this);
        if (n.Condition.Type.ActualType != TypeVal.Boolean)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(TypeVal.Boolean, n.Condition.Type.ActualType);
        }
        n.Type.ActualType = TypeVal.Ok;
        _symbolTable.OpenScope();
        foreach (var stmt in n.Body)
        {
            stmt.Accept(this);
        }
        _symbolTable.CloseScope();
    }

    public void Visit(VarsNode n) {
        foreach (var node in n.Dcls) {
            node.Accept(this);
            _symbolTable.EnterSymbol(node.Left.Id, node.Type);
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