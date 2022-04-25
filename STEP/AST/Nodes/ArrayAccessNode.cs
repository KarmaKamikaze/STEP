namespace STEP.AST.Nodes;

public class ArrayAccessNode : ExprNode
{
    public IdNode Array { get; set; }
    public ExprNode Index { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is ArrayAccessNode other)
        {
            return Equals(other.Array, Array) && Equals(other.Index, Index);
        }
        return false;
    }
}