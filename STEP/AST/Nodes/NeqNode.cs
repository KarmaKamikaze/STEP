namespace STEP.AST.Nodes;

public class NeqNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is NeqNode other)
        {
            return Equals(other.Left, Left) && Equals(other.Right, Right);
        }
        return false;
    }
}