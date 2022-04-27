namespace STEP.AST.Nodes;

public class ElseIfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> Body { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is ElseIfNode other)
        {
            return Equals(other.Condition, Condition)
                   && Body.SequenceEqual(other.Body);
        }

        return false;
    }
}