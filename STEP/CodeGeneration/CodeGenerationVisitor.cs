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
        Console.Write(line+" ");
    }
    
    public void Visit(AndNode n)
    {
        // E.g.: x and true -> x && true 
        n.Left.Accept(this);
        EmitAppend("&&");
        n.Right.Accept(this);
    }

    public void Visit(OrNode n)
    {
        // E.g.: x or true -> x || true 
        n.Left.Accept(this);
        EmitAppend("||");
        n.Right.Accept(this);
    }

    public void Visit(EqNode n)
    {
        // E.g.: x == true -> x == true 
        n.Left.Accept(this);
        EmitAppend("==");
        n.Right.Accept(this);
    }

    public void Visit(NeqNode n)
    {
        // E.g.: x != true -> x != true 
        n.Left.Accept(this);
        EmitAppend("!=");
        n.Right.Accept(this);
    }

    public void Visit(GThanNode n)
    {
        // E.g.: x > true -> x > true 
        n.Left.Accept(this);
        EmitAppend(">");
        n.Right.Accept(this);
    }

    public void Visit(GThanEqNode n)
    {
        // E.g.: x >= true -> x >= true 
        n.Left.Accept(this);
        EmitAppend(">=");
        n.Right.Accept(this);
    }

    public void Visit(LThanNode n)
    {
        // E.g.: x < true -> x < true 
        n.Left.Accept(this);
        EmitAppend("<");
        n.Right.Accept(this);
    }

    public void Visit(LThanEqNode n)
    {
        // E.g.: x <= true -> x <= true 
        n.Left.Accept(this);
        EmitAppend("<=");
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
        switch (n.Type)
        {
            case TypeVal.Number:
                break;
        }
        
        //int id[2];
        //int id[] = {2,1}
        //int id[3] = {1,2,3}
    }

    public void Visit(ArrLiteralNode n)
    {
    }

    public void Visit(ArrayAccessNode n)
    {
    }

    public void Visit(VarDclNode n)
    {
    }

    public void Visit(AssNode n)
    {
    }

    public void Visit(IdNode n)
    {
        EmitAppend(n.Id);
    }

    public void Visit(PlusNode n)
    {
    }

    public void Visit(MinusNode n)
    {
    }

    public void Visit(MultNode n)
    {
    }

    public void Visit(DivNode n)
    {
    }

    public void Visit(PowNode n)
    {
    }

    public void Visit(ParenNode n)
    {
    }

    public void Visit(UMinusNode n)
    {
    }

    public void Visit(WhileNode n)
    {
    }

    public void Visit(ForNode n)
    {
    }

    public void Visit(ContNode n)
    {
    }

    public void Visit(BreakNode n)
    {
    }

    public void Visit(LoopNode n)
    {
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
    }

    public void Visit(IfNode n)
    {
    }

    public void Visit(VarsNode n)
    {
    }

    public void Visit(NullNode n)
    {
    }

    public void Visit(ProgNode n)
    {
    }

    public void Visit(SetupNode n)
    {
    }
}