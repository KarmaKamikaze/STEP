namespace STEP.AST.Nodes;

public class StringNode : ExprNode
{
    public string Value { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is StringNode other
            && other.Value == Value;
    }
}