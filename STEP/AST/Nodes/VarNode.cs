namespace STEP.AST.Nodes;

public class VarNode : AstNode
{
    public TypeVal Type { get; set; }
    public SymTableEntry AttributesRef { get; set; }
    public string Id { get; set; }
}