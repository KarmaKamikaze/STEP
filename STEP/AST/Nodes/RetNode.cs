namespace STEP.AST.Nodes;

public class RetNode : StmtNode
{
    public ExprNode RetVal { get; set; }
    public TypeVal SurroundingFuncType { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}