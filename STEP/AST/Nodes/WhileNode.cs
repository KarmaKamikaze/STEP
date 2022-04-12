namespace STEP.AST.Nodes;

public class WhileNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> Body { get; set; }
}