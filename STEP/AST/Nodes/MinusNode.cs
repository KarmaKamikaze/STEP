namespace STEP.AST.Nodes;

public class MinusNode : ExprNode {
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}