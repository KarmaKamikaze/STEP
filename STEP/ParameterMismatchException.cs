using STEP.AST;
using STEP.AST.Nodes;

namespace STEP;

public class ParameterMismatchException : Exception
{
    public ParameterMismatchException(FuncStmtNode funcStmtNode, FunctionSymTableEntry symbol) : base(
        $"Parameter count mismatch, function is called with {funcStmtNode.Params.Count} parameters, but should be called with {symbol.Parameters.Count} parameters")
    {
        StmtNode = funcStmtNode;
        SymTableEntry = symbol;
    }

    public ParameterMismatchException(FuncExprNode funcExprNode, FunctionSymTableEntry symbol) : base(
        $"Parameter count mismatch, function is called with {funcExprNode.Params.Count} parameters, but should be called with {symbol.Parameters.Count} parameters")
    {
        ExprNode = funcExprNode;
        SymTableEntry = symbol;
    }

    public ParameterMismatchException(ArrLiteralNode arrLiteralNode) : base(
        $"Parameter count mismatch, array literal has {arrLiteralNode.Elements.Count} elements, but should have {arrLiteralNode.ExpectedSize} elements")
    {
        ArrLiteralNode = arrLiteralNode;
    }

    public FuncStmtNode StmtNode { get; }
    public FuncExprNode ExprNode { get; }
    public ArrLiteralNode ArrLiteralNode { get; }
    public FunctionSymTableEntry SymTableEntry { get; }
}