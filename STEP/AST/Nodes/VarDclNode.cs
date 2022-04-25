namespace STEP.AST.Nodes;

public class VarDclNode : StmtNode
{
    public bool IsConstant { get; set; }
    public IdNode Left { get; set; }
    public ExprNode Right { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is VarDclNode other)
        {
            return Equals(other.Left, Left) 
                && Equals(other.Right, Right)
                && Equals(other.IsConstant, IsConstant);
        }
        return false;
    }
}