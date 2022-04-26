namespace STEP.AST.Nodes;

public class IdNode : ExprNode
{
    public string Id { get; set; }
    public SymTableEntry AttributesRef { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is IdNode other)
        {
            return Equals(other.Id, Id)
                && Equals(other.AttributesRef, AttributesRef)
                && Equals(other.Type, Type);
        }
        return false;
    }
}