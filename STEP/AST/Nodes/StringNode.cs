namespace STEP.AST.Nodes;

public class StringNode
{
    public string Value { get; set; }
    public TypeVal Type => TypeVal.String;
}