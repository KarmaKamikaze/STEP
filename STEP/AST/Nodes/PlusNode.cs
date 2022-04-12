namespace STEP.AST.Nodes;

public class PlusNode : ExprNode {
    public new TypeVal ExprType => Left.ExprType == TypeVal.String || Right.ExprType == TypeVal.String
        ? TypeVal.String
        : TypeVal.Number;
}