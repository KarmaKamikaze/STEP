namespace STEP.AST.Nodes;

public class IdNode : ExprNode
{
    public string Id { get; set; }
    public SymTableEntry AttributesRef { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}