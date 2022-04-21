namespace STEP.AST.Nodes;

public class IfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> ThenClause { get; set; } = new();
    public List<ElseIfNode> ElseIfClauses { get; set; } = new();
    public List<StmtNode> ElseClause { get; set; } = new();

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }
}