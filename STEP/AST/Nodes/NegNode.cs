namespace STEP.AST.Nodes;

public class NegNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}