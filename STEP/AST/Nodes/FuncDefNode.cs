namespace STEP.AST.Nodes;

public class FuncDefNode : AstNode
{
    public IdNode Name { get; set; }
    public IdNode ReturnType { get; set; }
    public Dictionary<IdNode, Type> FormalParams { get; set; }
    public List<StmtNode> Stmts { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}