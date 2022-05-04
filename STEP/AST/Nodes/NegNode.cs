namespace STEP.AST.Nodes;

public class NegNode : ExprNode
{
    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is NegNode other)
        {
            return Equals(other.Left, Left);
        }

        return false;
    }
}