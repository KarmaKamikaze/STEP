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

    public override bool Equals(object obj)
    {
        if(obj is ProgNode other)
        {
            // If all child nodes are equal, then it is the same ProgNode and returns true.
            // If any of them are not equal, it returns false.
            return Equals(other.VarsBlock, VarsBlock)
                && Equals(other.SetupBlock, SetupBlock)
                && Equals(other.LoopBlock, LoopBlock)
                && Equals(other.FuncsBlock, FuncsBlock);
        }
        return false;
    }
}