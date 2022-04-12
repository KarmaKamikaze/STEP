namespace STEP.AST.Nodes;

public class NumberNode : AstNode
{
    public float Value { get; set; }
    public TypeVal Type => TypeVal.Number;
}