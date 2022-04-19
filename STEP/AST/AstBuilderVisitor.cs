using System.Text.RegularExpressions;
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

        if (children.Count < 1)
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

        if (children.Any(child => child is SetupNode) && children.Any(child => child is LoopNode))
        {
            AstNode setupNode = children.First(child => child is SetupNode);
            AstNode loopNode = children.First(child => child is LoopNode);

            List<AstNode> progChildren = new List<AstNode> {setupNode, loopNode};

            return (ProgNode) AstNode.MakeFamily(AstNodeType.ProgNode, progChildren);
        }

        if (children.Any(child => child is SetupNode))
        {
            AstNode setupNode = children.First(child => child is SetupNode);
            return (ProgNode) NodeFactory.MakeNode(AstNodeType.ProgNode).AdoptChildren(setupNode);
        }

        if (children.Any(child => child is LoopNode))
        {
            AstNode loopNode = children.First(child => child is LoopNode);
            return (ProgNode) NodeFactory.MakeNode(AstNodeType.ProgNode).AdoptChildren(loopNode);
        }

        return (ProgNode) AstNode.MakeFamily(AstNodeType.ProgNode, children);
    }


    public override SetupNode VisitSetup([NotNull] STEPParser.SetupContext context)
    {
        SetupNode node;
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .Where(child => child is StmtNode).ToList();

        if (children.Count >= 1)
        {
            node = (SetupNode) AstNode.MakeFamily(AstNodeType.SetupNode, children);
        }
        else
        {
            node = (SetupNode) NodeFactory.MakeNode(AstNodeType.SetupNode);
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
            node = (LoopNode) AstNode.MakeFamily(AstNodeType.LoopNode, children);
        else
        {
            node = (LoopNode) NodeFactory.MakeNode(AstNodeType.LoopNode);
            node.AdoptChildren(NodeFactory.MakeNode(null));
        }

        return node;
    }

    public override VarsNode VisitVariables([NotNull] STEPParser.VariablesContext context)
    {
        VarsNode node = (VarsNode)NodeFactory.MakeNode(AstNodeType.VarsNode);
        List<VarDclNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .Where(child => child != null).Cast<VarDclNode>().ToList(); // Terminals are null

        node.Dcls = children;
        /*
        if (children.Count >= 1)
            node = (VarsNode) AstNode.MakeFamily(AstNodeType.VarsNode, children);
        else
        {
            node = (VarsNode) NodeFactory.MakeNode(AstNodeType.VarsNode);
            node.AdoptChildren(NodeFactory.MakeNode(null));
        }
        */
        return node;
    }


    public override FuncsNode VisitFunctions([NotNull] STEPParser.FunctionsContext context)
    {
        FuncsNode node;
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .Where(child => child is FuncDefNode).ToList();

        if (children.Count >= 1)
            node = (FuncsNode) AstNode.MakeFamily(AstNodeType.FuncsNode, children);
        else
        {
            node = (FuncsNode) NodeFactory.MakeNode(AstNodeType.FuncsNode);
            node.AdoptChildren(NodeFactory.MakeNode(null));
        }

        return node;
    }

    public override AstNode VisitVardcl([NotNull] STEPParser.VardclContext context)
    {
        AstNode child = context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child != null);
        
        if (child.NodeType == AstNodeType.VarDclNode)
        {
            VarDclNode node = (VarDclNode)child;
            if (context.CONSTANT() != null)
                node.IsConstant = true; 
            
            return node;
        }
        else 
        {
            ArrDclNode node = (ArrDclNode)child;
            if (context.CONSTANT() != null)
                node.IsConstant = true; 
            
            return node;
        }
    }

    public override VarDclNode VisitNumdcl([NotNull] STEPParser.NumdclContext context)
    {
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.Number; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        List<AstNode> children = new List<AstNode>() {idNode, exprChild};
        VarDclNode node = (VarDclNode) AstNode.MakeFamily(AstNodeType.VarDclNode, children);
        node.Type = TypeVal.Number;

        return node;
    }

    public override VarDclNode VisitStringdcl([NotNull] STEPParser.StringdclContext context)
    {
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.String; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        List<AstNode> children = new List<AstNode>() {idNode, exprChild};
        VarDclNode node = (VarDclNode) AstNode.MakeFamily(AstNodeType.VarDclNode, children);
        node.Type = TypeVal.String;

        return node;
    }


    public override VarDclNode VisitBooldcl([NotNull] STEPParser.BooldclContext context)
    {
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.Boolean; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        List<AstNode> children = new List<AstNode>() {idNode, exprChild};
        VarDclNode node = (VarDclNode) AstNode.MakeFamily(AstNodeType.VarDclNode, children);
        node.Type = TypeVal.Boolean;

        return node;
    }

    public override ArrDclNode VisitArrdcl([NotNull] STEPParser.ArrdclContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        ArrDclNode node = new ArrDclNode();
        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);

        switch (context.type().GetText())
        {
            case "number":
                idNode.Type =
                    TypeVal.Number; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?
                break;
            case "string":
                idNode.Type =
                    TypeVal.String; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

                break;
            case "boolean":
                idNode.Type =
                    TypeVal.Boolean; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

                break;
            default:
                // node.Type = TypeVal.Error;
                break;
        }

        idNode.Id = context.ID().GetText();

        
        switch (context.type().GetText())
        {
            case "number":
                node.Type = TypeVal.Number;
                break;
            case "string":
                node.Type = TypeVal.String;
                break;
            case "boolean":
                node.Type = TypeVal.Boolean;
                break;
            default:
                // node.Type = TypeVal.Error;
                break;
        }

        node.Size = Int32.Parse(context.arrsizedcl().GetText().Trim('[', ']'));

        return node;
    }

    public override ExprNode VisitExpr([NotNull] STEPParser.ExprContext context)
    {
        return (ExprNode) NodeFactory.MakeNode(AstNodeType.FuncExprNode);
    }
}