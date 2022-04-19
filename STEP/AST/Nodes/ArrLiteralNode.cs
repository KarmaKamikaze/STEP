namespace STEP.AST.Nodes;

public class ArrLiteralNode : ExprNode
{
    public List<ExprNode> Elements { get; set; } = new();
    
    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }
}