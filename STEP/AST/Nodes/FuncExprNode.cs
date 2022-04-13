using System.Collections.Generic;

namespace STEP.AST.Nodes;

public class FuncExprNode : ExprNode
{
    public IdNode Id { get; set; }
    public List<ExprNode> Params { get; set; }
}