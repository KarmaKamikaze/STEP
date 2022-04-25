namespace STEP.AST.Nodes;

public class ContNode : StmtNode
{
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is ContNode;
    }
}