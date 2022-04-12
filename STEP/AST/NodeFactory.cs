namespace STEP.AST;

public class NodeFactory
{
    public static AstNode MakeNode(AstNodeType type)
    {
        switch (type)
        {
            case AstNodeType.AssNode:
                return new AssNode();
            default:
                return new NullNode();
        }
    }
}