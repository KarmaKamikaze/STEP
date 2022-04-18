namespace STEP.AST.Nodes;

public class FuncsNode : AstNode
{
    public List<FuncDefNode> FuncDcls { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}