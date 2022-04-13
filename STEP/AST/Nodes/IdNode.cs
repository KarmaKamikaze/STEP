namespace STEP.AST.Nodes;

public class IdNode : AstNode
{
    public TypeVal Type { get; set; }
    public string Id { get; set; }
    public bool IsArray { get; set; }
    public SymTableEntry AttributesRef { get; set; }
}
