namespace STEP.AST.Nodes;

public class ArrDclNode : VarDclNode
{
    public int Size { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is ArrDclNode other)
        {
            return other.Size == Size
                   && Equals(other.Right, Right);
        }

        return false;
    }
}