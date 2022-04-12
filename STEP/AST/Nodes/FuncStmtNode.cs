namespace STEP.AST.Nodes;

public class FuncStmtNode : StmtNode
{
    public IdNode Id { get; set; }
    public List<ExprNode> Params { get; set; }
    
}