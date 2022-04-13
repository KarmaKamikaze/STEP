using System.Collections.Generic;

namespace STEP.AST.Nodes;

public class VarsNode : AstNode
{
    public List<AstNode> Dcls { get; set; }
}