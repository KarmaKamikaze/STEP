namespace STEP.AST.Nodes;

public class AssNode : StmtNode
{
    public IdNode Id { get; set; }
    public ExprNode Expr { get; set; }
}