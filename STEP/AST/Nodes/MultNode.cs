namespace STEP.AST.Nodes;

public class MultNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is MultNode other)
        {
            return Equals(other.Left, Left) && Equals(other.Right, Right);
        }
        return false;
    }
}