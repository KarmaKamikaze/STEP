namespace STEP.AST.Nodes;

public class BoolNode : AstNode
{
    public bool Value { get; set; }
    public readonly TypeVal Type = TypeVal.Boolean;
    public override void Accept(IVisitor v) {
	    v.Visit(this);
    }
}