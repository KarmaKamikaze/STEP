namespace STEP.AST.Nodes;

public class WhileNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> Body { get; set; } = new();

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is WhileNode other
               && Equals(other.Condition, Condition)
               && Body.SequenceEqual(other.Body);
    }
}