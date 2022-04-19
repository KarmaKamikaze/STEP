namespace STEP.AST.Nodes;

public class VarsNode : AstNode
{
    public SymTableEntry AttributesRef { get; set; }
    public string Id { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
    public List<VarDclNode> Dcls { get; set; }
}