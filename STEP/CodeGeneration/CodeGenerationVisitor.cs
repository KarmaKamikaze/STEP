using System.Globalization;
using STEP.AST.Nodes;

namespace STEP.CodeGeneration;

public class CodeGenerationVisitor : IVisitor
{
    private void EmitLine(string line)
    {
        Console.WriteLine(line);
    }

    private void EmitAppend(string line)
    {
        Console.Write(line);
    }

    private void EmitAppend(TypeVal typeVal)
    {
        // TODO: translate our types to Arduino types here
        Console.Write(typeVal.ToString().ToLowerInvariant());
    }
    
    public void Visit(AndNode n)
    {
        // E.g.: x and true -> x && true 
        n.Left.Accept(this);
        EmitAppend(" && ");
        n.Right.Accept(this);
    }

    public void Visit(OrNode n)
    {
        // E.g.: x or true -> x || true 
        n.Left.Accept(this);
        EmitAppend(" || ");
        n.Right.Accept(this);
    }

    public void Visit(EqNode n)
    {
        // E.g.: x == true -> x == true 
        n.Left.Accept(this);
        EmitAppend(" == ");
        n.Right.Accept(this);
    }

    public void Visit(NeqNode n)
    {
        // E.g.: x != true -> x != true 
        n.Left.Accept(this);
        EmitAppend(" != ");
        n.Right.Accept(this);
    }

    public void Visit(GThanNode n)
    {
        // E.g.: x > true -> x > true 
        n.Left.Accept(this);
        EmitAppend(" > ");
        n.Right.Accept(this);
    }

    public void Visit(GThanEqNode n)
    {
        // E.g.: x >= true -> x >= true 
        n.Left.Accept(this);
        EmitAppend(" >= ");
        n.Right.Accept(this);
    }

    public void Visit(LThanNode n)
    {
        // E.g.: x < true -> x < true 
        n.Left.Accept(this);
        EmitAppend(" < ");
        n.Right.Accept(this);
    }

    public void Visit(LThanEqNode n)
    {
        // E.g.: x <= true -> x <= true 
        n.Left.Accept(this);
        EmitAppend(" <= ");
        n.Right.Accept(this);
    }

    public void Visit(NegNode n)
    {
        // not x -> ! x
        // not (x == y) -> !(x==y)
        EmitAppend("!");
        // TODO: Is this the right child?!?!?!
        n.Right.Accept(this);
    }

    public void Visit(NumberNode n)
    {
        EmitAppend(n.Value.ToString(CultureInfo.InvariantCulture));
    }

    public void Visit(StringNode n)
    {
        EmitAppend(n.Value);
    }

    public void Visit(BoolNode n)
    {
        EmitAppend(n.Value.ToString());
    }

    public void Visit(ArrDclNode n)
    {
        // Type id[size] = { elements };
        EmitAppend(n.Type + " ");
        n.Left.Accept(this);
        EmitAppend(" = ");
        n.Right.Accept(this);
        EmitLine(";");
    }

    public void Visit(ArrLiteralNode n)
    {
        EmitAppend("{");
        int count = n.Elements.Count;
        for (int i = 0; i < count; i++)
        {
            n.Elements[i].Accept(this);
            if (i < count-1)
            {
                // Add comma after all but the last element
                EmitAppend(", ");
            }
        }
        EmitAppend("}");
    }

    public void Visit(ArrayAccessNode n)
    {
        n.Array.Accept(this);
        EmitAppend("[");
        n.Index.Accept(this);
        EmitAppend("]");
    }

    public void Visit(VarDclNode n)
    {
        // Type id = expr;
        EmitAppend(n.Type + " ");
        n.Left.Accept(this);
        EmitAppend(" = ");
        n.Right.Accept(this);
        EmitLine(";");
    }

    public void Visit(AssNode n)
    {
        // id = expr;
        // TODO: if assignments can be used in expressions, this must be redone
        n.Id.Accept(this);
        EmitAppend(" = ");
        n.Expr.Accept(this);
        EmitLine(";");
    }

    public void Visit(IdNode n)
    {
        EmitAppend(n.Id);
    }

    public void Visit(PlusNode n)
    {
        // expr1 + expr2
        n.Left.Accept(this);
        EmitAppend(" + ");
        n.Right.Accept(this);
    }

    public void Visit(MinusNode n)
    {
        // expr1 - expr2
        n.Left.Accept(this);
        EmitAppend(" - ");
        n.Right.Accept(this);
    }

    public void Visit(MultNode n)
    {
        // expr1 * expr2
        n.Left.Accept(this);
        EmitAppend(" * ");
        n.Right.Accept(this);
    }

    public void Visit(DivNode n)
    {
        // expr1 / expr2
        n.Left.Accept(this);
        EmitAppend(" / ");
        n.Right.Accept(this);
    }

    public void Visit(PowNode n)
    {
        // expr1 ^ expr2
        // Uses built in Arduino function pow(int value, int exponent)
        EmitAppend("pow(");
        n.Left.Accept(this);
        EmitAppend(", ");
        n.Right.Accept(this);
        EmitAppend(")");
    }

    public void Visit(ParenNode n)
    {
        // (expr)
        EmitAppend("(");
        n.Left.Accept(this);
        EmitAppend(")");
    }

    public void Visit(UMinusNode n)
    {
        // - expr
        EmitAppend("-");
        n.Left.Accept(this);
    }

    public void Visit(WhileNode n)
    {
        // while(expr) { body }
        EmitAppend("while(");
        n.Condition.Accept(this);
        EmitLine(") {");
        foreach (StmtNode statement in n.Body)
        {
            statement.Accept(this);
        }
        EmitLine("}");
    }

    public void Visit(ForNode n)
    {
        // TODO: this!
        // for(int x = 0; i < n; i++)
        // VarDcl, AssNode, Identifier, ArrAccessNode
        EmitAppend("for(");
        n.Initializer.Accept(this);
        EmitAppend("; ");
    }

    public void Visit(ContNode n)
    {
        EmitAppend("continue;");
    }

    public void Visit(BreakNode n)
    {
        EmitAppend("break;");
    }

    public void Visit(FuncDefNode n)
    {
    }

    public void Visit(FuncExprNode n)
    {
    }

    public void Visit(FuncStmtNode n)
    {
    }

    public void Visit(FuncsNode n)
    {
    }

    public void Visit(RetNode n)
    {
        // If no expression, emit empty return
        if (n.RetVal is null) {
            EmitLine("return;");
        }
        else {
            EmitAppend("return ");
            n.RetVal.Accept(this);
        }
    }

    public void Visit(IfNode n)
    {
    }

    public void Visit(VarsNode n)
    {
        foreach(var dclNode in n.Dcls) {
            dclNode.Accept(this);
        }
    }

    public void Visit(ProgNode n)
    {
        // TODO: library inclusion?
        n.VarsBlock?.Accept(this);
        n.FuncsBlock?.Accept(this);
        n.SetupBlock?.Accept(this);
        n.LoopBlock?.Accept(this);
    }

    public void Visit(SetupNode n)
    {
        EmitLine("void setup() {");
        foreach(var stmt in n.Stmts) {
            stmt.Accept(this);
        }
        EmitLine("}");
    }
    
    public void Visit(LoopNode n)
    {
        EmitLine("void loop() {");
        foreach(var stmt in n.Stmts) {
            stmt.Accept(this);
        }
        EmitLine("}");
    }
}