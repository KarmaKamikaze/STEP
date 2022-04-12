﻿namespace STEP.AST;

public class AssNode : StmtNode
{
    public AstNodeType Type { get; } = AstNodeType.AssNode;
    public IdNode Id { get; set; }
    public ExprNode Expr { get; set; }
}

public class IdNode : AstNode
{
    public AstNodeType Type { get; set; } = AstNodeType.IdNode;
    public string Id { get; set; }
    public Attributes AttributesRef { get; set; }
}


public abstract class StmtNode : AstNode
{


}

public class FuncStmtNode : StmtNode
{
    public IdNode Id { get; set; }



}

