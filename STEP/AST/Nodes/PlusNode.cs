namespace STEP.AST.Nodes;

public class PlusNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}