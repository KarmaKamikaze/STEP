namespace STEP.AST.Nodes;

public class ArrayAccessNode : ExprNode
{
    public IdNode Array { get; set; }
    public ExprNode Index { get; set; }
}