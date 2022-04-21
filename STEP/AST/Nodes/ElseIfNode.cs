namespace STEP.AST.Nodes;

public class ElseIfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> Body { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }
}