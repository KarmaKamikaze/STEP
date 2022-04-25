namespace STEP.AST.Nodes;

public class FuncsNode : AstNode
{
    public List<FuncDefNode> FuncDcls { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is FuncsNode other)
        {
            return FuncDcls.SequenceEqual(other.FuncDcls);
        }
        return false;
    }
}