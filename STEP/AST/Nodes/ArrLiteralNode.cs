namespace STEP.AST.Nodes;

public class ArrLiteralNode : ExprNode
{
    public List<ExprNode> Elements { get; init; } = new();
    public int ExpectedSize { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is ArrLiteralNode other)
        {
            return Elements.SequenceEqual(other.Elements);
        }

        return false;
    }
}