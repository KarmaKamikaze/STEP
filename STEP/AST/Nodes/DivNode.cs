namespace STEP.AST.Nodes;

public class DivNode : ExprNode
{
    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is DivNode other)
        {
            return Equals(other.Left, Left) && Equals(other.Right, Right);
        }

        return false;
    }
}