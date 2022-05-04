namespace STEP.AST.Nodes;

public class LoopNode : AstNode
{
    public List<StmtNode> Stmts { get; set; }

    public override void Accept(IVisitor v)
    {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is LoopNode other
               && other.Stmts.SequenceEqual(Stmts);
    }
}