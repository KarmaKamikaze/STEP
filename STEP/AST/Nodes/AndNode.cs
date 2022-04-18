namespace STEP.AST.Nodes;

public class AndNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}