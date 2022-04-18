namespace STEP.AST.Nodes;

public class ArrDclNode : AstNode
{
    // TODO: Should maybe be a NumberNode
    public int Size { get; set; }
    public TypeVal Type { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}