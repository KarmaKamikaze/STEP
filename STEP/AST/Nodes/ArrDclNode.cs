using System.Collections.Generic;

namespace STEP.AST.Nodes;

public class ArrDclNode : AstNode
{
    // TODO: Should maybe be a NumberNode
    public bool IsConstant { get; set; }
    public int Size { get; set; }
    public TypeVal Type { get; set; }
    public IdNode Left { get; set; }
    public bool IsId { get; set; }
    public IdNode IdRight { get; set; }
    public List<ExprNode> ArrLitRight { get; set; }
}