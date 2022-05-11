using STEP.AST;
using STEP.AST.Nodes;

namespace STEP.CodeGeneration;

/// <summary>
/// A visitor that replaces the names of any standard functions with their corresponding Arduino names.
/// </summary>
public class StandardEnvironmentVisitor : IVisitor
{
    public void Visit(AndNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(OrNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(EqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(NeqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(GThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(GThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(LThanNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(LThanEqNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(NegNode n)
    {
        n.Left.Accept(this);
    }

    public void Visit(NumberNode n)
    {
    }

    public void Visit(StringNode n)
    {
    }

    public void Visit(BoolNode n)
    {
    }

    public void Visit(DurationNode n) {
    }

    public void Visit(ArrDclNode n)
    {
        n.Left.Accept(this);
        n.Right?.Accept(this);
    }

    public void Visit(ArrLiteralNode n)
    {
        foreach (var element in n.Elements)
        {
            element.Accept(this);
        }
    }

    public void Visit(ArrayAccessNode n)
    {
        n.Array.Accept(this);
        n.Index.Accept(this);
    }

    public void Visit(VarDclNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(PinDclNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(AssNode n)
    {
        n.Id.Accept(this);
        n.Expr.Accept(this);
    }

    public void Visit(IdNode n)
    {
        // Check for Arduino constants here
        if (n.AttributesRef is StdSymTableEntry symbol)
        {
            n.Name = symbol.ArduinoName;
        }
    }

    public void Visit(PlusNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(MinusNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(MultNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(DivNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(PowNode n)
    {
        n.Left.Accept(this);
        n.Right.Accept(this);
    }

    public void Visit(ParenNode n)
    {
        n.Left.Accept(this);
    }

    public void Visit(UMinusNode n)
    {
        n.Left.Accept(this);
    }

    public void Visit(WhileNode n)
    {
        n.Condition.Accept(this);
        foreach (var stmt in n.Body)
        {
            stmt.Accept(this);
        }
    }

    public void Visit(ForNode n)
    {
        n.Initializer.Accept(this);
        n.Update.Accept(this);
        n.Limit.Accept(this);
        foreach (var stmt in n.Body)
        {
            stmt.Accept(this);
        }
    }

    public void Visit(ContNode n)
    {
    }

    public void Visit(BreakNode n)
    {
    }

    public void Visit(LoopNode n)
    {
        foreach (var stmt in n.Stmts)
        {
            stmt.Accept(this);
        }
    }

    public void Visit(FuncDefNode n)
    {
        foreach (var stmt in n.Stmts)
        {
            stmt.Accept(this);
        }
    }

    public void Visit(FuncExprNode n)
    {
        // Check for Arduino standard function
        if (n.Id.AttributesRef is StdFuncSymTableEntry symbol)
        {
            n.Id.Name = symbol.ArduinoName;
            foreach (var param in n.Params)
            {
                param.Accept(this);
            }
        }
    }

    public void Visit(FuncStmtNode n)
    {
        // Check for Arduino standard function
        if (n.Id.AttributesRef is StdFuncSymTableEntry symbol)
        {
            n.Id.Name = symbol.ArduinoName;
            foreach (var param in n.Params)
            {
                param.Accept(this);
            }
        }
    }

    public void Visit(FuncsNode n)
    {
        foreach (var dcl in n.FuncDcls)
        {
            dcl.Accept(this);
        }
    }

    public void Visit(RetNode n)
    {
    }

    public void Visit(IfNode n)
    {
        n.Condition.Accept(this);
        foreach (var stmt in n.ThenClause)
        {
            stmt.Accept(this);
        }

        if (n.ElseIfClauses != null)
        {
            foreach (var elseIf in n.ElseIfClauses)
            {
                elseIf.Accept(this);
            }
        }

        if (n.ElseClause != null)
        {
            foreach (var stmt in n.ElseClause)
            {
                stmt.Accept(this);
            }
        }
    }

    public void Visit(ElseIfNode n)
    {
        n.Condition.Accept(this);
        foreach (var stmt in n.Body)
        {
            stmt.Accept(this);
        }
    }

    public void Visit(VarsNode n)
    {
        foreach (var dcl in n.Dcls)
        {
            dcl.Accept(this);
        }
    }

    public void Visit(ProgNode n)
    {
        n.VarsBlock?.Accept(this);
        n.FuncsBlock?.Accept(this);
        n.SetupBlock?.Accept(this);
        n.LoopBlock?.Accept(this);
    }

    public void Visit(SetupNode n)
    {
        foreach (var stmt in n.Stmts)
        {
            stmt.Accept(this);
        }
    }
}