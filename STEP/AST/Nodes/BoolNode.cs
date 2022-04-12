namespace STEP.AST.Nodes;

public class BoolNode : AstNode
{
    public bool Value { get; set; }
	public TypeVal Type => TypeVal.Boolean;
}