namespace STEP.AST.Nodes; 

public abstract class ExprNode : AstNode {
    public TypeVal ExprType { get; set; }
    public ExprNode Left { get; set; }
    public ExprNode Right { get; set; }
}