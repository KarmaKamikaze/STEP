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
    private int _scopeLevel = 0;
    private readonly List<Tuple<int, ArrDclNode>> _arrDclsPerScope = new(); // Keeps track of array declarations per scope
    public void OutputToBaseFile()
    {
        string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        File.WriteAllText(directoryPath + "/compiled.c", Output);
    }

    public string OutputToString()
    {
        return Output;
    }
    
    private void EmitLine(string line) {
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

    private void EnterScope() {
        _scopeLevel++;
    }
    
    private void ExitScope(bool isArrayFunc = false) {
        _scopeLevel--;
        var tuplesToRemove = new List<Tuple<int, ArrDclNode>>();
        // If exiting a function returning an array, do not free declared arrays
        if (isArrayFunc) {
            tuplesToRemove.AddRange(_arrDclsPerScope.Where(t => t.Item1 > _scopeLevel));
        }
        // Else, when exiting a scope, free all arrays declared in the scope
        else {
            foreach (var tuple in _arrDclsPerScope.Where(k => k.Item1 > _scopeLevel)) {
                EmitLine($"free({tuple.Item2.Left.Id});");
                tuplesToRemove.Add(tuple);
            }
        }
        tuplesToRemove.ForEach(t => _arrDclsPerScope.Remove(t));
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
        EmitAppend($")malloc({n.Size} * sizeof(");
        EmitAppend(n.Type, "))");
        EmitLine(";");
        _arrDclsPerScope.Add(new Tuple<int, ArrDclNode>(_scopeLevel, n));
    }

    public void Visit(ArrLiteralNode n)
    {
        int count = n.Elements.Count;
        if (count == 0) return;
        EmitAppend("{");
        EnterScope();
        for (int i = 0; i < count; i++)
        {
            n.Elements[i].Accept(this);
            if (i < count - 1)
            {
                // Add comma after all but the last element
                EmitAppend(", ");
            }
        }

        ExitScope();
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
        EmitAppend(n.Left.Type);

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

    public void Visit(PlusNode n) {
        switch (n.Type.ActualType) {
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
            statement.Accept(this);
        }

        ExitScope();
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
        EnterScope();
        foreach (StmtNode statement in n.Body)
        {
            statement.Accept(this);
        }

        ExitScope();
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
        EmitAppend(n.ReturnType, n.Type.IsArray ? "* " : " ");
        n.Name.Accept(this);
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
        foreach (StmtNode stmt in n.Stmts)
        {
            stmt.Accept(this);
        }

        ExitScope(n.Type.IsArray);
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
        if (n.RetVal is null)
        {
            EmitLine("return;");
        }
        else
        {
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
        foreach(var stmt in n.ThenClause) {
            stmt.Accept(this);
        }

        ExitScope();
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
            EnterScope();
            foreach (var stmt in n.ElseClause)
            {
                stmt.Accept(this);
            }
            ExitScope();
            EmitLine("}");
        }
    }

    public void Visit(VarsNode n)
    {
        foreach (var dclNode in n.Dcls)
        {
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
        EnterScope();
        // Add declared pinModes from variables scope
        if (_pinSetup.ToString() != String.Empty)
            EmitLine(_pinSetup.ToString());

        foreach (var stmt in n.Stmts)
        {
            stmt.Accept(this);
        }

        ExitScope();
        EmitLine("}");
    }

    public void Visit(LoopNode n)
    {
        EmitLine("void loop() {");
        EnterScope();
        foreach(var stmt in n.Stmts) {
            stmt.Accept(this);
        }

        ExitScope();
        EmitLine("}");
    }

    public void Visit(ElseIfNode n)
    {
        if (n.Body?.Count > 0)
        {
            EmitAppend("else if(");
            n.Condition.Accept(this);
            EmitLine(") {");
            EnterScope();
            foreach(var stmt in n.Body)
            {
                stmt.Accept(this);
            }

            ExitScope();
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
        _pinSetup.Append("pinMode(");
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
        string variableConstant = $"#define {n.Left.Id} {pinVisitor.GetPinCode()}\n";
        _stringBuilder.Insert(0, variableConstant);
    }
}