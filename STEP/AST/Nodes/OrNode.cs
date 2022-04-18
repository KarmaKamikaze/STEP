namespace STEP.AST.Nodes;

public class OrNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}