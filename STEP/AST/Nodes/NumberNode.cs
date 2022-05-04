namespace STEP.AST.Nodes;

public class NumberNode : ExprNode
{
    public double Value { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is NumberNode other
               && other.Value == Value;
    }
}