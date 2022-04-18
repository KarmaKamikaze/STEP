namespace STEP.AST.Nodes;

public class FuncStmtNode : StmtNode
{
    public IdNode Id { get; set; }
    public List<ExprNode> Params { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}