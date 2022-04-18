namespace STEP.AST.Nodes;

public class NumberNode : AstNode
{
    public float Value { get; set; }
    public readonly TypeVal Type = TypeVal.Number;
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}