using System.Runtime.CompilerServices;
using Antlr4.Runtime.Misc;

namespace STEP.AST.Nodes;

public abstract class AstNode
{
    public AstNode()
    {
        LeftmostSibling = this;
    }
    
    public AstNodeType NodeType { get; set; }
    public TypeVal TypeVal { get; set; }
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
        AstNode newNodeSib = node.LeftmostSibling;
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
            this.LeftmostChild = newNodeSib;
            while (newNodeSib != null)
            {
                newNodeSib.Parent = this;
                newNodeSib = newNodeSib.RightSibling;
            }
        }

        return this;
    }

    public static AstNode MakeFamily(AstNodeType opType, List<AstNode> children)
    {
        AstNode firstChild = children[0];
        for (int i = 1; i < children.Count; i++)
        {
            firstChild.MakeSiblings(children[i]);
        }
        
        return NodeFactory.MakeNode(opType).AdoptChildren(firstChild);
    }

    public abstract void Accept(IVisitor v);
}