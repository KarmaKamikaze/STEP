namespace STEP.AST;

public class NodeFactory
{
    public static AstNode MakeNode(AstNodeType type)
    {
        switch (type)
        {
            // Expressions
            case AstNodeType.IdNode:
                return new IdNode();
            case AstNodeType.PlusNode:
                return new PlusNode();
            case AstNodeType.MinusNode:
                return new MinusNode();
            case AstNodeType.MultNode:
                return new MultNode();
            case AstNodeType.DivNode:
                return new DivNode();
            case AstNodeType.PowNode:
                return new PowNode();
            case AstNodeType.UMinusNode:
                return new UMinusNode();
            case AstNodeType.NegNode:
                return new NegNode();
            case AstNodeType.GThanNode:
                return new GThanNode();
            case AstNodeType.GThanEqNode:
                return new GThanEqNode();
            case AstNodeType.LThanNode:
                return new LThanNode();
            case AstNodeType.LThanEqNode:
                return new LThanEqNode();
            case AstNodeType.EqNode:
                return new EqNode();
            case AstNodeType.NeqNode:
                return new NeqNode();
            case AstNodeType.AndNode:
                return new AndNode();
            case AstNodeType.OrNode:
                return new OrNode();
            case AstNodeType.ParenNode:
                return new ParenNode();
            case AstNodeType.NumberNode:
                return new NumberNode();
            case AstNodeType.StringNode:
                return new StringNode();
            case AstNodeType.BooleanNode:
                return new BooleanNode();
            case AstNodeType.FuncExprNode:
                return new FuncExprNode();
            // Variable Declarations
            case AstNodeType.ArrDclNode:
                return new ArrDclNode();
            case AstNodeType.VarDclNode:
                return new VarDclNode();
            case AstNodeType.VarsNode:
                return new VarsNode();
            // Statements
            case AstNodeType.AssNode:
                return new AssNode();
            case AstNodeType.FuncStmtNode:
                return new FuncStmtNode();
            case AstNodeType.LoopStmtNode:
                return new LoopStmtNode();
            case AstNodeType.WhileNode:
                return new WhileNode();
            case AstNodeType.ForNode:
                return new ForNode();
            case AstNodeType.IfNode:
                return new IfNode();
            case AstNodeType.RetNode:
                return new RetNode();
            case AstNodeType.ContNode:
                return new ContNode();
            case AstNodeType.BreakNode:
                return new BreakNode();
            // Function Declarations
            case AstNodeType.FuncDefNode:
                return new FuncDefNode();
            // Program Structure
            case AstNodeType.ProgNode:
                return new ProgNode();
            case AstNodeType.SetupNode:
                return new SetupNode();
            case AstNodeType.LoopNode:
                return new LoopNode();
            case AstNodeType.FuncsNode:
                return new FuncsNode();
            default:
                return new NullNode();
        }
    }
}