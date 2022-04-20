using Antlr4.Runtime;
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
            TypeVal.Number;

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
            TypeVal.String;

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
            TypeVal.Boolean;

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
                    TypeVal.Number;
                break;
            case "string":
                idNode.Type =
                    TypeVal.String;

                break;
            case "boolean":
                idNode.Type =
                    TypeVal.Boolean;

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

    public override ExprNode VisitLogicexpr([NotNull] STEPParser.LogicexprContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if(context.AND() != null)
        {
            AndNode andNode = (AndNode) NodeFactory.MakeNode(AstNodeType.AndNode);
            andNode.Left = (ExprNode)children.First(child => child is ExprNode);
            andNode.Right = (ExprNode)children.Last(child => child is ExprNode);
            return andNode;
        }
        if(context.OR() != null)
        {
            OrNode orNode = (OrNode) NodeFactory.MakeNode(AstNodeType.OrNode);
            orNode.Left = (ExprNode)children.First(child => child is ExprNode);
            orNode.Right = (ExprNode)children.Last(child => child is ExprNode);
            return orNode;
        }
        
        // If it is neither an "and" or an "or" node, we therefore we simply return the child.
        return (ExprNode)children.First();
    }

    public override ExprNode VisitLogicequal([NotNull] STEPParser.LogicequalContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
    
        if(context.EQ() != null)
        {
            EqNode eqNode = (EqNode) NodeFactory.MakeNode(AstNodeType.EqNode);
            eqNode.Left = (ExprNode)children.First(child => child is ExprNode);
            eqNode.Right = (ExprNode)children.Last(child => child is ExprNode);
            return eqNode;
        }
        if(context.NEQ() != null)
        {
            NeqNode neqNode = (NeqNode) NodeFactory.MakeNode(AstNodeType.NeqNode);
            neqNode.Left = (ExprNode)children.First(child => child is ExprNode);
            neqNode.Right = (ExprNode)children.Last(child => child is ExprNode);
            return neqNode;
        }
        
        // If it is neither an "eq" or an "neq" node, we therefore we simply return the child.
        return (ExprNode)children.First();
    }
    
    public override ExprNode VisitLogiccomp([NotNull] STEPParser.LogiccompContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.logiccompop() != null)
        {
            if (context.logiccompop().GRTHANEQ() != null)
            {
                GThanEqNode node = (GThanEqNode) NodeFactory.MakeNode(AstNodeType.GThanEqNode);              
                node.Left = (ExprNode)children.First(child => child is ExprNode);
                node.Right = (ExprNode)children.Last(child => child is ExprNode);
                return node;
            }
            if (context.logiccompop().GRTHAN() != null) 
            {
                GThanNode node = (GThanNode) NodeFactory.MakeNode(AstNodeType.GThanNode);              
                node.Left = (ExprNode)children.First(child => child is ExprNode);
                node.Right = (ExprNode)children.Last(child => child is ExprNode);
                return node;
            }
            if (context.logiccompop().LTHANEQ() != null) 
            {
                LThanEqNode node = (LThanEqNode) NodeFactory.MakeNode(AstNodeType.LThanEqNode);              
                node.Left = (ExprNode)children.First(child => child is ExprNode);
                node.Right = (ExprNode)children.Last(child => child is ExprNode);
                return node;
            }
            if (context.logiccompop().LTHAN() != null) 
            {
                LThanNode node = (LThanNode) NodeFactory.MakeNode(AstNodeType.LThanNode);              
                node.Left = (ExprNode)children.First(child => child is ExprNode);
                node.Right = (ExprNode)children.Last(child => child is ExprNode);
                return node;
            }
        }

        // If it is neither an "greq", "gr", "lteq" or " node, we therefore we simply return the child.
        return (ExprNode)children.First();
    }
    
    public override ExprNode VisitLogicvalue([NotNull] STEPParser.LogicvalueContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.NEG() != null)
        {
            NegNode node = (NegNode) NodeFactory.MakeNode(AstNodeType.NegNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = null;
            return node;
        }

        return (ExprNode)children.First(child => child is ExprNode);
    }


    public override ExprNode VisitExpr([NotNull] STEPParser.ExprContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.PLUS() != null)
        {
            PlusNode node = (PlusNode) NodeFactory.MakeNode(AstNodeType.PlusNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = (ExprNode)children.Last(child => child is ExprNode);
            return node;
        }
        if (context.MINUS() != null)
        {
            MinusNode node = (MinusNode) NodeFactory.MakeNode(AstNodeType.MinusNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = (ExprNode)children.Last(child => child is ExprNode);
            return node;
        }
        
        return (ExprNode)children.First(child => child is ExprNode);
    }
    
    public override ExprNode VisitTerm([NotNull] STEPParser.TermContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
    
        if (context.MULT() != null)
        {
            MultNode node = (MultNode) NodeFactory.MakeNode(AstNodeType.MultNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = (ExprNode)children.Last(child => child is ExprNode);
            return node;
        }
        if (context.DIVIDE() != null)
        {
            DivNode node = (DivNode) NodeFactory.MakeNode(AstNodeType.DivNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = (ExprNode)children.Last(child => child is ExprNode);
            return node;
        }
        
        return (ExprNode)children.First(child => child is ExprNode);
    }
    
    public override ExprNode VisitFactor([NotNull] STEPParser.FactorContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        
        if (context.POW() != null)
        {
            PowNode node = (PowNode) NodeFactory.MakeNode(AstNodeType.PowNode);              
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = (ExprNode)children.Last(child => child is ExprNode);
            return node;
        }
        
        return (ExprNode)children.First(child => child is ExprNode);
    }
    
    public override ExprNode VisitValue([NotNull] STEPParser.ValueContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        UMinusNode uNode = (UMinusNode)NodeFactory.MakeNode(AstNodeType.UMinusNode);  
        
        if(context.ID() != null)
        {
            IdNode idNode = (IdNode)NodeFactory.MakeNode(AstNodeType.IdNode);
            idNode.Id = context.ID().GetText();
            if(context.arrindex() != null)
            {
                ArrayAccessNode node = (ArrayAccessNode)NodeFactory.MakeNode(AstNodeType.ArrayAccessNode);               
                node.Array = idNode;
                node.Index = (ExprNode)children.Last(child => child is ExprNode);
                if(context.MINUS() != null)
                {
                    uNode.Left = node;
                    return uNode;
                }
                return node;
            }
            if(context.MINUS() != null)
            {          
                uNode.Left = idNode;
                return uNode;
            }
            return idNode;        
        }

        if(context.logicexpr() != null)
        {
            ParenNode node = (ParenNode)NodeFactory.MakeNode(AstNodeType.ParenNode);
            node.Left = (ExprNode)children.First(child => child is ExprNode);
            node.Right = null;
            if(context.MINUS() != null)
            {          
                uNode.Left = node;
                return uNode;
            }
            return node;
        }
        
        if(context.MINUS() != null)
        {          
            uNode.Left = (ExprNode)children.First(child => child is ExprNode);
            return uNode;
        }
        return (ExprNode)children.First(child => child is ExprNode);
    }
    
    public override ExprNode VisitConstant([NotNull] STEPParser.ConstantContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.NUMLITERAL() != null)
        {
            NumberNode numNode = (NumberNode)NodeFactory.MakeNode(AstNodeType.NumberNode);
            numNode.Value = Double.Parse(context.NUMLITERAL().GetText());
            
            return numNode;
        }
        else if (context.STRLITERAL() != null)
        {
            StringNode node = (StringNode)NodeFactory.MakeNode(AstNodeType.StringNode);
            node.Value = context.STRLITERAL().GetText();
            return node;
        }
        else // boolean literal is not null
        {
            BoolNode node = (BoolNode)NodeFactory.MakeNode(AstNodeType.BoolNode);
            node.Value = context.BOOLLITERAL().GetText().ToLower() == "true";
            return node;
        }
    }
 
    public override AstNode VisitFunccall([NotNull] STEPParser.FunccallContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        IdNode idNode = (IdNode)NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Id = context.ID().GetText();
        
        if(context.Parent is STEPParser.ValueContext)
        {
            FuncExprNode exprNode = (FuncExprNode)NodeFactory.MakeNode(AstNodeType.FuncExprNode);
            exprNode.Id = idNode;
            exprNode.Params = children.OfType<ExprNode>().ToList();
            return exprNode;
        }

        if (context.Parent is STEPParser.StmtsContext)
        {
            FuncStmtNode stmtNode = (FuncStmtNode)NodeFactory.MakeNode(AstNodeType.FuncStmtNode);
            stmtNode.Id = idNode;
            stmtNode.Params = children.OfType<ExprNode>().ToList();
            return stmtNode;
        }
        else
        {
            throw new InvalidOperationException("Funccall nodes should be a child of either Value or Stmt nodes.");
        }
    }
   
   
   
   
   
   
}