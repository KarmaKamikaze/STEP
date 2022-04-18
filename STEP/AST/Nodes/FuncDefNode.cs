namespace STEP.AST.Nodes; 

public class FuncDefNode : AstNode {
    public IdNode Name { get; set; }
    public IdNode ReturnType { get; set; }
    public List<ExprNode> FormalParams { get; set; } // Help - do we need a special type of node here?
    public List<StmtNode> Stmts { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}