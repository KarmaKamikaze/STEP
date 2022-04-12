namespace STEP.AST.Nodes;

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
            case AstNodeType.AssNode:
                return new AssNode();
            default:
                return new NullNode();
        }
    }
}