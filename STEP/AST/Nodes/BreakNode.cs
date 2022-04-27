namespace STEP.AST.Nodes;

public class BreakNode : StmtNode
{
    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is BreakNode;
    }
}