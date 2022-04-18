namespace STEP.AST.Nodes;

public class DivNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}