namespace STEP.AST.Nodes;

public class StringNode : ExprNode
{
    public string Value { get; set; }
    public readonly TypeVal Type = TypeVal.String;
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}