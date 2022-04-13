namespace STEP.AST.Nodes;

public class FuncDefNode : AstNode
{
    public IdNode Name { get; set; }
    public IdNode ReturnType { get; set; }
    public Dictionary<IdNode, (TypeVal, bool)> FormalParams { get; set; } // Tuple boolean is true when param is array
    public List<StmtNode> Stmts { get; set; }
}