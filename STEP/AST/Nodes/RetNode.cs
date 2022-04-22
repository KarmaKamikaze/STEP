namespace STEP.AST.Nodes;

public class RetNode : StmtNode
{
    public ExprNode RetVal { get; set; }
    public Type SurroundingFuncType { get; set; } = new();
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}