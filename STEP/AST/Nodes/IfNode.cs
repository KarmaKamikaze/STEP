using System.Collections.Specialized;

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

    public override bool Equals(object obj)
    {
        if(obj is IfNode other) 
        {

            //TODO: fix this shit with SequenceEquals
            // Check if all parts of the if nodes are equal
            if(!Equals(other.Condition, Condition))
            {
                return false;
            }

            // ThenClause - check if they have the same number of statements
            if (other.ThenClause.Count != ThenClause.Count)
            {
                return false;
            }
            // If so, check if each one is identical
            for (int i = 0; i < ThenClause.Count; i++)
            {
                if (!Equals(other.ThenClause[i], ThenClause[i]))
                {
                    return false;
                }
            }

            // ElseIfClauses
            if (other.ElseIfClauses.Count != ElseIfClauses.Count)
            {
                return false;
            }
            for (int i = 0; i < ElseIfClauses.Count; i++)
            {
                if (!Equals(other.ElseIfClauses[i], ElseIfClauses[i]))
                {
                    return false;
                }
            }

            // ElseClause
            if (other.ElseClause.Count != ElseClause.Count)
            {
                return false;
            }
            for (int i = 0; i < ElseClause.Count; i++)
            {
                if (!Equals(other.ElseClause[i], ElseClause[i]))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}