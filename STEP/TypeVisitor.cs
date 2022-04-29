using STEP.AST;
using STEP.AST.Nodes;

namespace STEP;

public class TypeVisitor : IVisitor
{
    public TypeVisitor()
    {
        _symbolTable = new SymbolTable();
        _pinTable = new PinTable();
    }

    public TypeVisitor(ISymbolTable symbolTable) : this()
    {
        _symbolTable = symbolTable;
    }

    private readonly ISymbolTable _symbolTable;
    private readonly IPinTable _pinTable;
    private int _scopeLevel = 0;

    private void EnterScope() {
        _symbolTable.OpenScope();
        _scopeLevel++;
    }

    private void ExitScope() {
        _scopeLevel--;
        _symbolTable.CloseScope();
    }

    public void Visit(AndNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Boolean}, 
                                  n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(OrNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Boolean}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(EqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type && !n.Left.Type.IsArray)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(NeqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type && !n.Left.Type.IsArray)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(GThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(GThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(LThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(LThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type == expectedType && n.Right.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(NegNode n)
    {
        n.Left.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Boolean;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Boolean}, n.Left.Type);
        }
    }

    public void Visit(NumberNode n)
    {
        n.Type.ActualType = TypeVal.Number;
    }

    public void Visit(StringNode n)
    {
        n.Type.ActualType = TypeVal.String;
    }

    public void Visit(BoolNode n)
    {
        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(ArrDclNode n)
    {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Left.Type.ArrSize = n.Size;
        n.Type = n.Left.Type;
        if (n.Right is null) return;
        n.Right.Type = n.Left.Type;
        n.Right.Accept(this);
        if (n.Right is IdNode idRight && idRight.Type.IsArray) {
            if (n.Left.Type.ArrSize < idRight.Type.ArrSize) {
                 throw new TypeException(n, 
                     $"Array size mismatch, {n.Left.Id} can only fit {n.Left.Type.ArrSize} elements. "+
                       $"{idRight.Id} has {idRight.Type.ArrSize} elements");
            }
        }
        if (n.Left.Type != n.Right.Type)
            throw new TypeException(n, $"Type mismatch, type of left: {n.Left.Type}, type of right: {n.Right.Type}");
    }

    public void Visit(ArrLiteralNode n)
    {
        // Check if all elements have the same type
        if (!n.Elements.Any()) return;
        if (n.Elements.Count != n.ExpectedSize) throw new ParameterMismatchException(n);
        foreach (var expr in n.Elements)
        {
            expr.Accept(this);
            if (expr.Type.ActualType == n.Type.ActualType) continue;
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                $"Type mismatch, Array type is {n.Type.ActualType}, "+
                  $"element type was {expr.Type.ActualType}"); // Type will never be null here
        }
    }

    public void Visit(ArrayAccessNode n)
    {
        n.Array.Accept(this);
        n.Index.Accept(this);
        if (n.Index.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = n.Array.Type.ActualType;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                $"Type mismatch, expected array index to be of type {TypeVal.Number},"+
                  $" actual type is {n.Index.Type}");
        }
        // Index is expression node, should we "calculate" the expression if possible to check if
        // index is >= 0 and < ArrSize?
    }

    public void Visit(VarDclNode n)
    {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.Type)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(PinDclNode n)
    {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        int pinVal = (int) ((NumberNode) n.Right).Value;
        switch (n.Left.Type.ActualType)
        {
            case TypeVal.Analogpin when pinVal is < 0 or > 5:
                n.Type.ActualType = TypeVal.Error;
                throw new ArgumentOutOfRangeException(nameof(pinVal), "Analog pins must be in range 0-5");
            case TypeVal.Digitalpin when pinVal is < 0 or > 13:
                n.Type.ActualType = TypeVal.Error;
                throw new ArgumentOutOfRangeException(nameof(pinVal), "Digital pins must be in range 0-13");
            default:
                _pinTable.RegisterPin(n.Left.Type.ActualType, pinVal);
                n.Type.ActualType = TypeVal.Ok;
                break;
        }
    }

    public void Visit(AssNode n)
    {
        // Add constant check, needs to be in ST (i think)
        AssVisitor assVisitor = new AssVisitor(_symbolTable);
        n.Id.Accept(assVisitor);
        n.Expr.Accept(this);
        // Prevent copying larger array into smaller
        if (n.Id.Type.IsArray && n.Expr.Type.IsArray) {
            if (n.Id.Type.ArrSize < n.Expr.Type.ArrSize) {
                throw new TypeException(n, 
                    $"Array size mismatch, {n.Id} can only fit {n.Id.Type.ArrSize} elements. " +
                      $"{((IdNode)n.Expr).Id} has {n.Expr.Type.ArrSize} elements");
            }
        }
        if (n.Id.Type.ActualType is TypeVal.Analogpin or TypeVal.Digitalpin)
        {
            throw new TypeException(n, "Cannot reassign values to pin variables");
        }

        if (n.Id.Type.IsConstant)
        {
            throw new TypeException(n, "Cannot reassign values to constant variables");
        }

        if (n.Id.Type.ActualType == n.Expr.Type.ActualType)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, n.Id.Type, n.Expr.Type);
        }
    }

    public virtual void Visit(IdNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Id);
        n.Type = symbol?.Type ?? throw new SymbolNotDeclaredException(n.Id);
    }

    public void Visit(PlusNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else if (n.Left.Type.ActualType == TypeVal.String || n.Right.Type.ActualType == TypeVal.String)
        {
            n.Type.ActualType = TypeVal.String;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number},
                new Type() {ActualType = TypeVal.String}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(MinusNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(MultNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(DivNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(PowNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number && n.Right.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Number}, n.Left.Type, n.Right.Type);
        }
    }

    public void Visit(ParenNode n)
    {
        n.Left.Accept(this);
        n.Type.ActualType = n.Left.Type.ActualType;
    }

    public void Visit(UMinusNode n)
    {
        n.Left.Accept(this);
        if (n.Left.Type.ActualType == TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Number;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                $"Type mismatch, expected value to be of type {TypeVal.Number}, "+
                  $"actual type is {n.Left.Type.ActualType}");
        }
    }

    public void Visit(WhileNode n)
    {
        EnterScope();
        n.Condition.Accept(this);
        if (n.Condition.Type.ActualType == TypeVal.Boolean)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                $"Type mismatch, expected condition to be of type {TypeVal.Boolean},"+
                  $" actual type is {n.Condition.Type.ActualType}");
        }

        foreach (var stmtNode in n.Body)
        {
            stmtNode.Accept(this);
            stmtNode.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();
    }

    public void Visit(ForNode n)
    {
        EnterScope();
        n.Initializer.Accept(this);
        n.Limit.Accept(this);
        n.Update.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        var initNode = n.Initializer;
        if (initNode is VarDclNode vn)
        {
            // VarDclNodes will have Type OK if they do not have errors - In this case, grab type from the Id
            initNode = vn.Left;
        }

        if (initNode.Type == expectedType && n.Limit.Type == expectedType && n.Update.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                "Type mismatch, expected for-parameters to be of types (Number, Number, Number),"+
                   $" actual types are ({n.Initializer.Type.ActualType}, {n.Limit.Type.ActualType},"+
                   $" {n.Update.Type.ActualType})");
        }

        foreach (var stmtNode in n.Body)
        {
            stmtNode.Accept(this);
            stmtNode.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();
    }

    public void Visit(ContNode n) { }
    public void Visit(BreakNode n) { }
    
    public void Visit(LoopNode n) {
        EnterScope();
        foreach (var stmt in n.Stmts)
        {
            stmt.Accept(this);
            stmt.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();
    }

    public void Visit(FuncDefNode n)
    {
        if (_symbolTable.IsDeclaredLocally(n.Name.Id))
        {
            throw new DuplicateDeclarationException("An id of this name have already been declared", n.Name.Id);
        }

        EnterScope();
        foreach (var param in n.FormalParams)
        {
            _symbolTable.EnterSymbol(param.Id, param.Type);
        }

        foreach (var stmtNode in n.Stmts)
        {
            stmtNode.Accept(this); // Should throw exception if return doesn't match type
            stmtNode.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();

        n.Type = n.ReturnType;
        _symbolTable.EnterSymbol(n);
    }

    public void Visit(FuncExprNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        if (symbol is not FunctionSymTableEntry funcEntry)
        {
            // FuncEntry is now symbol as FunctionSymTableEntry
            if (symbol is null)
            {
                throw new TypeException(n, $"The symbol {n.Id.Id} does not exist in any current scope");
            }

            throw new TypeException(n, $"The retrieved symbol {n.Id.Id} was not a function symbol table entry");
        }

        n.Type = funcEntry.Type;
        var parameterTypes = funcEntry.Parameters.Values.ToArray();
        var i = 0;
        if (funcEntry.Parameters.Count != n.Params.Count) throw new ParameterMismatchException(n, funcEntry);
        foreach (var param in n.Params)
        {
            param.Accept(this);
            if (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error)
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(n,
                    $"Type mismatch, expected parameter {i} to be of type {parameterTypes[i]},"+
                      $" actual type is {param.Type.ActualType}");
            }

            i++;
        }
    }

    public void Visit(FuncStmtNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Id);
        if (symbol is not FunctionSymTableEntry funcEntry)
        {
            // FuncEntry is now symbol as FunctionSymTableEntry
            if (symbol is null)
            {
                throw new TypeException(n, $"The symbol {n.Id.Id} does not exist in any current scope");
            }

            throw new TypeException(n, $"The retrieved symbol {n.Id.Id} was not a function symbol table entry");
        }

        n.Type = funcEntry.Type;
        var parameterTypes = funcEntry.Parameters.Values.ToArray();
        var i = 0;
        if (funcEntry.Parameters.Count != n.Params.Count) throw new ParameterMismatchException(n, funcEntry);
        foreach (var param in n.Params)
        {
            param.Accept(this);
            if (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error)
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(n,
                    $"Type mismatch, expected parameter {i} to be of type {parameterTypes[i]}, "+
                      $"actual type is {param.Type.ActualType}");
            }

            i++;
        }
    }

    public void Visit(FuncsNode n)
    {
        foreach (var funcDcl in n.FuncDcls)
        {
            funcDcl.Accept(this);
        }
    }

    public void Visit(RetNode n)
    {
        if (n.SurroundingFuncType.ActualType == TypeVal.Blank)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.RetVal.Accept(this);
            // Mark arrays that are returned from functions for later
            if (n.RetVal.Type.IsArray) {
                n.RetVal.Type.IsReturned = true;
            }
            if (n.SurroundingFuncType == n.RetVal.Type)
            {
                n.Type.ActualType = TypeVal.Ok;
            }
            else
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeException(n,
                    $"Type mismatch, expected return value to be of type {n.SurroundingFuncType}, "+
                      $" actual type is {n.RetVal.Type}");
            }
        }
    }

    public void Visit(IfNode n)
    {
        n.Condition.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Condition.Type == expectedType)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n,
                $"Type mismatch, expected condition to be of type {TypeVal.Boolean}, "+
                  $"actual type is {n.Condition.Type}");
        }

        EnterScope();
        foreach (var node in n.ThenClause)
        {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }
        ExitScope();
        
        foreach (var node in n.ElseIfClauses) {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }

        EnterScope();
        foreach (var node in n.ElseClause)
        {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }
        ExitScope();
        
        
    }

    public void Visit(ElseIfNode n)
    {
        n.Condition.Accept(this);
        if (n.Condition.Type.ActualType != TypeVal.Boolean)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeException(n, new Type() {ActualType = TypeVal.Boolean}, n.Condition.Type);
        }

        n.Type.ActualType = TypeVal.Ok;
        
        EnterScope();
        foreach (var stmt in n.Body)
        {
            stmt.Accept(this);
            stmt.Type.ScopeLevel = _scopeLevel;
        }
        ExitScope();
    }

    public void Visit(VarsNode n)
    {
        foreach (var node in n.Dcls)
        {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }
    }

    public void Visit(ProgNode n)
    {
        n.VarsBlock?.Accept(this);
        n.FuncsBlock?.Accept(this);
        n.SetupBlock?.Accept(this);
        n.LoopBlock?.Accept(this);
    }

    public void Visit(SetupNode n)
    {
        EnterScope();
        foreach (var node in n.Stmts)
        {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();
    }
}