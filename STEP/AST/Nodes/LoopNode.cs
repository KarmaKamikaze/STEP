namespace STEP.AST.Nodes;

public class LoopNode : AstNode
{
    public List<StmtNode> Stmts { get; set; }
}