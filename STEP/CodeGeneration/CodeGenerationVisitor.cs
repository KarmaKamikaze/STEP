using System.Globalization;
using System.Reflection;
using System.Text;
using STEP.AST.Nodes;

namespace STEP.CodeGeneration;

public class CodeGenerationVisitor : IVisitor
{
    private readonly StringBuilder _stringBuilder = new();
    private string Output => _stringBuilder.ToString();
    private readonly StringBuilder _pinSetup = new();
    private readonly StringBuilder _memcpySetup = new();
    private int _scopeLevel;
    private int _tempCount;
    private readonly List<IdNode> _arrDclsInScope = new(); // Keeps track of array declarations currently in scope

    public void OutputToBaseFile(string filename)
    {
        string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Directory.CreateDirectory(directoryPath + $"/{filename}/");
        File.WriteAllText(directoryPath + $"/{filename}/{filename}.ino", Output);
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

    private void EmitAppend(Type type, string suffix = " ")
    {
        if (type.IsConstant)
            EmitAppend("const" + suffix);

        switch (type.ActualType)
        {
            case TypeVal.Number:
                EmitAppend("double" + suffix);
                break;
            case TypeVal.String:
                EmitAppend("String" + suffix);
                break;
            case TypeVal.Boolean:
                EmitAppend("boolean" + suffix);
                break;
            case TypeVal.Blank:
                EmitAppend("void" + suffix);
                break;
        }
    }

    private void EnterScope()
    {
        _scopeLevel++;
    }

    private void ExitScope()
    {
        FreeArrays(_scopeLevel - 1);
        _scopeLevel--;
    }

    // Called upon exiting any scope
    private void FreeArrays(int scopeLevel)
    {
        var arrsToRemove = new List<IdNode>();
        foreach (var id in _arrDclsInScope.Where(n => n.Type.ScopeLevel > scopeLevel))
        {
            if (!id.Type.IsReturned)
            {
                EmitIndentation();
                EmitLine($"free({id.Name});");
            }

            arrsToRemove.Add(id);
        }

        arrsToRemove.ForEach(id => _arrDclsInScope.Remove(id));
    }

    // Called upon exiting array function, handles freeing excess arrays
    private void FreeArrays(int scopeLevel, IdNode exemptId, bool shouldRemove)
    {
        var arrsToRemove = new List<IdNode>();
        foreach (var id in _arrDclsInScope.Where(n => n.Type.ScopeLevel > scopeLevel && !n.Equals(exemptId)))
        {
            EmitLine($"free({id.Name});");
            EmitIndentation();
            arrsToRemove.Add(id);
        }

        if (shouldRemove) arrsToRemove.ForEach(id => _arrDclsInScope.Remove(id));
    }

    private void CopyArrayHelper(ArrDclNode n)
    {
        // Copies array from RHS into LHS
        EmitIndentation();
        var id = n.Right as IdNode;
        EmitLine(
            $"memcpy({n.Left.Name}, {id.Name}, sizeof({id.Name}[0])*{Math.Min(n.Left.Type.ArrSize, id.Type.ArrSize)});");
    }

    private void CopyArrayHelper(ArrDclNode n, ArrLiteralNode lit)
    {
        // Copies array from arr literal into LHS
        string tempName = $"__temp{_tempCount - 1}__";
        string memcpyString = $"memcpy({n.Left.Name}, {tempName}, sizeof({tempName}[0])*{Math.Min(n.Left.Type.ArrSize, lit.Type.ArrSize)});";
        // Calls to memcpy are not allowed in the "global" scope, so it must be moved to setup()
        if (n.Type.ScopeLevel == 0)
        {
            _memcpySetup.Append(' ', 4); // Ensure that it is correctly indented in setup()
            _memcpySetup.AppendLine(memcpyString);
        }
        else
        {
            EmitIndentation();
            EmitLine(memcpyString);
        }
    }

    // Adds tabs to format output code
    private void EmitIndentation()
    {
        _stringBuilder.Append(' ', _scopeLevel * 4);
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

    public virtual void Visit(NumberNode n)
    {
        EmitAppend(n.Value.ToString(CultureInfo.InvariantCulture));
    }

    public void Visit(StringNode n)
    {
        EmitAppend(n.Value);
    }

    public void Visit(BoolNode n)
    {
        EmitAppend(n.Value.ToString().ToLowerInvariant());
    }

    public void Visit(ArrDclNode n)
    {
        // e.g. double* studentAges = (double*)malloc(5 * sizeof(double));
        // equivalent to double[5] studentAges;
        EmitAppend(n.Type, "* ");
        n.Left.Accept(this);
        EmitAppend($" = (");
        EmitAppend(n.Type, "*");
        EmitAppend($")calloc({n.Size}, sizeof(");
        EmitAppend(n.Type, "))");
        EmitLine(";");
        // Copy array
        if (n.Right is IdNode)
        {
            CopyArrayHelper(n);
        }
        else if (n.Right is ArrLiteralNode lit)
        {
            n.Right.Accept(this);
            CopyArrayHelper(n, lit);
        }

        _arrDclsInScope.Add(n.Left);
    }

    public void Visit(ArrLiteralNode n)
    {
        int count = n.Elements.Count;
        if (count == 0) return;       
        EmitIndentation();
        EmitAppend(n.Type);
        EmitAppend($"__temp{_tempCount}__[] = {{");
        for (int i = 0; i < count; i++)
        {
            n.Elements[i].Accept(this);
            if (i < count - 1)
            {
                // Add comma after all but the last element
                EmitAppend(", ");
            }
        }

        EmitLine("};");
        _tempCount++;
    }

    public void Visit(ArrayAccessNode n)
    {
        n.Array.Accept(this);
        EmitAppend("[(int) ");
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
        EmitAppend(n.Left.Type);

        n.Left.Accept(this);
        EmitAppend(" = ");
        n.Right.Accept(this);
    }

    public void Visit(AssNode n)
    {
        // If assigning new pointer to array pointer, free previously allocated memory
        // Unless RHS is func expression with array in params
        if (n.Id.Type.IsArray && n.ArrIndex is null)
        {
            if (n.Expr is FuncExprNode fen)
            {
                if (!fen.Params.Any(param => ((IdNode) param).Name.Equals(n.Id.Name)))
                {
                    EmitLine($"free({n.Id.Name});");
                    EmitIndentation();
                }
            }
        }

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
            EmitAppend("[(int) ");
            n.ArrIndex.Accept(this);
            EmitAppend("]");
        }

        EmitAppend(" = ");
        n.Expr.Accept(this);
    }

    public void Visit(IdNode n)
    {
        EmitAppend(n.Name);
    }

    public void Visit(PlusNode n)
    {
        switch (n.Type.ActualType)
        {
            // If the overall expression has type string, we must convert any non-string children to strings
            case TypeVal.String when n.Left.Type.ActualType != TypeVal.String:
                // String(left) + right
                EmitAppend("String(");
                n.Left.Accept(this);
                EmitAppend(") + ");
                n.Right.Accept(this);
                break;
            case TypeVal.String when n.Right.Type.ActualType != TypeVal.String:
                // left + String(right)
                n.Left.Accept(this);
                EmitAppend(" + String(");
                n.Right.Accept(this);
                EmitAppend(")");
                break;
            default:
                // left + right
                n.Left.Accept(this);
                EmitAppend(" + ");
                n.Right.Accept(this);
                break;
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
        EnterScope();
        foreach (StmtNode statement in n.Body)
        {
            EmitIndentation();
            statement.Accept(this);
        }

        ExitScope();
        EmitIndentation();
        EmitLine("}");
    }

    public void Visit(ForNode n)
    {
        // for(int x = 0; i < n; i++)
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
        
        // Only emit value
        if (n.Update is UMinusNode un) {
            un.Left.Accept(this);
        }
        else {
            n.Update.Accept(this);
        }

        EmitLine(") {");
        EnterScope();
        foreach (StmtNode statement in n.Body)
        {
            EmitIndentation();
            statement.Accept(this);
        }

        ExitScope();
        EmitIndentation();
        EmitLine("}");
    }

    private void ForNodeHelper(AstNode node, ForNode n) {
        EmitAppend("; ");
        //Generate Limit Condition
        node.Accept(this);
        EmitAppend(n.IsUpTo ? " <= " : " >= ");
        n.Limit.Accept(this);
        EmitAppend("; ");
        //Generate part of the Update Condition
        node.Accept(this);
        EmitAppend(" = ");
        node.Accept(this);
        EmitAppend(n.Update is not UMinusNode ? " + " : " - ");
    }

    private void ForNodeHelperAssNode(AssNode node, ForNode n)
    {
        EmitAppend("; ");
        //Generate Limit Condition
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(n.IsUpTo ? " <= " : " >= ");
        n.Limit.Accept(this);
        EmitAppend("; ");
        //Generate part of the Update Condition
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(" = ");
        ForNodeHelperAssNodeHelper(node);
        EmitAppend(n.Update is not UMinusNode ? " + " : " - ");
    }

    private void ForNodeHelperAssNodeHelper(AssNode node)
    {
        node.Id.Accept(this);
        if (node.ArrIndex != null)
        {
            EmitAppend("[(int) ");
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
        EmitAppend(n.ReturnType, n.Type.IsArray ? "* " : " ");
        n.Id.Accept(this);
        EmitAppend("(");
        for (int i = 0; i < n.FormalParams.Count; i++)
        {
            var param = n.FormalParams[i];
            EmitAppend(param.Type);
            param.Accept(this);
            if (param.Type.IsArray)
            {
                EmitAppend("[]");
            }

            if (i < n.FormalParams.Count - 1)
            {
                EmitAppend(", ");
            }
        }

        EmitLine(") {");
        // Body
        EnterScope();
        foreach (var stmt in n.Stmts)
        {
            EmitIndentation();
            stmt.Accept(this);
        }

        ExitScope();
        EmitIndentation();
        EmitLine("}\r\n");
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
        EmitLine("// Functions");
        foreach (var funDcl in n.FuncDcls)
        {
            funDcl.Accept(this);
        }
    }

    public void Visit(RetNode n)
    {
        // If no expression, emit empty return
        if (n.RetVal is null)
        {
            EmitLine("return;");
        }
        else
        {
            // If returning from function, free arrays declared in scope
            if (n.SurroundingFuncType.IsArray)
            {
                // If in outer scope, free all arrays except returned and remove from list - else free without removing from list
                // Typed functions always end on return, so this is where we 'forget' about the obsolete arrays
                // In inner scopes, we want to free them, but still 'remember' them to free at any returns in outer scopes
                bool isInOuterScope = n.Type.ScopeLevel - 1 == n.SurroundingFuncType.ScopeLevel;
                FreeArrays(n.SurroundingFuncType.ScopeLevel, (IdNode) n.RetVal, isInOuterScope);
            }

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
        EnterScope();
        foreach (var stmt in n.ThenClause)
        {
            EmitIndentation();
            stmt.Accept(this);
        }

        ExitScope();
        EmitIndentation();
        EmitLine("}");

        if (n.ElseIfClauses?.Count > 0)
        {
            foreach (var elseIf in n.ElseIfClauses)
            {
                EmitIndentation();
                elseIf.Accept(this);
            }
        }

        if (n.ElseClause?.Count > 0)
        {
            EmitIndentation();
            EmitLine("else {");
            EnterScope();
            foreach (var stmt in n.ElseClause)
            {
                EmitIndentation();
                stmt.Accept(this);
            }

            ExitScope();
            EmitIndentation();
            EmitLine("}");
        }
    }

    public void Visit(VarsNode n)
    {
        if (n.Dcls.Count > 0) EmitLine("// Global variables");
        foreach (var dclNode in n.Dcls)
        {
            dclNode.Accept(this);
        }

        EmitLine("");
    }

    public void Visit(ProgNode n)
    {
        if (n.FuncsBlock != null && n.VarsBlock != null)
        {
            EmitFunctionPrototypes(n.FuncsBlock);
        }

        n.VarsBlock?.Accept(this);
        n.FuncsBlock?.Accept(this);
        EmitLine("void setup() {");
        EmitLine("    Serial.begin(9600);");
        // Add memcpy calls for globally defined array
        EmitAppend(_memcpySetup.ToString());
        // Add declared pinModes from variables scope
        EmitAppend(_pinSetup.ToString());
        n.SetupBlock?.Accept(this);
        EmitLine("}");
        EmitLine("void loop() {");
        n.LoopBlock?.Accept(this);
        EmitLine("}");
    }

    private void EmitFunctionPrototypes(FuncsNode n)
    {
        EmitLine("// Function prototypes");
        foreach (var dcl in n.FuncDcls)
        {
            EmitAppend(dcl.ReturnType, dcl.ReturnType.IsArray ? "* " : " ");
            EmitAppend(dcl.Id.Name + "(");
            for (int i = 0; i < dcl.FormalParams.Count; i++)
            {
                var param = dcl.FormalParams[i];
                EmitAppend(param.Type, param.Type.IsArray ? "*" : "");
                if (i < dcl.FormalParams.Count - 1)
                {
                    EmitAppend(", ");
                }
            }
            EmitLine(");");
        }
        EmitLine("");
    }

    public void Visit(SetupNode n)
    {
        EnterScope();

        foreach (var stmt in n.Stmts)
        {
            EmitIndentation();
            stmt.Accept(this);
        }

        ExitScope();
        EmitIndentation();
    }

    public void Visit(LoopNode n)
    {
        EnterScope();
        foreach (var stmt in n.Stmts)
        {
            EmitIndentation();
            stmt.Accept(this);
        }

        ExitScope();
        EmitIndentation();
    }

    public void Visit(ElseIfNode n)
    {
        if (n.Body?.Count > 0)
        {
            EmitAppend("else if(");
            n.Condition.Accept(this);
            EmitLine(") {");
            EnterScope();
            foreach (var stmt in n.Body)
            {
                EmitIndentation();
                stmt.Accept(this);
            }

            ExitScope();
            EmitIndentation();
            EmitLine("}");
        }
    }

    public void Visit(PinDclNode n)
    {
        PinCodeVisitor pinVisitor = new PinCodeVisitor();
        /* The emitted code will be stored in a temporary variable. If any pins are declared, we know
         * it must be declared in the variables scope, which is always visited first. Once the code generation
         * reaches the Setup scope, the temporary variables will be used to insert this code in the correct place.
         */
        _pinSetup.Append("    pinMode(");
        n.Right.Accept(pinVisitor);
        // Append A if the pin is analog to allow for arduino to
        // differentiate between analog and digital pins
        if (n.Left.Type.ActualType is TypeVal.Analogpin)
        {
            _pinSetup.Append('A');
        }

        _pinSetup.Append(pinVisitor.GetPinCode() + ", ");
        switch (((PinType) n.Type).Mode)
        {
            case PinMode.INPUT:
                _pinSetup.Append("INPUT");
                break;
            case PinMode.OUTPUT:
                _pinSetup.Append("OUTPUT");
                break;
        }

        _pinSetup.Append(");\r\n");

        // Save variable names as constant declarations and prepend to generated code
        string variableConstant = $"#define {n.Left.Name} {pinVisitor.GetPinCode()}\r\n";
        _stringBuilder.Insert(0, variableConstant);
    }
}