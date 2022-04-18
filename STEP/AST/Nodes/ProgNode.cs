namespace STEP.AST.Nodes;

public class ProgNode : AstNode
{
    public VarsNode VarBlock { get; set; }
    public SetupNode SetupBlock { get; set; }
    public LoopNode LoopBlock { get; set; }
    public FuncsNode FuncBlock { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}