namespace STEP.AST.Nodes;

public class ArrDclNode : VarDclNode
{
    public int Size { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
    public bool IsId { get; set; }
    public IdNode IdRight { get; set; }
}