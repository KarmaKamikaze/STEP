namespace STEP.AST.Nodes;

public class IfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> ThenClause { get; set; }
    public List<StmtNode> ElseClause { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}