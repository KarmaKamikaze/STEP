namespace STEP.AST.Nodes;

public class NeqNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}