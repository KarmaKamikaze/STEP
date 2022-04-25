namespace STEP.AST.Nodes;

public class FuncDefNode : AstNode
{
    public IdNode Name { get; set; }
    public Type ReturnType { get; set; } = new();
    public List<IdNode> FormalParams { get; set; } = new();
    public List<StmtNode> Stmts { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}