namespace STEP.AST.Nodes;

public abstract class StmtNode : AstNode
{
    public override void Accept(IVisitor v) {
        //v.Visit(this);
    }
}