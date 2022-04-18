namespace STEP.AST.Nodes;

public class PowNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}