namespace STEP.AST.Nodes;

public class EqNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}