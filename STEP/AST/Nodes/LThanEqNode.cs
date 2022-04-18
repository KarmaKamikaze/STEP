namespace STEP.AST.Nodes;

public class LThanEqNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}