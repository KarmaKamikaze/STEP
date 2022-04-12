namespace STEP.AST.Nodes;

public class StringNode : AstNode
{
    public string Value { get; set; }
    public TypeVal Type => TypeVal.String;
}