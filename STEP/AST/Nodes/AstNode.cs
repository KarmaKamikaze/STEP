using Antlr4.Runtime.Misc;

namespace STEP.AST.Nodes;

public abstract class AstNode
{
    public AstNodeType NodeType { get; set; }
    public AstNode Parent { get; set; }
    public AstNode RightSibling { get; set; }
    public AstNode LeftmostSibling { get; set; }
    public AstNode LeftmostChild { get; set; }

    public AstNode MakeSiblings([NotNull] AstNode node)
    {
        // Find the rightmost sibling
        AstNode thisSib = this;
        while (thisSib.RightSibling != null)
            thisSib = thisSib.RightSibling;
        
        // Append the new sibling node
        AstNode newNodeSib = thisSib.LeftmostSibling;
        thisSib.RightSibling = newNodeSib;
        
        // Connect the new siblings
        newNodeSib.LeftmostSibling = thisSib.LeftmostSibling;
        newNodeSib.Parent = thisSib.Parent;
        while (newNodeSib.RightSibling != null)
        {
            newNodeSib = newNodeSib.RightSibling;
            newNodeSib.LeftmostSibling = thisSib.LeftmostSibling;
            newNodeSib.Parent = thisSib.Parent;
        }

        return newNodeSib;
    }

    public AstNode AdoptChildren([NotNull] AstNode node)
    {
        if (this.LeftmostChild != null)
            this.LeftmostChild.MakeSiblings(node);
        else
        {
            AstNode newNodeSib = node.LeftmostSibling;
            this.LeftmostSibling = newNodeSib;
            while (newNodeSib != null)
            {
                newNodeSib.Parent = this;
                newNodeSib = newNodeSib.RightSibling;
            }
        }

        return node;
    }

    public static AstNode MakeFamily(AstNodeType opType, AstNode firstChild, AstNode secondChild)
    {
        return NodeFactory.MakeNode(opType).AdoptChildren(firstChild.MakeSiblings(secondChild));
    }
}