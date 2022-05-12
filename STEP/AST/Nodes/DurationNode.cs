namespace STEP.AST.Nodes;

public class DurationNode : ExprNode
{
    public double Value { get; set; }
    public enum DurationScale {Ms, S, M, H, D}

    public DurationScale Scale = DurationScale.Ms;

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is DurationNode other
               && other.Value == Value;
    }
}