namespace STEP.AST.Nodes;

public class FuncExprNode : ExprNode
{
    public IdNode Id { get; set; }
    public List<ExprNode> Params { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is FuncExprNode other)
        {
            return Equals(other.Id, Id)
                   && Params.SequenceEqual(other.Params);
        }

        return false;
    }
}