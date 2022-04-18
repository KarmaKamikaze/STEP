namespace STEP.AST.Nodes;

public class GThanNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}