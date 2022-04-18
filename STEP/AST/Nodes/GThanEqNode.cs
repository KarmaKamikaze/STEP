namespace STEP.AST.Nodes;

public class GThanEqNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}