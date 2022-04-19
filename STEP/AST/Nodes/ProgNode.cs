namespace STEP.AST.Nodes;

public class ProgNode : AstNode
{
    public VarsNode VarsBlock { get; set; }
    public SetupNode SetupBlock { get; set; }
    public LoopNode LoopBlock { get; set; }
    public FuncsNode FuncsBlock { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}