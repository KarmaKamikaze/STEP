﻿using System.Collections.Generic;

namespace STEP.AST.Nodes;

public class ForNode : StmtNode
{
    public AstNode Initializer { get; set; }
    public ExprNode Limit { get; set; }
    public ExprNode Update { get; set; }
    public List<StmtNode> Body { get; set; }
}