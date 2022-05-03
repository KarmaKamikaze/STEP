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

    private void EnterScope()
    {
        _symbolTable.OpenScope();
        _scopeLevel++;
    }

    private void ExitScope()
    {
        _scopeLevel--;
        _symbolTable.CloseScope();
    }

    public void Visit(AndNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in boolean expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in boolean expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(OrNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Boolean};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in boolean expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in boolean expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
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
            throw new TypeMismatchException("Operands in an equality check must be of the same type", n.SourcePosition,
                n.Right.Type, n.Left.Type);
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
            throw new TypeMismatchException("Operands in an equality check must be of the same type", n.SourcePosition,
                n.Right.Type, n.Left.Type);
        }
    }

    public void Visit(GThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Left.SourcePosition, n.Left.Type,
                expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Right.SourcePosition, n.Right.Type,
                expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(GThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Left.SourcePosition, n.Left.Type,
                expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Right.SourcePosition, n.Right.Type,
                expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(LThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Left.SourcePosition, n.Left.Type,
                expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Right.SourcePosition, n.Right.Type,
                expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
    }

    public void Visit(LThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type() {ActualType = TypeVal.Number};
        if (n.Left.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Left.SourcePosition, n.Left.Type,
                expectedType);
        }
        else if (n.Right.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Incompatible types in expression", n.Right.SourcePosition, n.Right.Type,
                expectedType);
        }

        n.Type.ActualType = TypeVal.Boolean;
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
            throw new TypeMismatchException("Invalid type in boolean expression", n.Left.SourcePosition, n.Left.Type,
                expectedType);
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
        if (n.Right is IdNode idRight && idRight.Type.IsArray)
        {
            if (n.Left.Type.ArrSize < idRight.Type.ArrSize)
            {
                throw new ArraySizeMismatchException("The provided array sizes are incompatible", n.SourcePosition,
                    n.Left, idRight);
            }
        }

        if (n.Left.Type != n.Right.Type)
            throw new TypeMismatchException("The element types are not compatible with the declared array type",
                n.Right.SourcePosition, n.Right.Type, n.Left.Type);
    }

    public void Visit(ArrLiteralNode n)
    {
        // Check if all elements have the same type
        if (!n.Elements.Any()) return;
        if (n.Elements.Count > n.ExpectedSize)
            throw new ParameterCountMismatchException("Unexpected number of parameters", n.SourcePosition,
                n.ExpectedSize, n.Elements.Count);
        foreach (var expr in n.Elements)
        {
            expr.Accept(this);
            if (expr.Type.ActualType == n.Type.ActualType) continue;
            // TODO: If we change the actual type to an error, we ruin the error message.
            // The exception also stops compilation, so the error type is not needed here.
            //n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected element type in array declaration", expr.SourcePosition,
                expr.Type, n.Type);
        }

        n.Type.ArrSize = n.Elements.Count;
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
            throw new TypeMismatchException("Unexpected type for array indexing", n.Index.SourcePosition, n.Index.Type,
                new Type {ActualType = TypeVal.Number});
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
            throw new TypeMismatchException("The assigned value is incompatible with the declared type",
                n.Right.SourcePosition, n.Right.Type, n.Left.Type);
        }
    }

    public void Visit(PinDclNode n)
    {
        DclVisitor dclVisitor = new DclVisitor(_symbolTable);
        n.Left.Accept(dclVisitor);
        n.Right.Accept(this);
        int pinVal = Convert.ToInt32((n.Right as NumberNode).Value);
        switch (n.Left.Type.ActualType)
        {
            case TypeVal.Analogpin when pinVal is < 0 or > 5:
                n.Type.ActualType = TypeVal.Error;
                throw new ArgumentOutOfRangeException(nameof(pinVal), "Analog pins must be in range 0-5");
            case TypeVal.Digitalpin when pinVal is < 0 or > 13:
                n.Type.ActualType = TypeVal.Error;
                throw new ArgumentOutOfRangeException(nameof(pinVal), "Digital pins must be in range 0-13");
            default:
                _pinTable.RegisterPin(n);
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
        if (n.Id.Type.IsArray && n.Expr.Type.IsArray)
        {
            if (n.Id.Type.ArrSize < n.Expr.Type.ArrSize)
            {
                throw new ArraySizeMismatchException(
                    "The left-hand-side array is not large enough to hold the assigned values", n.SourcePosition, n.Id,
                    n.Expr as IdNode);
            }
        }

        if (n.Id.Type.ActualType is TypeVal.Analogpin or TypeVal.Digitalpin)
        {
            throw new TypeMismatchException("Cannot reassign values to pin variables", n.SourcePosition);
        }

        if (n.Id.Type.IsConstant)
        {
            throw new TypeMismatchException("Cannot reassign values to constant variables", n.SourcePosition);
        }

        if (n.Id.Type.ActualType == n.Expr.Type.ActualType)
        {
            n.Type.ActualType = TypeVal.Ok;
        }
        else
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("The assigned value is incompatible with the left-hand-side",
                n.SourcePosition, n.Expr.Type, n.Id.Type);
        }
    }

    public virtual void Visit(IdNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Name);
        if (symbol is null)
        {
            throw new SymbolNotDeclaredException("The identifier has not been declared in an active scope",
                n.SourcePosition, n.Name);
        }

        if (n.AttributesRef is null)
        {
            n.AttributesRef = symbol;
        }

        n.Type = symbol.Type;
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
            Type[] expectedTypes = new Type[]
            {
                new Type {ActualType = TypeVal.String}, new Type {ActualType = TypeVal.Number}
            };
            if (n.Left.Type.ActualType is TypeVal.Boolean)
            {
                throw new TypeMismatchException("Incompatible type in left-hand-side of plus expression",
                    n.Left.SourcePosition, n.Left.Type, expectedTypes);
            }
            else
            {
                throw new TypeMismatchException("Incompatible type in right-hand-side of plus expression",
                    n.Right.SourcePosition, n.Right.Type, expectedTypes);
            }
        }
    }

    public void Visit(MinusNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type {ActualType = TypeVal.Number};
        if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in left-hand-side of expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in right-hand-side of expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Number;
    }

    public void Visit(MultNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type {ActualType = TypeVal.Number};
        if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in left-hand-side of expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in right-hand-side of expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Number;
    }

    public void Visit(DivNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type {ActualType = TypeVal.Number};
        if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in left-hand-side of expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in right-hand-side of expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Number;
    }

    public void Visit(PowNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
        var expectedType = new Type {ActualType = TypeVal.Number};
        if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in left-hand-side of expression", n.Left.SourcePosition,
                n.Left.Type, expectedType);
        }
        else if (n.Left.Type.ActualType != TypeVal.Number)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in right-hand-side of expression", n.Right.SourcePosition,
                n.Right.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Number;
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
            throw new TypeMismatchException("Unexpected type in expression", n.Left.SourcePosition, n.Left.Type,
                new Type {ActualType = TypeVal.Number});
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
            throw new TypeMismatchException("Unexpected type in the while-loop condition", n.Condition.SourcePosition,
                n.Condition.Type, new Type {ActualType = TypeVal.Boolean});
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

        if (initNode.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in for-loop initializer", initNode.SourcePosition,
                initNode.Type, expectedType);
        }
        else if (n.Limit.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in for-loop limit expression", n.Limit.SourcePosition,
                n.Limit.Type, expectedType);
        }
        else if (n.Update.Type != expectedType)
        {
            n.Type.ActualType = TypeVal.Error;
            throw new TypeMismatchException("Unexpected type in for-loop update expression", n.Update.SourcePosition,
                n.Update.Type, expectedType);
        }

        n.Type.ActualType = TypeVal.Ok;

        foreach (var stmtNode in n.Body)
        {
            stmtNode.Accept(this);
            stmtNode.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();
    }

    public void Visit(ContNode n)
    {
    }

    public void Visit(BreakNode n)
    {
    }

    public void Visit(LoopNode n)
    {
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
        EnterScope();
        foreach (var param in n.FormalParams)
        {
            _symbolTable.EnterSymbol(param);
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
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Name);
        if (symbol is not FunctionSymTableEntry funcEntry)
        {
            // FuncEntry is now symbol as FunctionSymTableEntry
            if (symbol is null)
            {
                throw new SymbolNotDeclaredException("The identifier does not exist in any active scope",
                    n.Id.SourcePosition, n.Id.Name);
            }

            throw new SymbolNotDeclaredException("The identifier does not correspond to a function declaration",
                n.Id.SourcePosition, n.Id.Name);
        }

        if (n.Id.AttributesRef is null)
        {
            n.Id.AttributesRef = funcEntry;
        }

        n.Type = funcEntry.Type;
        var parameterTypes = funcEntry.Parameters.Values.ToArray();
        var i = 0;
        if (funcEntry.Parameters.Count != n.Params.Count)
            throw new ParameterCountMismatchException("Unexpected number of parameters", n.SourcePosition,
                funcEntry.Parameters.Count, n.Params.Count, n.Id.Name);
        foreach (var param in n.Params)
        {
            param.Accept(this);
            if (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error)
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeMismatchException("Unexpected type in parameter", param.SourcePosition, param.Type,
                    parameterTypes[i]);
            }

            i++;
        }
    }

    public void Visit(FuncStmtNode n)
    {
        var symbol = _symbolTable.RetrieveSymbol(n.Id.Name);
        if (symbol is not FunctionSymTableEntry funcEntry)
        {
            // FuncEntry is now symbol as FunctionSymTableEntry
            if (symbol is null)
            {
                throw new SymbolNotDeclaredException("The identifier does not exist in any active scope",
                    n.Id.SourcePosition, n.Id.Name);
            }

            throw new SymbolNotDeclaredException("The identifier does not correspond to a function declaration",
                n.Id.SourcePosition, n.Id.Name);
        }

        if (n.Id.AttributesRef is null)
        {
            n.Id.AttributesRef = funcEntry;
        }

        n.Type = funcEntry.Type;
        var parameterTypes = funcEntry.Parameters.Values.ToArray();
        var i = 0;
        if (funcEntry.Parameters.Count != n.Params.Count)
            throw new ParameterCountMismatchException("Unexpected number of parameters", n.SourcePosition,
                funcEntry.Parameters.Count, n.Params.Count, n.Id.Name);
        foreach (var param in n.Params)
        {
            param.Accept(this);
            if (parameterTypes[i].ActualType != TypeVal.Any &&
                (param.Type != parameterTypes[i] || param.Type.ActualType == TypeVal.Error))
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeMismatchException("Unexpected type in parameter", param.SourcePosition, param.Type,
                    parameterTypes[i]);
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
            if (n.RetVal.Type.IsArray)
            {
                n.RetVal.Type.IsReturned = true;
            }

            if (n.SurroundingFuncType == n.RetVal.Type)
            {
                n.Type.ActualType = TypeVal.Ok;
            }
            else
            {
                n.Type.ActualType = TypeVal.Error;
                throw new TypeMismatchException("The given return type does not match its surrounding function",
                    n.SourcePosition, n.SurroundingFuncType, n.RetVal.Type);
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
            throw new TypeMismatchException("Unexpected type in the if-statement condition", n.Condition.SourcePosition,
                n.Condition.Type, new Type {ActualType = TypeVal.Boolean});
        }

        EnterScope();
        foreach (var node in n.ThenClause)
        {
            node.Accept(this);
            node.Type.ScopeLevel = _scopeLevel;
        }

        ExitScope();

        foreach (var node in n.ElseIfClauses)
        {
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
            throw new TypeMismatchException("Unexpected type in the else-if-statement condition",
                n.Condition.SourcePosition, n.Condition.Type, new Type {ActualType = TypeVal.Boolean});
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