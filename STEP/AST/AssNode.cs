namespace STEP.AST;

public class AssNode : AstNode
{
    public AstNodeType Type { get; } = AstNodeType.AssNode;
    public IdNode Id { get; set; }
    public LogicExprNode LogicExpr { get; set; }
}

public class IdNode : AstNode
{
    public AstNodeType Type { get; set; } = AstNodeType.IdNode;
    public string Id { get; set; }
    public Attributes AttributesRef { get; set; }
}