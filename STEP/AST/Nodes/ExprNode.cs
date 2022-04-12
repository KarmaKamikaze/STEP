namespace STEP.AST.Nodes; 

public abstract class ExprNode : AstNode {
    public ExprNode Left { get; set; }
    public ExprNode Right { get; set; }

    public ExprNode(ExprNode left, ExprNode right) {
        Left = left;
        Right = right;
    }
}