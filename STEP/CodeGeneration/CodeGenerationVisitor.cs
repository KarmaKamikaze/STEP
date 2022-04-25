using System.Globalization;
using System.Reflection;
using System.Text;
using STEP.AST.Nodes;

namespace STEP.CodeGeneration;

public class CodeGenerationVisitor : IVisitor
{
    private readonly StringBuilder _stringBuilder = new();
    private string Output => _stringBuilder.ToString();
    
    public void OutputToBaseFile()
    {
        string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        File.WriteAllText(directoryPath + "/compiled.c", Output);
    }
    
    private void EmitLine(string line)
    {
        _stringBuilder.AppendLine(line);
    }

    private void EmitAppend(string line)
    {
        _stringBuilder.Append(line);
    }

    private void EmitAppend(TypeVal typeVal)
    {
        // TODO: translate our types to Arduino types here
        _stringBuilder.Append(typeVal.ToString()+" ");
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
        EmitAppend(n.Type.ActualType);
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
        EmitAppend(n.Type.ActualType);
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
        // TODO: String concatenation, maybe use strcat(str1, str2) in C
        if (n.Type.ActualType is TypeVal.String)
        {
            if (n.Left.Type.ActualType is TypeVal.String && n.Right.Type.ActualType is not TypeVal.String)
            {
                // Convert right to string
                // Use Arduino's built in String() class
                EmitAppend("String(");
                n.Right.Accept(this);
                EmitAppend(")");
            }
            else if (n.Left.Type.ActualType is not TypeVal.String)
            {
                // Convert left to string
            }
        }
        
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
        EmitLine("continue;");
    }

    public void Visit(BreakNode n)
    {
        EmitLine("break;");
    }

    public void Visit(FuncDefNode n)
    {
        /* Type Id(Type1 Id1, ..., Typek Idk) {
         *   statements
         * }
         */
        EmitAppend(n.ReturnType.ActualType);
        n.Name.Accept(this);
        EmitAppend("(");
        // TODO: maybe this can be made prettier in a traditional for-loop!
        int i = 0;
        foreach (var param in n.FormalParams)
        {
            //EmitAppend(param.Value.Item1); // Type 
            //param.Key.Accept(this); // Id
            if (i < n.FormalParams.Count - 1)
            {
                EmitAppend(", ");
            }
            i++;
        }
        EmitLine(")");
        
        // Body
        foreach (StmtNode stmt in n.Stmts)
        {
            stmt.Accept(this);
        }
        EmitLine("}");
    }

    public void Visit(FuncExprNode n)
    {
        // Id(Param1, ..., Paramk)
        n.Id.Accept(this);
        EmitAppend("(");
        for (int i = 0; i < n.Params.Count; i++)
        {
            n.Params[i].Accept(this);
            if (i < n.Params.Count - 1)
            {
                EmitAppend(", ");
            }
        }
        EmitAppend(")");
    }

    public void Visit(FuncStmtNode n)
    {
        // Id(Param1, ..., Paramk);
        n.Id.Accept(this);
        EmitAppend("(");
        for (int i = 0; i < n.Params.Count; i++)
        {
            n.Params[i].Accept(this);
            if (i < n.Params.Count - 1)
            {
                EmitAppend(", ");
            }
        }
        EmitLine(");");
    }

    public void Visit(FuncsNode n)
    {
        foreach (var funDcl in n.FuncDcls)
        {
            funDcl.Accept(this);
        }
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
        /* if(condition) {
         *   ThenClause
         * }
         * else {
         *   ElseClause
         * }
         */
        
        EmitAppend("if(");
        n.Condition.Accept(this);
        EmitLine(") {");
        foreach(var stmt in n.ThenClause) {
            stmt.Accept(this);
        }
        EmitLine("}");
        if (n.ElseClause != null)
        {
            EmitLine("else {");
            foreach (var stmt in n.ElseClause)
            {
                stmt.Accept(this);
            }
            EmitLine("}");
        }
        
        /* if (expr1)
         *   statements1
         * else 
         *   if (expr1)
         *     statements2
         *   end if
         * end if
         *
         * if (expr1)
         *   statements
         * end if
         * if (expr2)
         *   statements
         * end if
         *
         * TODO: Det her er ikke muligt i STEP
         * if (expr1)
         *   statements1
         * else if (expr2)
         *   statements2
         * else
         *   statements3
         * end if
         */
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

    public void Visit(ElseIfNode n)
    {
        EmitAppend("else if(");
        n.Condition.Accept(this);
        EmitLine(") {");
        foreach(var stmt in n.Body)
        {
            stmt.Accept(this);
        }
        EmitLine("}");
    }
}