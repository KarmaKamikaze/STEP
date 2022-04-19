namespace STEP.AST.Nodes;

public class NumberNode : ExprNode
{
    public double Value { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}