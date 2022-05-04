using System.Collections.Specialized;

namespace STEP.AST.Nodes;

public class IfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> ThenClause { get; set; } = new();
    public List<ElseIfNode> ElseIfClauses { get; init; } = new();
    public List<StmtNode> ElseClause { get; init; } = new();

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is IfNode other)
        {
            return Equals(other.Condition, Condition)
                   && other.ThenClause.SequenceEqual(ThenClause)
                   && other.ElseIfClauses.SequenceEqual(ElseIfClauses)
                   && other.ElseClause.SequenceEqual(ElseClause);
        }

        return false;
    }
}