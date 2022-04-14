using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using STEP.AST.Nodes;

namespace STEP.AST;

public class AstBuilderVisitor : STEPBaseVisitor<AstNode>
{
    public AstNode Build([NotNull] STEPParser.ProgramContext context)
    {
        return context.Accept(this);
    }
    
    public override ProgNode VisitProgram([NotNull] STEPParser.ProgramContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if(children.Count < 1) 
            throw new ApplicationException("Root node has no children.");

        ProgNode parentNode = (ProgNode) children.Single(child => child is ProgNode);
        List<AstNode> otherChildren = children.Where(child => child is not ProgNode).ToList();
        if (otherChildren.Count > 0)
        {
            AstNode firstChild = otherChildren[0];
            for (int i = 1; i < otherChildren.Count; i++)
            {
                firstChild.MakeSiblings(otherChildren[i]);
            }

            return (ProgNode) parentNode.AdoptChildren(firstChild);
        }

        return parentNode;
    }
    
    public override ProgNode VisitSetuploop([NotNull] STEPParser.SetuploopContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        Console.WriteLine("children count setuploop:" + children.Count);
        if (children.Any(child => child is SetupNode) && children.Any(child => child is LoopNode))
        {
            AstNode setupNode = children.First(child => child is SetupNode);
            AstNode loopNode = children.First(child => child is LoopNode);
            
            List<AstNode> progChildren = new List<AstNode> {setupNode, loopNode};
            
            return (ProgNode)AstNode.MakeFamily(AstNodeType.ProgNode, progChildren);
        }
        if (children.Any(child => child is SetupNode))
        {
            AstNode setupNode = children.First(child => child is SetupNode);
            return (ProgNode)NodeFactory.MakeNode(AstNodeType.ProgNode).AdoptChildren(setupNode);
        }

        if (children.Any(child => child is LoopNode))
        {
            AstNode loopNode = children.First(child => child is LoopNode);
            return (ProgNode)NodeFactory.MakeNode(AstNodeType.ProgNode).AdoptChildren(loopNode);
        }
        
        return (ProgNode)AstNode.MakeFamily(AstNodeType.ProgNode, children);
    }
    

    public override SetupNode VisitSetup([NotNull] STEPParser.SetupContext context)
    {
        SetupNode node;
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .Where(child => child is StmtNode).ToList();

        if (children.Count >= 1)
        {
            node = (SetupNode)AstNode.MakeFamily(AstNodeType.SetupNode, children);
        }
        else 
        {
            node = (SetupNode)NodeFactory.MakeNode(AstNodeType.SetupNode);
            node.AdoptChildren(NodeFactory.MakeNode(null));
        }

        return node;
    }
    
     public override LoopNode VisitLoop([NotNull] STEPParser.LoopContext context)
        {
            LoopNode node;
            List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this))
                .Where(child => child is StmtNode).ToList();
            
            if (children.Count >= 1)
                node = (LoopNode)AstNode.MakeFamily(AstNodeType.LoopNode, children);
            else 
            {
                node = (LoopNode)NodeFactory.MakeNode(AstNodeType.LoopNode);
                node.AdoptChildren(NodeFactory.MakeNode(null));
            }
    
            return node;
        }
    
    



}