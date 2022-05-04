namespace STEP.AST.Nodes;

public enum AstNodeType
{
    // Expressions
    IdNode,
    PlusNode,
    MinusNode,
    MultNode,
    DivNode,
    PowNode,
    UMinusNode, // Right child is null (unary)
    NegNode, // Right child is null (unary)
    GThanNode,
    GThanEqNode,
    LThanNode,
    LThanEqNode,
    EqNode,
    NeqNode,
    AndNode,
    OrNode,
    ParenNode,
    NumberNode,
    StringNode,
    BoolNode,
    FuncExprNode,
    ArrayAccessNode,
    ArrayLiteralNode,

    // Variable Declarations
    ArrDclNode,
    VarDclNode,
    PinDclNode,
    VarsNode,

    // Statements
    AssNode,
    FuncStmtNode,
    WhileNode,
    ForNode,
    IfNode,
    ElseIfNode,
    RetNode,
    ContNode,
    BreakNode,

    // Function Declarations
    FuncDefNode,

    // Program Structure
    ProgNode,
    SetupNode,
    LoopNode,
    FuncsNode,

    // Helper nodes
    NodesList
}