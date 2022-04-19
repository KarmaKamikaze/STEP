namespace STEP.AST.Nodes;

public class VarDclNode : StmtNode
{
    public bool IsConstant { get; set; }
    public IdNode Left { get; set; }
    public ExprNode Right { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}