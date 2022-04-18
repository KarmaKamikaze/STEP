namespace STEP.AST.Nodes;

public class MultNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}