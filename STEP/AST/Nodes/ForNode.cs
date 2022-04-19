namespace STEP.AST.Nodes;

public class ForNode : StmtNode
{
    public AstNode Initializer { get; set; }
    public ExprNode Limit { get; set; }
    public ExprNode Update { get; set; }
    public List<StmtNode> Body { get; set; } = new();
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}