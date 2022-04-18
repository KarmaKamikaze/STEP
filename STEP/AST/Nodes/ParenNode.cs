namespace STEP.AST.Nodes;

public class ParenNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}