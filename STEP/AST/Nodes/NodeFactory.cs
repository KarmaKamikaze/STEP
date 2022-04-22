namespace STEP.AST.Nodes;

public static class NodeFactory
{
    public static AstNode MakeNode(AstNodeType? type)
    {
        switch (type)
        {
            // Expressions
            case AstNodeType.IdNode:
                return new IdNode() {NodeType = AstNodeType.IdNode};
            case AstNodeType.PlusNode:
                return new PlusNode() {NodeType = AstNodeType.PlusNode};
            case AstNodeType.MinusNode:
                return new MinusNode() {NodeType = AstNodeType.MinusNode};
            case AstNodeType.MultNode:
                return new MultNode() {NodeType = AstNodeType.MultNode};
            case AstNodeType.DivNode:
                return new DivNode() {NodeType = AstNodeType.DivNode};
            case AstNodeType.PowNode:
                return new PowNode() {NodeType = AstNodeType.PowNode};
            case AstNodeType.UMinusNode:
                return new UMinusNode() {NodeType = AstNodeType.UMinusNode};
            case AstNodeType.NegNode:
                return new NegNode() {NodeType = AstNodeType.NegNode};
            case AstNodeType.GThanNode:
                return new GThanNode() {NodeType = AstNodeType.GThanNode};
            case AstNodeType.GThanEqNode:
                return new GThanEqNode() {NodeType = AstNodeType.GThanEqNode};
            case AstNodeType.LThanNode:
                return new LThanNode() {NodeType = AstNodeType.LThanNode};
            case AstNodeType.LThanEqNode:
                return new LThanEqNode() {NodeType = AstNodeType.LThanEqNode};
            case AstNodeType.EqNode:
                return new EqNode() {NodeType = AstNodeType.EqNode};
            case AstNodeType.NeqNode:
                return new NeqNode() {NodeType = AstNodeType.NeqNode};
            case AstNodeType.AndNode:
                return new AndNode() {NodeType = AstNodeType.AndNode};
            case AstNodeType.OrNode:
                return new OrNode() {NodeType = AstNodeType.OrNode};
            case AstNodeType.ParenNode:
                return new ParenNode() {NodeType = AstNodeType.ParenNode};
            case AstNodeType.NumberNode:
                return new NumberNode() {NodeType = AstNodeType.NumberNode};
            case AstNodeType.StringNode:
                return new StringNode() {NodeType = AstNodeType.StringNode};
            case AstNodeType.BoolNode:
                return new BoolNode() {NodeType = AstNodeType.BoolNode};
            case AstNodeType.FuncExprNode:
                return new FuncExprNode() {NodeType = AstNodeType.FuncExprNode};
            case AstNodeType.ArrayAccessNode:
                return new ArrayAccessNode() {NodeType = AstNodeType.ArrayAccessNode};
            case AstNodeType.ArrayLiteralNode:
                return new ArrLiteralNode() {NodeType = AstNodeType.ArrayLiteralNode};
                // Variable Declarations
            case AstNodeType.ArrDclNode:
                return new ArrDclNode() {NodeType = AstNodeType.ArrDclNode};
            case AstNodeType.VarDclNode:
                return new VarDclNode() {NodeType = AstNodeType.VarDclNode};
            case AstNodeType.VarsNode:
                return new VarsNode() {NodeType = AstNodeType.VarsNode};
            // Statements
            case AstNodeType.AssNode:
                return new AssNode() {NodeType = AstNodeType.AssNode};
            case AstNodeType.FuncStmtNode:
                return new FuncStmtNode() {NodeType = AstNodeType.FuncStmtNode};
            case AstNodeType.WhileNode:
                return new WhileNode() {NodeType = AstNodeType.WhileNode};
            case AstNodeType.ForNode:
                return new ForNode() {NodeType = AstNodeType.ForNode};
            case AstNodeType.IfNode:
                return new IfNode() {NodeType = AstNodeType.IfNode};
            case AstNodeType.RetNode:
                return new RetNode() {NodeType = AstNodeType.RetNode};
            case AstNodeType.ContNode:
                return new ContNode() {NodeType = AstNodeType.ContNode};
            case AstNodeType.BreakNode:
                return new BreakNode() {NodeType = AstNodeType.BreakNode};
            // Function Declarations
            case AstNodeType.FuncDefNode:
                return new FuncDefNode() {NodeType = AstNodeType.FuncDefNode};
            // Program Structure
            case AstNodeType.ProgNode:
                return new ProgNode() {NodeType = AstNodeType.ProgNode};
            case AstNodeType.SetupNode:
                return new SetupNode() {NodeType = AstNodeType.SetupNode};
            case AstNodeType.LoopNode:
                return new LoopNode() {NodeType = AstNodeType.LoopNode};
            case AstNodeType.FuncsNode:
                return new FuncsNode() {NodeType = AstNodeType.FuncsNode};
            case AstNodeType.NodesList:
                return new NodesList() {NodeType = AstNodeType.NodesList};
            default:
                return null;
        }
    }
}