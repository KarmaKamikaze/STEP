namespace STEP.AST.Nodes;

public class AssNode : StmtNode
{
    public IdNode Id { get; set; }
    public ExprNode ArrIndex { get; set; }
    public ExprNode Expr { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is AssNode other)
        {
            return Equals(other.Id, Id)
                   && Equals(other.ArrIndex, ArrIndex)
                   && Equals(other.Expr, Expr);
        }

        return false;
    }
}