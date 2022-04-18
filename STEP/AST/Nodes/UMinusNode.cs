namespace STEP.AST.Nodes;

public class UMinusNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}