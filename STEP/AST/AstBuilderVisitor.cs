using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using STEP.AST.Nodes;
using System.Globalization;
using System.Xml.Linq;

namespace STEP.AST;

public class AstBuilderVisitor : STEPBaseVisitor<AstNode>
{
    public AstNode Build([NotNull] STEPParser.ProgramContext context)
    {
        return context.Accept(this);
    }

    private static SourcePosition GetSourcePosition(ParserRuleContext context)
    {
        return new SourcePosition(context.Start.Line, context.Start.Column);
    }

    private static SourcePosition GetSourcePosition(Antlr4.Runtime.Tree.ITerminalNode context)
    {
        return new SourcePosition(context.Symbol.Line, context.Symbol.Column);
    }

    public override ProgNode VisitProgram([NotNull] STEPParser.ProgramContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (children.Count == 0)
            throw new ApplicationException("Root node has no children.");

        ProgNode parentNode = (ProgNode) children.Single(child => child is ProgNode);
        List<AstNode> otherChildren = children.Where(child => child is not ProgNode).ToList();

        parentNode.SourcePosition = GetSourcePosition(context);

        if (otherChildren.Any(child => child is VarsNode))
        {
            parentNode.VarsBlock = (VarsNode) otherChildren.First(child => child is VarsNode);
        }

        if (otherChildren.Any(child => child is FuncsNode))
        {
            parentNode.FuncsBlock = (FuncsNode) otherChildren.First(child => child is FuncsNode);
        }

        return parentNode;
    }

    public override ProgNode VisitSetuploop([NotNull] STEPParser.SetuploopContext context)
    {
        ProgNode node = (ProgNode) NodeFactory.MakeNode(AstNodeType.ProgNode);
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        node.SourcePosition = GetSourcePosition(context);

        if (children.Any(child => child is SetupNode))
        {
            node.SetupBlock = (SetupNode) children.First(child => child is SetupNode);
        }

        if (children.Any(child => child is LoopNode))
        {
            node.LoopBlock = (LoopNode) children.First(child => child is LoopNode);
        }

        return node;
    }


    public override SetupNode VisitSetup([NotNull] STEPParser.SetupContext context)
    {
        List<StmtNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<StmtNode>().ToList();

        SetupNode node = (SetupNode) NodeFactory.MakeNode(AstNodeType.SetupNode);
        node.Stmts = children;
        node.SourcePosition = GetSourcePosition(context);

        return node;
    }

    public override LoopNode VisitLoop([NotNull] STEPParser.LoopContext context)
    {
        List<StmtNode> children = context.children
            .Select(kiddies => kiddies.Accept(this)).OfType<StmtNode>().ToList();

        LoopNode node = (LoopNode) NodeFactory.MakeNode(AstNodeType.LoopNode);
        node.Stmts = children;
        node.SourcePosition = GetSourcePosition(context);

        return node;
    }

    public override VarsNode VisitVariables([NotNull] STEPParser.VariablesContext context)
    {
        List<VarDclNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<VarDclNode>().ToList(); // Terminals are null

        VarsNode node = (VarsNode) NodeFactory.MakeNode(AstNodeType.VarsNode);
        node.Dcls = children;
        node.SourcePosition = GetSourcePosition(context);

        return node;
    }


    public override FuncsNode VisitFunctions([NotNull] STEPParser.FunctionsContext context)
    {
        List<FuncDefNode> children = context.children.Select(kiddies => kiddies.Accept(this))
            .OfType<FuncDefNode>().ToList();

        FuncsNode node = (FuncsNode) NodeFactory.MakeNode(AstNodeType.FuncsNode);
        node.FuncDcls = children;
        node.SourcePosition = GetSourcePosition(context);

        return node;
    }

    public override PinDclNode VisitPindcl([NotNull] STEPParser.PindclContext context)
    {
        PinDclNode node = (PinDclNode) NodeFactory.MakeNode(AstNodeType.PinDclNode);
        node.SourcePosition = GetSourcePosition(context);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Name = context.ID().GetText();
        idNode.Type = new PinType();
        idNode.SourcePosition = GetSourcePosition(context.ID());
        if (context.pintype().ANALOGPIN() != null)
        {
            idNode.Type.ActualType = TypeVal.Analogpin;
        }
        else
        {
            idNode.Type.ActualType = TypeVal.Digitalpin;
        }

        if (context.pinmode().INPUT() != null)
        {
            node.Type = new PinType() {Mode = PinMode.INPUT};
        }
        else
        {
            node.Type = new PinType() {Mode = PinMode.OUTPUT};
        }

        node.Left = idNode;

        NumberNode numNode = (NumberNode) NodeFactory.MakeNode(AstNodeType.NumberNode);
        numNode.Value = Int64.Parse(context.INTLITERAL().GetText(), new CultureInfo("en-US"));
        numNode.SourcePosition = GetSourcePosition(context.INTLITERAL());
        node.Right = numNode;

        return node;
    }

    public override AstNode VisitVardcl([NotNull] STEPParser.VardclContext context)
    {
        AstNode child = context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child != null);

        if (child.NodeType == AstNodeType.VarDclNode)
        {
            VarDclNode node = (VarDclNode) child;
            if (context.CONSTANT() != null)
                node.Left.Type.IsConstant = true;

            return node;
        }
        else
        {
            ArrDclNode node = (ArrDclNode) child;
            if (context.CONSTANT() != null)
                node.Left.Type.IsConstant = true;

            return node;
        }
    }

    public override VarDclNode VisitNumdcl([NotNull] STEPParser.NumdclContext context)
    {
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        node.SourcePosition = GetSourcePosition(context);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type.ActualType = TypeVal.Number;
        idNode.SourcePosition = GetSourcePosition(context.ID());
        idNode.Name = context.ID().GetText();

        node.Left = idNode;

        return node;
    }

    public override VarDclNode VisitStringdcl([NotNull] STEPParser.StringdclContext context)
    {
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        node.SourcePosition = GetSourcePosition(context);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type.ActualType = TypeVal.String;
        idNode.SourcePosition = GetSourcePosition(context.ID());
        idNode.Name = context.ID().GetText();

        node.Left = idNode;

        return node;
    }


    public override VarDclNode VisitBooldcl([NotNull] STEPParser.BooldclContext context)
    {
        VarDclNode node = (VarDclNode) NodeFactory.MakeNode(AstNodeType.VarDclNode);
        node.SourcePosition = GetSourcePosition(context);
        ExprNode exprChild = (ExprNode) context.children.Select(kiddies => kiddies.Accept(this))
            .First(child => child is ExprNode);
        node.Right = exprChild;

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Type.ActualType = TypeVal.Boolean;
        idNode.SourcePosition = GetSourcePosition(context.ID());
        idNode.Name = context.ID().GetText();

        node.Left = idNode;

        return node;
    }

    public override ArrDclNode VisitArrdcl([NotNull] STEPParser.ArrdclContext context)
    {
        ArrDclNode node = (ArrDclNode) NodeFactory.MakeNode(AstNodeType.ArrDclNode);
        node.SourcePosition = GetSourcePosition(context);
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.SourcePosition = GetSourcePosition(context.ID());

        switch (context.type().GetText())
        {
            case "number":
                idNode.Type.ActualType = TypeVal.Number;
                break;
            case "string":
                idNode.Type.ActualType = TypeVal.String;
                break;
            case "boolean":
                idNode.Type.ActualType = TypeVal.Boolean;
                break;
            default:
                node.Type.ActualType = TypeVal.Error;
                break;
        }

        idNode.Name = context.ID().GetText();
        idNode.Type.IsArray = true;
        node.Left = idNode;
        node.Size = Int32.Parse(context.arrsizedcl().GetText().Trim('[', ']'));

        if (children.Any(child => child is IdNode))
        {
            node.Right = (IdNode) children.First(child => child is IdNode);
        }
        else if (children.Any(child => child is ArrLiteralNode))
        {
            node.Right = (ArrLiteralNode) children.First(child => child is ArrLiteralNode);
            ((ArrLiteralNode) node.Right).ExpectedSize = node.Size;
        }

        return node;
    }

    public override NodesList VisitParams_options([NotNull] STEPParser.Params_optionsContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        NodesList node = (NodesList) NodeFactory.MakeNode(AstNodeType.NodesList);
        node.SourcePosition = GetSourcePosition(context);
        List<ExprNode> exprChildren = children.OfType<ExprNode>().ToList();

        foreach (ExprNode child in exprChildren)
        {
            node.Nodes.Add(child);
        }

        return node;
    }

    public override ExprNode VisitArrdclrhs([NotNull] STEPParser.ArrdclrhsContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        if (children.Count == 1)
        {
            IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
            idNode.SourcePosition = GetSourcePosition(context.ID());
            idNode.Name = context.ID().GetText();
            return idNode;
        }

        ArrLiteralNode node = (ArrLiteralNode) NodeFactory.MakeNode(AstNodeType.ArrayLiteralNode);
        NodesList nodesList = (NodesList) children.FirstOrDefault(child => child is NodesList);
        if (nodesList is not null)
        {
            foreach (AstNode astNode in nodesList.Nodes)
            {
                node.Elements.Add((ExprNode) astNode);
            }
        }

        return node;
    }

    public override ExprNode VisitLogicexpr([NotNull] STEPParser.LogicexprContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.AND() != null)
        {
            AndNode andNode = (AndNode) NodeFactory.MakeNode(AstNodeType.AndNode);
            andNode.SourcePosition = GetSourcePosition(context);
            andNode.Left = (ExprNode) children.First(child => child is ExprNode);
            andNode.Right = (ExprNode) children.Last(child => child is ExprNode);
            return andNode;
        }

        if (context.OR() != null)
        {
            OrNode orNode = (OrNode) NodeFactory.MakeNode(AstNodeType.OrNode);
            orNode.SourcePosition = GetSourcePosition(context);
            orNode.Left = (ExprNode) children.First(child => child is ExprNode);
            orNode.Right = (ExprNode) children.Last(child => child is ExprNode);
            return orNode;
        }

        // If it is neither an "and" or an "or" node, we therefore we simply return the child.
        return (ExprNode) children.First();
    }

    public override ExprNode VisitLogicequal([NotNull] STEPParser.LogicequalContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.EQ() != null)
        {
            EqNode eqNode = (EqNode) NodeFactory.MakeNode(AstNodeType.EqNode);
            eqNode.SourcePosition = GetSourcePosition(context);
            eqNode.Left = (ExprNode) children.First(child => child is ExprNode);
            eqNode.Right = (ExprNode) children.Last(child => child is ExprNode);
            return eqNode;
        }

        if (context.NEQ() != null)
        {
            NeqNode neqNode = (NeqNode) NodeFactory.MakeNode(AstNodeType.NeqNode);
            neqNode.SourcePosition = GetSourcePosition(context);
            neqNode.Left = (ExprNode) children.First(child => child is ExprNode);
            neqNode.Right = (ExprNode) children.Last(child => child is ExprNode);
            return neqNode;
        }

        // If it is neither an "eq" or an "neq" node, we therefore we simply return the child.
        return (ExprNode) children.First();
    }

    public override ExprNode VisitLogiccomp([NotNull] STEPParser.LogiccompContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.logiccompop() != null)
        {
            if (context.logiccompop().GRTHANEQ() != null)
            {
                GThanEqNode node = (GThanEqNode) NodeFactory.MakeNode(AstNodeType.GThanEqNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Left = (ExprNode) children.First(child => child is ExprNode);
                node.Right = (ExprNode) children.Last(child => child is ExprNode);
                return node;
            }

            if (context.logiccompop().GRTHAN() != null)
            {
                GThanNode node = (GThanNode) NodeFactory.MakeNode(AstNodeType.GThanNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Left = (ExprNode) children.First(child => child is ExprNode);
                node.Right = (ExprNode) children.Last(child => child is ExprNode);
                return node;
            }

            if (context.logiccompop().LTHANEQ() != null)
            {
                LThanEqNode node = (LThanEqNode) NodeFactory.MakeNode(AstNodeType.LThanEqNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Left = (ExprNode) children.First(child => child is ExprNode);
                node.Right = (ExprNode) children.Last(child => child is ExprNode);
                return node;
            }

            if (context.logiccompop().LTHAN() != null)
            {
                LThanNode node = (LThanNode) NodeFactory.MakeNode(AstNodeType.LThanNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Left = (ExprNode) children.First(child => child is ExprNode);
                node.Right = (ExprNode) children.Last(child => child is ExprNode);
                return node;
            }
        }

        // If it is neither an "greq", "gr", "lteq" or " node, we therefore we simply return the child.
        return (ExprNode) children.First();
    }

    public override ExprNode VisitLogicvalue([NotNull] STEPParser.LogicvalueContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.NEG() != null)
        {
            NegNode node = (NegNode) NodeFactory.MakeNode(AstNodeType.NegNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = null;
            return node;
        }

        return (ExprNode) children.First(child => child is ExprNode);
    }


    public override ExprNode VisitExpr([NotNull] STEPParser.ExprContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.PLUS() != null)
        {
            PlusNode node = (PlusNode) NodeFactory.MakeNode(AstNodeType.PlusNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = (ExprNode) children.Last(child => child is ExprNode);
            return node;
        }

        if (context.MINUS() != null)
        {
            MinusNode node = (MinusNode) NodeFactory.MakeNode(AstNodeType.MinusNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = (ExprNode) children.Last(child => child is ExprNode);
            return node;
        }

        return (ExprNode) children.First(child => child is ExprNode);
    }

    public override ExprNode VisitTerm([NotNull] STEPParser.TermContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.MULT() != null)
        {
            MultNode node = (MultNode) NodeFactory.MakeNode(AstNodeType.MultNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = (ExprNode) children.Last(child => child is ExprNode);
            return node;
        }

        if (context.DIVIDE() != null)
        {
            DivNode node = (DivNode) NodeFactory.MakeNode(AstNodeType.DivNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = (ExprNode) children.Last(child => child is ExprNode);
            return node;
        }

        return (ExprNode) children.First(child => child is ExprNode);
    }

    public override ExprNode VisitFactor([NotNull] STEPParser.FactorContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.POW() != null)
        {
            PowNode node = (PowNode) NodeFactory.MakeNode(AstNodeType.PowNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            node.Right = (ExprNode) children.Last(child => child is ExprNode);
            return node;
        }

        return (ExprNode) children.First(child => child is ExprNode);
    }

    public override ExprNode VisitArrindex([NotNull] STEPParser.ArrindexContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        return (ExprNode) children.First(child => child is ExprNode);
    }

    public override ExprNode VisitValue([NotNull] STEPParser.ValueContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.ID() != null)
        {
            IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
            idNode.SourcePosition = GetSourcePosition(context.ID());
            idNode.Name = context.ID().GetText();
            if (context.arrindex() != null)
            {
                ArrayAccessNode node = (ArrayAccessNode) NodeFactory.MakeNode(AstNodeType.ArrayAccessNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Array = idNode;
                node.Index = (ExprNode) children.Last(child => child is ExprNode);
                return UMinusCheck(context, node);
            }

            return UMinusCheck(context, idNode);
        }

        if (context.logicexpr() != null)
        {
            ParenNode node = (ParenNode) NodeFactory.MakeNode(AstNodeType.ParenNode);
            node.SourcePosition = GetSourcePosition(context);
            node.Left = (ExprNode) children.First(child => child is ExprNode);
            return UMinusCheck(context, node);
        }

        return UMinusCheck(context, (ExprNode) children.First(child => child is ExprNode));
    }

    // Helper function
    private ExprNode UMinusCheck([NotNull] STEPParser.ValueContext context, ExprNode node)
    {
        if (context.MINUS() != null)
        {
            UMinusNode uNode = (UMinusNode) NodeFactory.MakeNode(AstNodeType.UMinusNode);
            uNode.SourcePosition = GetSourcePosition(context);
            uNode.Left = node;
            return uNode;
        }

        return node;
    }

    public override ExprNode VisitConstant([NotNull] STEPParser.ConstantContext context)
    {
        context.children.Select(kiddies => kiddies.Accept(this));

        if (context.NUMLITERAL() != null)
        {
            NumberNode node = (NumberNode) NodeFactory.MakeNode(AstNodeType.NumberNode);
            node.Value = Double.Parse(context.NUMLITERAL().GetText(), new CultureInfo("en-US"));
            node.SourcePosition = GetSourcePosition(context.NUMLITERAL());

            return node;
        }
        else if (context.INTLITERAL() != null)
        {
            // TODO: Should this be a special IntNode/PinNode?
            NumberNode node = (NumberNode) NodeFactory.MakeNode(AstNodeType.NumberNode);
            node.Value = int.Parse(context.INTLITERAL().GetText(), new CultureInfo("en-US"));
            node.SourcePosition = GetSourcePosition(context.INTLITERAL());

            return node;
        }
        else if (context.STRLITERAL() != null)
        {
            StringNode node = (StringNode) NodeFactory.MakeNode(AstNodeType.StringNode);
            node.Value = context.STRLITERAL().GetText();
            node.SourcePosition = GetSourcePosition(context.STRLITERAL());

            return node;
        }
        else if (context.BOOLLITERAL() != null) // boolean literal is not null
        {
            BoolNode node = (BoolNode) NodeFactory.MakeNode(AstNodeType.BoolNode);
            node.Value = context.BOOLLITERAL().GetText().ToLower() == "true";
            node.SourcePosition = GetSourcePosition(context.BOOLLITERAL());

            return node;
        }
        else {
            DurationNode node = (DurationNode) NodeFactory.MakeNode(AstNodeType.DurationNode);
            node.Value = GetLeadingInt(context.DURATIONLITERAL().GetText());
            node.SourcePosition = GetSourcePosition(context.DURATIONLITERAL());
            node.Scale = GetString(context.DURATIONLITERAL().GetText()) switch {
                "ms" => DurationNode.DurationScale.Ms,
                "s" => DurationNode.DurationScale.S,
                "m" => DurationNode.DurationScale.M,
                "h" => DurationNode.DurationScale.H,
                "d" => DurationNode.DurationScale.D,
                _ => node.Scale
            };

            return node;
        }
    }

    private int GetLeadingInt(string input) {
        return Int32.Parse(new string(input.Trim().TakeWhile(c => char.IsDigit(c)).ToArray()));
    }

    private string GetString(string input) {
        return new string(input.Where(c => char.IsLetter(c)).ToArray());
    }

    public override AstNode VisitFunccall([NotNull] STEPParser.FunccallContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Name = context.ID().GetText();
        idNode.SourcePosition = GetSourcePosition(context.ID());

        List<ExprNode> parameters = new();

        NodesList nodesList = (NodesList) children.FirstOrDefault(child => child is NodesList);
        if (nodesList != null) parameters.AddRange(nodesList.Nodes.Cast<ExprNode>());

        if (context.Parent is STEPParser.ValueContext)
        {
            FuncExprNode exprNode = (FuncExprNode) NodeFactory.MakeNode(AstNodeType.FuncExprNode);
            exprNode.Id = idNode;
            exprNode.Params = parameters;
            exprNode.SourcePosition = GetSourcePosition(context);
            return exprNode;
        }
        else if (context.Parent is STEPParser.StmtsContext or STEPParser.Loop_stmtsContext)
        {
            FuncStmtNode stmtNode = (FuncStmtNode) NodeFactory.MakeNode(AstNodeType.FuncStmtNode);
            stmtNode.Id = idNode;
            stmtNode.Params = parameters;
            stmtNode.SourcePosition = GetSourcePosition(context);
            return stmtNode;
        }
        else
        {
            throw new InvalidOperationException("Funccall nodes should be a child of either Value or Stmt nodes.");
        }
    }

    public override AssNode VisitAssstmt([NotNull] STEPParser.AssstmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        AssNode node = (AssNode) NodeFactory.MakeNode(AstNodeType.AssNode);
        node.SourcePosition = GetSourcePosition(context);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.Name = context.ID().GetText();
        idNode.SourcePosition = GetSourcePosition(context.ID());

        node.Id = idNode;
        if (context.arrindex() != null)
        {
            node.ArrIndex = (ExprNode) children.First(child => child is ExprNode);
        }

        node.Expr = (ExprNode) children.Last(child => child is ExprNode);

        return node;
    }

    public override StmtNode VisitStmt([NotNull] STEPParser.StmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (children.Any(child => child is StmtNode))
        {
            return (StmtNode) children.First(child => child is StmtNode);
        }

        return null;
    }

    public override StmtNode VisitLoop_stmt([NotNull] STEPParser.Loop_stmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (children.Any(child => child is StmtNode))
        {
            return (StmtNode) children.First(child => child is StmtNode);
        }

        return null;
    }

    public override StmtNode VisitLoop_stmts([NotNull] STEPParser.Loop_stmtsContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        return (StmtNode) children.First(child => child is StmtNode);
    }

    public override WhileNode VisitWhilestmt([NotNull] STEPParser.WhilestmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        WhileNode node = (WhileNode) NodeFactory.MakeNode(AstNodeType.WhileNode);
        node.SourcePosition = GetSourcePosition(context);
        node.Condition = (ExprNode) children.First(child => child is ExprNode);
        node.Body = children.OfType<StmtNode>().ToList();

        return node;
    }

    public override ForNode VisitForstmt([NotNull] STEPParser.ForstmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();
        ForNode node = (ForNode) NodeFactory.MakeNode(AstNodeType.ForNode);
        node.SourcePosition = GetSourcePosition(context);

        node.Initializer = children.First(child => child != null);
        children.Remove(node.Initializer);

        node.Limit = (ExprNode) children.First(child => child is ExprNode);
        children.Remove(node.Limit);
        node.IsUpTo = context.UPTO() != null;

        node.Update = (ExprNode) children.First(child => child is ExprNode);
        children.Remove(node.Update);

        node.Body = children.OfType<StmtNode>().ToList();

        return node;
    }

    public override AstNode VisitFor_iter_opt([NotNull] STEPParser.For_iter_optContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.ID() != null)
        {
            IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
            idNode.SourcePosition = GetSourcePosition(context.ID());
            idNode.Name = context.ID().GetText();
            if (children.Any(child => child is ExprNode))
            {
                ArrayAccessNode node = (ArrayAccessNode) NodeFactory.MakeNode(AstNodeType.ArrayAccessNode);
                node.SourcePosition = GetSourcePosition(context);
                node.Array = idNode;
                node.Index = (ExprNode) children.First(child => child is ExprNode);
                return node;
            }

            return idNode;
        }
        else if (children.Any(child => child is VarDclNode))
        {
            return (VarDclNode) children.First(child => child is VarDclNode);
        }
        else // A child is an AssNode
        {
            return (AssNode) children.First(child => child is AssNode);
        }
    }

    public override IfNode VisitIfstmt([NotNull] STEPParser.IfstmtContext context)
    {
        IfNode node = (IfNode) NodeFactory.MakeNode(AstNodeType.IfNode);
        node.SourcePosition = GetSourcePosition(context);

        // Get condition
        node.Condition = (ExprNode) context.logicexpr().Accept(this);

        // Get then clause
        foreach (var stmt in context.stmt())
        {
            // Ensure that we do not add empty statements (which are null)
            if (stmt.Accept(this) is StmtNode stmtNode)
            {
                node.ThenClause.Add(stmtNode);
            }
        }

        // Get all else-if-clauses
        foreach (var elseIf in context.elseifstmt())
        {
            node.ElseIfClauses.Add((ElseIfNode) elseIf.Accept(this));
        }

        // Get else clause
        if (context.elsestmt() != null)
        {
            foreach (var stmt in context.elsestmt().stmt())
            {
                if (stmt.Accept(this) is StmtNode stmtNode)
                {
                    node.ElseClause.Add(stmtNode);
                }
            }
        }

        return node;
    }

    public override ElseIfNode VisitElseifstmt([NotNull] STEPParser.ElseifstmtContext context)
    {
        var node = (ElseIfNode) NodeFactory.MakeNode(AstNodeType.ElseIfNode);
        node.SourcePosition = GetSourcePosition(context);
        node.Condition = (ExprNode) context.logicexpr().Accept(this);
        node.Body = new List<StmtNode>();
        foreach (var stmt in context.stmt())
        {
            if (stmt.Accept(this) is StmtNode stmtNode)
            {
                node.Body.Add(stmtNode);
            }
        }

        return node;
    }

    public override IfNode VisitLoopifstmt([NotNull] STEPParser.LoopifstmtContext context)
    {
        IfNode node = (IfNode) NodeFactory.MakeNode(AstNodeType.IfNode);
        node.SourcePosition = GetSourcePosition(context);
        // Get condition
        node.Condition = (ExprNode) context.logicexpr().Accept(this);

        // Get then clause
        foreach (var stmt in context.loopifbody())
        {
            if (stmt.BREAK() != null)
            {
                var breakNode = (BreakNode) NodeFactory.MakeNode(AstNodeType.BreakNode);
                breakNode.SourcePosition = GetSourcePosition(stmt.BREAK());
                node.ThenClause.Add(breakNode);
            }
            else if (stmt.CONTINUE() != null)
            {
                var contNode = (ContNode) NodeFactory.MakeNode(AstNodeType.ContNode);
                contNode.SourcePosition = GetSourcePosition(stmt.CONTINUE());
                node.ThenClause.Add(contNode);
            }
            else if (stmt.Accept(this) is StmtNode stmtNode)
            {
                node.ThenClause.Add(stmtNode);
            }
        }

        // Get all else-if-clauses
        if (context.loopelseifstmt() != null)
        {
            foreach (var elseIf in context.loopelseifstmt())
            {
                node.ElseIfClauses.Add((ElseIfNode) elseIf.Accept(this));
            }
        }

        // Get else clause
        if (context.loopelsestmt() != null)
        {
            foreach (var stmt in context.loopelsestmt().loopifbody())
            {
                if (stmt.BREAK() != null)
                {
                    var breakNode = (BreakNode) NodeFactory.MakeNode(AstNodeType.BreakNode);
                    breakNode.SourcePosition = GetSourcePosition(stmt.BREAK());
                    node.ThenClause.Add(breakNode);
                }
                else if (stmt.CONTINUE() != null)
                {
                    var contNode = (ContNode) NodeFactory.MakeNode(AstNodeType.ContNode);
                    contNode.SourcePosition = GetSourcePosition(stmt.CONTINUE());
                    node.ThenClause.Add(contNode);
                }
                else if (stmt.Accept(this) is StmtNode stmtNode)
                {
                    node.ElseClause.Add(stmtNode);
                }
            }
        }

        return node;
    }

    public override ElseIfNode VisitLoopelseifstmt([NotNull] STEPParser.LoopelseifstmtContext context)
    {
        var node = (ElseIfNode) NodeFactory.MakeNode(AstNodeType.ElseIfNode);
        node.SourcePosition = GetSourcePosition(context);
        node.Condition = (ExprNode) context.logicexpr().Accept(this);
        node.Body = new List<StmtNode>();

        foreach (var stmt in context.loopifbody())
        {
            if (stmt.BREAK() != null)
            {
                var breakNode = (BreakNode) NodeFactory.MakeNode(AstNodeType.BreakNode);
                breakNode.SourcePosition = GetSourcePosition(stmt.BREAK());
                node.Body.Add(breakNode);
            }
            else if (stmt.CONTINUE() != null)
            {
                var contNode = (ContNode) NodeFactory.MakeNode(AstNodeType.ContNode);
                contNode.SourcePosition = GetSourcePosition(stmt.CONTINUE());
                node.Body.Add(contNode);
            }
            else if (stmt.Accept(this) is StmtNode stmtNode)
            {
                node.Body.Add(stmtNode);
            }
        }

        return node;
    }

    public override StmtNode VisitLoopifbody([NotNull] STEPParser.LoopifbodyContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.BREAK() != null)
        {
            var breakNode = (BreakNode) NodeFactory.MakeNode(AstNodeType.BreakNode);
            breakNode.SourcePosition = GetSourcePosition(context.BREAK());
            return breakNode;
        }
        else if (context.CONTINUE() != null)
        {
            var contNode = (ContNode) NodeFactory.MakeNode(AstNodeType.ContNode);
            contNode.SourcePosition = GetSourcePosition(context.CONTINUE());
            return contNode;
        }

        if (children.Any(child => child is StmtNode))
        {
            return (StmtNode) children.First(child => child is StmtNode);
        }
        else
        {
            return null;
        }
    }

    public override StmtNode VisitRetstmt([NotNull] STEPParser.RetstmtContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        RuleContext parent = context.Parent;
        while (parent is not STEPParser.FuncdclContext)
        {
            parent = parent.Parent;
        }

        STEPParser.FuncdclContext funcParent = (STEPParser.FuncdclContext) parent;

        RetNode node = (RetNode) NodeFactory.MakeNode(AstNodeType.RetNode);
        node.SourcePosition = GetSourcePosition(context);
        if (funcParent.BLANK() == null)
        {
            node.RetVal = (ExprNode) children.First(child => child is ExprNode);
            string type = funcParent.type().GetText().ToLower();
            node.SurroundingFuncType.ActualType = type == "number" ? TypeVal.Number :
                type == "string" ? TypeVal.String : TypeVal.Boolean;
            if (funcParent.brackets() != null)
            {
                node.SurroundingFuncType.IsArray = true;
            }

            return node;
        }

        node.SurroundingFuncType.ActualType = TypeVal.Blank;
        return node;
    }

    public override FuncDefNode VisitFuncdcl([NotNull] STEPParser.FuncdclContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        FuncDefNode node = (FuncDefNode) NodeFactory.MakeNode(AstNodeType.FuncDefNode);
        node.SourcePosition = GetSourcePosition(context);

        IdNode idNode = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        idNode.SourcePosition = GetSourcePosition(context.ID());
        idNode.Name = context.ID().GetText();
        node.Id = idNode;

        node.Stmts = children.OfType<StmtNode>().ToList();

        NodesList nodesList = (NodesList) children.First(child => child is NodesList);

        foreach (AstNode astNode in nodesList.Nodes)
        {
            node.FormalParams.Add((IdNode) astNode);
        }

        if (context.BLANK() == null)
        {
            string type = context.type().GetText().ToLower();
            node.ReturnType.ActualType = type == "number" ? TypeVal.Number :
                type == "string" ? TypeVal.String : TypeVal.Boolean;
            if (context.brackets() != null)
            {
                node.ReturnType.IsArray = true;
            }

            return node;
        }

        node.ReturnType.ActualType = TypeVal.Blank;
        return node;
    }

    public override NodesList VisitParams([NotNull] STEPParser.ParamsContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        if (context.params_content() != null)
        {
            return (NodesList) children.First(child => child is NodesList);
        }

        return (NodesList) NodeFactory.MakeNode(AstNodeType.NodesList);
    }


    public override NodesList VisitParams_content([NotNull] STEPParser.Params_contentContext context)
    {
        List<AstNode> children = context.children.Select(kiddies => kiddies.Accept(this)).ToList();

        IdNode node = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        node.Name = context.ID().GetText();
        node.SourcePosition = GetSourcePosition(context.ID());

        string type = context.paramstype().GetText().ToLower();
        node.Type.ActualType = type == "number" ? TypeVal.Number :
            type == "string" ? TypeVal.String : TypeVal.Boolean;

        if (context.brackets() != null)
        {
            node.Type.IsArray = true;
        }

        List<IdNode> paramsChildren = children.OfType<IdNode>().ToList();
        paramsChildren.Insert(0, node);

        NodesList nodesList = (NodesList) NodeFactory.MakeNode(AstNodeType.NodesList);

        foreach (IdNode idNode in paramsChildren)
        {
            nodesList.Nodes.Add((AstNode) idNode);
        }

        return nodesList;
    }

    public override IdNode VisitParams_multi([NotNull] STEPParser.Params_multiContext context)
    {
        IdNode node = (IdNode) NodeFactory.MakeNode(AstNodeType.IdNode);
        node.Name = context.ID().GetText();
        node.SourcePosition = GetSourcePosition(context.ID());

        string type = context.paramstype().GetText().ToLower();
        node.Type.ActualType = type == "number" ? TypeVal.Number :
            type == "string" ? TypeVal.String : TypeVal.Boolean;

        if (context.brackets() != null)
        {
            node.Type.IsArray = true;
        }

        return node;
    }
}