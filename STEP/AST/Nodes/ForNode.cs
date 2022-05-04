namespace STEP.AST.Nodes;

public class ForNode : StmtNode
{
    // VarDcl, AssNode, Identifier, ArrAccessNode
    public AstNode Initializer { get; set; }
    public ExprNode Limit { get; set; }
    public ExprNode Update { get; set; }
    public List<StmtNode> Body { get; set; } = new();
    public bool IsUpTo { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is ForNode other)
        {
            return Equals(other.Initializer, Initializer)
                   && Equals(other.Limit, Limit)
                   && Equals(other.Update, Update)
                   && other.Body.SequenceEqual(Body);
        }

        return false;
    }
}