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

    public string OutputToString()
    {
        return Output;
    }
    
    private void EmitLine(string line)
    {
        _stringBuilder.AppendLine(line);
    }

    private void EmitAppend(string line)
    {
        _stringBuilder.Append(line);
    }

    private void EmitAppend(Type type)
    {
        if (type.IsConstant)
            EmitAppend("const ");
        
        switch (type.ActualType)
        {
            case TypeVal.Number:
                EmitAppend("double ");
                break;
            case TypeVal.String:
                EmitAppend("String ");
                break;
            case TypeVal.Boolean:
                EmitAppend("boolean ");
                break;
            case TypeVal.Blank:
                EmitAppend("void ");
                break;
        }
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
        n.Left.Accept(this);
    }

    public void Visit(NumberNode n)
    {
        EmitAppend(n.Value.ToString(CultureInfo.InvariantCulture));
    }

    public void Visit(StringNode n)
    {
        EmitAppend("\"" + n.Value + "\"");
    }

    public void Visit(BoolNode n)
    {
        EmitAppend(n.Value.ToString().ToLowerInvariant());
    }
    
    public void Visit(ArrDclNode n)
    {
        // Type id[size] = { elements };
        EmitAppend(n.Type);

        n.Left.Accept(this);
        EmitAppend($"[{n.Size}] = ");
        if (n.IsId)
        {
            n.IdRight.Accept(this);
        }
        else
        {
            n.Right.Accept(this);
        }

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
        VarDclNodeGen(n);
        EmitLine(";");
    }

    private void VarDclNodeGen(VarDclNode n)
    {
        // Type id = expr;
        EmitAppend(n.Type);

        n.Left.Accept(this);
        EmitAppend(" = ");
        n.Right.Accept(this);
    }
    
    public void Visit(AssNode n)
    {
        AssNodeGen(n);
        EmitLine(";");
    }

    private void AssNodeGen(AssNode n)
    {
        // id = expr;
        // TODO: if assignments can be used in expressions, this must be redone
        n.Id.Accept(this);
        if (n.ArrIndex != null)
        {
            EmitAppend("[");
            n.ArrIndex.Accept(this);
            EmitAppend("]");
        }
        EmitAppend(" = ");
        n.Expr.Accept(this);
    }
    
    public void Visit(IdNode n)
    {
        EmitAppend(n.Id);
    }

    public void Visit(PlusNode n)
    {
        // If the overall expression has type string, we must convert any non-string children to strings
        if (n.Type.ActualType is TypeVal.String && n.Left.Type.ActualType != TypeVal.String)
        {
            // String(left) + right
            EmitAppend("String(");
            n.Left.Accept(this);
            EmitAppend(") + ");
            n.Right.Accept(this);
        }
        else if (n.Type.ActualType is TypeVal.String && n.Right.Type.ActualType != TypeVal.String)
        {
            // left + String(right)
            n.Left.Accept(this);
            EmitAppend(" + String(");
            n.Right.Accept(this);
            EmitAppend(")");
        }
        else
        {
            // left + right
            n.Left.Accept(this);
            EmitAppend(" + ");
            n.Right.Accept(this);
        }
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
        // -expr
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
        
        //Each if-statement creates the for-loop header with the relevant type of initializer
        if (n.Initializer is VarDclNode varInit)
        {
            VarDclNodeGen(varInit);
            ForNodeHelper(varInit.Left, n);
        }
        else if (n.Initializer is AssNode assInit)
        {
            AssNodeGen(assInit);
            ForNodeHelperAssNode(assInit, n);
        }
        else
        {
            n.Initializer.Accept(this);
            ForNodeHelper(n.Initializer, n);
        }
        
        n.Update.Accept(this);
        
        EmitLine(") {");
        foreach (StmtNode statement in n.Body)
        {
            statement.Accept(this);
        }
        EmitLine("}");
        
    }

    private void ForNodeHelper(AstNode node, ForNode n)
    {
        EmitAppend("; ");
        //Generate Limit Condition
        node.Accept(this);
        EmitAppend(" <= ");
        n.Limit.Accept(this);
        EmitAppend("; ");
        //Generate part of the Update Condition
        node.Accept(this);
        EmitAppend(" = ");
        node.Accept(this);
        EmitAppend(" + ");
    }

    private void ForNodeHelperAssNode(AssNode node, ForNode n)
    {
        EmitAppend("; ");
        //Generate Limit Condition
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(" <= ");
        n.Limit.Accept(this);
        EmitAppend("; ");
        //Generate part of the Update Condition
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(" = ");
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(" + ");
    }

    private void ForNodeHelperAssNodeHelper(AssNode node)
    {
        node.Id.Accept(this);
        if (node.ArrIndex != null)
        {
            EmitAppend("[");
            node.ArrIndex.Accept(this);
            EmitAppend("]");
        }
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
        EmitAppend(n.ReturnType);
        n.Name.Accept(this);
        EmitAppend("(");
        for (int i = 0; i < n.FormalParams.Count; i++)
        {
            var param = n.FormalParams[i];
            EmitAppend(param.Type);
            param.Accept(this);
            if (i < n.FormalParams.Count - 1)
            {
                EmitAppend(", ");
            }
        }
        EmitLine(") {");
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
            EmitLine(";");
        }
    }

    public void Visit(IfNode n)
    {
        /* if(condition) {
         *   ThenClause
         * }
         * else if(condition) {
         *   ElseIfClause
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
        
        if (n.ElseIfClauses?.Count > 0)
        {
            foreach (var elseIf in n.ElseIfClauses)
            {
                elseIf.Accept(this);
            }
        }
        
        if (n.ElseClause?.Count > 0)
        {
            EmitLine("else {");
            foreach (var stmt in n.ElseClause)
            {
                stmt.Accept(this);
            }
            EmitLine("}");
        }
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
        if (n.Body?.Count > 0)
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
}