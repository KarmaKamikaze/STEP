namespace STEP.AST.Nodes;

public class NodesList : AstNode
{
    public List<AstNode> Nodes { get; } = new();

    public override void Accept(IVisitor v) { }
}