namespace STEP.AST.Nodes;

public class PinDclNode : VarDclNode {
    public PinType PinType { get; set; } = new();
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
    public override bool Equals(object obj)
    {
        if (obj is PinDclNode other) {
            return Equals(other.Left, Left)
                   && Equals(other.Right, Right) 
                   && Equals(other.PinType, PinType);
        }
        return false;
    }
}