namespace STEP.AST.Nodes;

public class LThanNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}