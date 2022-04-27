namespace STEP.AST.Nodes;

public class VarsNode : AstNode
{
    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public List<VarDclNode> Dcls { get; set; } = new();

    public override bool Equals(object obj)
    {
        return obj is VarsNode other
               && other.Dcls.SequenceEqual(Dcls);
    }
}