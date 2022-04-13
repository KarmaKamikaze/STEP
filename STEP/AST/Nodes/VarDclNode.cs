namespace STEP.AST.Nodes;

public class VarDclNode : AstNode
{
    public bool IsConstant { get; set; }
    public TypeVal Type { get; set; }
    public IdNode Left { get; set; }
    public ExprNode Right { get; set; }
}