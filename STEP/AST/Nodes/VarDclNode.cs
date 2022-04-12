namespace STEP.AST.Nodes;

public class VarDclNode : AstNode
{
    public TypeVal Type { get; set; }
    public VarsNode Left { get; set; }
    public ExprNode Right { get; set; }
}