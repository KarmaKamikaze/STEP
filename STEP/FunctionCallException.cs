using STEP.AST;
using STEP.AST.Nodes;

namespace STEP;

public class FunctionCallException : Exception {
    public FunctionCallException(FuncStmtNode funcStmtNode, FunctionSymTableEntry symbol) : base($"Parameter count mismatch, function is called with {funcStmtNode.Params.Count} parameters, but should be called with {symbol.Parameters.Count} parameters") {
        StmtNode = funcStmtNode;
        SymTableEntry = symbol;
    }
    public FunctionCallException(FuncExprNode funcExprNode, FunctionSymTableEntry symbol) : base($"Parameter count mismatch, function is called with {funcExprNode.Params.Count} parameters, but should be called with {symbol.Parameters.Count} parameters") {
        ExprNode = funcExprNode;
        SymTableEntry = symbol;
    }
    public FuncStmtNode StmtNode { get; set; }
    public FuncExprNode ExprNode { get; set; }
    public FunctionSymTableEntry SymTableEntry { get; set; }
}