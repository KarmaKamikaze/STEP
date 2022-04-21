using STEP.AST.Nodes;

namespace STEP.AST;

public class AstPrintVisitor : IVisitor
{
    private int _ind = 0;

    private void Print(string s)
    {
        Console.Write(s);
    }

    private void Indent()
    {
        for (int i = 0; i < _ind; i++)
        {
            Console.Write("  ");
        }
    }

    public void Visit(ProgNode node)
    {
        if (node != null)
        {
            node.VarsBlock?.Accept(this);
            node.SetupBlock?.Accept(this);
            node.LoopBlock?.Accept(this);
            node.FuncsBlock?.Accept(this);
        }
    }

    public void Visit(ElseIfNode n)
    {
        // TODO: Implement this
        throw new NotImplementedException();
    }

    public void Visit(VarsNode node)
    {
        if (node != null)
        {
            Print("VarsNode\n");
            _ind++;
            foreach(VarDclNode dcl in node.Dcls)
            {
                dcl.Accept(this);
                Print("\n");
            }
            _ind--;
            Print("end VarsNode\n");
        }
    }

    public void Visit(SetupNode node)
    {
        if (node != null)
        {
            Print("SetupNode\n");
            _ind++;
            foreach (StmtNode stmt in node.Stmts)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Print("end SetupNode\n");
        }
    }

    public void Visit(VarDclNode node)
    {
        if (node != null)
        {
            Indent();
            if(node.IsConstant)
            {
                Print("constant ");
            }
            Print(node.Left.Type.ToString().ToLower() + " ");
            node.Left.Accept(this);
            Print(" = ");
            node.Right.Accept(this);
        }
    }

    public void Visit(LoopNode node)
    {
        if (node != null)
        {
            Print("LoopNode\n");
            _ind++;
            foreach (StmtNode stmt in node.Stmts)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Print("end LoopNode\n");
        }
    }

    public void Visit(FuncsNode node)
    {
        if (node != null)
        {
            Print("FuncsNode\n");
            _ind++;
            foreach (FuncDefNode funcdef in node.FuncDcls)
            {
                funcdef.Accept(this);
            }
            _ind--;
            Print("end FuncsNode\n");
        }
    }

    public void Visit(AndNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" and ");
            n.Right.Accept(this);
        }
    }

    public void Visit(OrNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" or ");
            n.Right.Accept(this);
        }
    }

    public void Visit(EqNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" == ");
            n.Right.Accept(this);
        }
    }

    public void Visit(NeqNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" != ");
            n.Right.Accept(this);
        }
    }

    public void Visit(GThanNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" > ");
            n.Right.Accept(this);
        }
    }

    public void Visit(GThanEqNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" >= ");
            n.Right.Accept(this);
        }
    }

    public void Visit(LThanNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" < ");
            n.Right.Accept(this);
        }
    }

    public void Visit(LThanEqNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" <= ");
            n.Right.Accept(this);
        }
    }

    public void Visit(NegNode n)
    {
        if (n != null)
        {
            Print("!");
            n.Left.Accept(this);
        }
    }

    public void Visit(NumberNode n)
    {
        if (n != null)
        {
            Print(n.Value.ToString());
        }
    }

    public void Visit(StringNode n)
    {
        if (n != null)
        {
            Print(n.Value);
        }
    }

    public void Visit(BoolNode n)
    {
        if (n != null)
        {
            Print(n.Value.ToString().ToLower());
        }
    }

    public void Visit(ArrDclNode n)
    {
        if (n != null)
        {
            Indent();
            if (n.IsConstant)
            {
                Print("constant ");
            }
            Print(n.Left.Type.ToString().ToLower() + $"[{n.Size}] ");
            n.Left.Accept(this);
            Print(" = ");
            if (n.IsId)
            {
                n.IdRight.Accept(this);
            }
            else
            {
                n.Right.Accept(this);
            }
        }
    }

    public void Visit(ArrLiteralNode n)
    {
        Print("[");
        n.Elements[0].Accept(this);
        for(int i = 1; i<n.Elements.Count; i++)
        {
            Print(", ");
            n.Elements[i].Accept(this);                
        }
        Print("]");
    }

    public void Visit(ArrayAccessNode n)
    {
        if (n != null)
        {
            n.Array.Accept(this);
            Print("[");
            n.Index.Accept(this);
            Print("]");
        }
    }

    public void Visit(AssNode n)
    {
        if (n != null)
        {
            Indent();
            n.Id.Accept(this);
            if (n.ArrIndex != null)
            {
                Print("[");
                n.ArrIndex.Accept(this);
                Print("]");
            }
            Print(" = ");
            n.Expr.Accept(this);
        }
    }

    public void Visit(IdNode n)
    {
        if (n != null)
        {
            Print(n.Id);
        }
    }

    public void Visit(PlusNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" + ");
            n.Right.Accept(this);
        }
    }

    public void Visit(MinusNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" - ");
            n.Right.Accept(this);
        }
    }

    public void Visit(MultNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" * ");
            n.Right.Accept(this);
        }
    }

    public void Visit(DivNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" / ");
            n.Right.Accept(this);
        }
    }

    public void Visit(PowNode n)
    {
        if (n != null)
        {
            n.Left.Accept(this);
            Print(" ^ ");
            n.Right.Accept(this);
        }
    }

    public void Visit(ParenNode n)
    {
        if (n != null)
        {
            Print("(");
            n.Left.Accept(this);
            Print(") ");
        }
    }

    public void Visit(UMinusNode n)
    {
        if (n != null)
        {
            Print("-");
            n.Left.Accept(this);
        }
    }

    public void Visit(WhileNode n)
    {
        if (n != null)
        {
            Indent();
            Print("while (");
            n.Condition.Accept(this);
            Print(")\n");
            _ind++;

            foreach (StmtNode stmt in n.Body)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Indent();
            Print("end while");
        }
    }

    public void Visit(ForNode n)
    {
        if (n != null)
        {
            Indent();
            Print("for (");
            n.Initializer.Accept(this);
            Print(" to ");
            n.Limit.Accept(this);
            Print(", change by ");
            n.Update.Accept(this);
            Print(")\n");
            _ind++;

            foreach (StmtNode stmt in n.Body)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Indent();
            Print("end for");
        }
    }

    public void Visit(ContNode n)
    {
        if(n != null)
        {
            Indent();
            Print("continue");
        }
    }

    public void Visit(BreakNode n)
    {
        if (n != null)
        {
            Indent();
            Print("break");
        }
    }

    public void Visit(FuncDefNode n)
    {
        throw new NotImplementedException();
    }

    public void Visit(FuncExprNode n)
    { 
        if (n != null)
        {
            n.Id.Accept(this);
            Print("(");
            n.Params[0].Accept(this);
            for (int i = 1; i < n.Params.Count; i++)
            {
                Print(", ");
                n.Params[i].Accept(this);
            }
            Print(")");
        }
    }

    public void Visit(FuncStmtNode n)
    {
        if (n != null)
        {
            Indent();
            n.Id.Accept(this);
            Print("(");
            n.Params[0].Accept(this);
            for (int i = 1; i < n.Params.Count; i++)
            {
                Print(", ");
                n.Params[i].Accept(this);
            }
            Print(")");
        }
    }

    public void Visit(RetNode n)
    {
        throw new NotImplementedException();
    }

    public void Visit(IfNode n)
    {
        if (n != null)
        {
            Indent();
            Print("if(");
            n.Condition.Accept(this);
            Print(")\n");

            _ind++;

            foreach (StmtNode stmt in n.ThenClause)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Indent();
            Print("else\n");
            _ind++;
            foreach (StmtNode stmt in n.ElseClause)
            {
                stmt.Accept(this);
                Print("\n");
            }
            _ind--;
            Indent();
            Print("end if");
        }
    }
}

