namespace STEP.AST.Nodes;

public class SetupNode : AstNode
{
    public List<StmtNode> Stmts { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        return obj is SetupNode other
            && other.Stmts.SequenceEqual(Stmts);
    }
}