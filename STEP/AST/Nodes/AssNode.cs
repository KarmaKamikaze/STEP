namespace STEP.AST.Nodes;

public class AssNode : StmtNode
{
    public TypeVal Type { get; set; }
    public IdNode Id { get; set; }
    public ExprNode Expr { get; set; }
}

public abstract class StmtNode : AstNode
{


}

public class WhileNode : StmtNode
{
    public 
    


}

