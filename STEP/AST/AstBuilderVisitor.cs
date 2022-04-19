using Antlr4.Runtime.Misc;
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
        
        if (otherChildren.Any(child => child is VarsNode))
        {
            parentNode.VarsBlock = (VarsNode)otherChildren.First(child => child is VarsNode);
        }
        
        if (otherChildren.Any(child => child is FuncsNode))
        {
            parentNode.FuncsBlock = (FuncsNode)otherChildren.First(child => child is FuncsNode);
        }

        return parentNode;
    }

    public override ProgNode VisitSetuploop([NotNull] STEPParser.SetuploopContext context)
    {
        ProgNode node = (ProgNode)NodeFactory.MakeNode(AstNodeType.ProgNode);
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (children.Any(child => child is SetupNode))
        {
            node.SetupBlock = (SetupNode)children.First(child => child is SetupNode);
        }

        if (children.Any(child => child is LoopNode))
        {
            node.LoopBlock = (LoopNode)children.First(child => child is LoopNode);
        }
        
        return node;
    }


    public override SetupNode VisitSetup([NotNull] STEPParser.SetupContext context)
    {
        List<StmtNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<StmtNode>().ToList();
        
        SetupNode node = (SetupNode)NodeFactory.MakeNode(AstNodeType.SetupNode);
        node.Stmts = children;
        
        return node;
    }

    public override LoopNode VisitLoop([NotNull] STEPParser.LoopContext context)
    {
        List<StmtNode> children = context.children
            .Select( kiddies => kiddies.Accept(this)).OfType<StmtNode>().ToList();
        
        LoopNode node = (LoopNode)NodeFactory.MakeNode(AstNodeType.LoopNode);
        node.Stmts = children;
        
        return node;
    }

    public override VarsNode VisitVariables([NotNull] STEPParser.VariablesContext context)
    {
        List<VarDclNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<VarDclNode>().ToList(); // Terminals are null
        
        VarsNode node = (VarsNode)NodeFactory.MakeNode(AstNodeType.VarsNode);
        node.Dcls = children;
        
        return node;
    }


    public override FuncsNode VisitFunctions([NotNull] STEPParser.FunctionsContext context)
    {    
        List<FuncDefNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<FuncDefNode>().ToList();

        FuncsNode node = (FuncsNode)NodeFactory.MakeNode(AstNodeType.FuncsNode);
        node.FuncDcls = children;
        
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
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.Number; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        node.Left = idNode;

        return node;
    }

    public override VarDclNode VisitStringdcl([NotNull] STEPParser.StringdclContext context)
    {
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.String; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        node.Left = idNode;

        return node;
    }


    public override VarDclNode VisitBooldcl([NotNull] STEPParser.BooldclContext context)
    {
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type =
            TypeVal.Boolean; // Maybe not the right place to assign the type? Maybe should be done in typechecking or symbol table creation?

        idNode.Id = context.ID().GetText();

        node.Left = idNode;

        return node;
    }

    public override ArrDclNode VisitArrdcl([NotNull] STEPParser.ArrdclContext context)
    {
        ArrDclNode node = (ArrDclNode)NodeFactory.MakeNode(AstNodeType.ArrDclNode);
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

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
                node.Type = TypeVal.Error;
                break;
        }

        idNode.Id = context.ID().GetText();
        idNode.IsArray = true;
        node.Left = idNode;

        node.Size = Int32.Parse(context.arrsizedcl().GetText().Trim('[', ']'));

        return node;
    }

    public override ExprNode VisitArr_id_or_lit([NotNull] STEPParser.Arr_id_or_litContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        if(children.Count == 1)
        {
            IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
            idNode.Id = context.ID().GetText();
            return idNode;
        }
        ArrLiteralNode arrLitNode = (ArrLiteralNode)NodeFactory.MakeNode(AstNodeType.ArrayLiteralNode);
        arrLitNode.Elements = children.OfType<ExprNode>().ToList();
        return arrLitNode;
    }

    public override ExprNode VisitExpr([NotNull] STEPParser.ExprContext context)
    {
        return (ExprNode) NodeFactory.MakeNode(AstNodeType.FuncExprNode);
    }

    
}