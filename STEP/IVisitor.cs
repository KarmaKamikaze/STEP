using STEP.AST.Nodes;

namespace STEP; 

public interface IVisitor {
    // Logic nodes
    public void Visit(AndNode n);
    public void Visit(OrNode n);
    public void Visit(EqNode n);
    public void Visit(NeqNode n);
    public void Visit(GThanNode n);
    public void Visit(GThanEqNode n);
    public void Visit(LThanNode n);
    public void Visit(LThanEqNode n);
    public void Visit(NegNode n);
    
    // Expression nodes
    public void Visit(NumberNode n);
    public void Visit(StringNode n);
    public void Visit(BoolNode n);
    public void Visit(ArrDclNode n);
    public void Visit(ArrLiteralNode n);
    public void Visit(ArrayAccessNode n);
    public void Visit(VarDclNode n);
    public void Visit(AssNode n);
    public void Visit(IdNode n);
    public void Visit(PlusNode n);
    public void Visit(MinusNode n);
    public void Visit(MultNode n);
    public void Visit(DivNode n);
    public void Visit(PowNode n);
    public void Visit(ParenNode n);
    public void Visit(UMinusNode n);
    
    // Loop nodes
    public void Visit(WhileNode n);
    public void Visit(ForNode n);
    public void Visit(ContNode n);
    public void Visit(BreakNode n);
    public void Visit(LoopNode n);
    
    // General nodes
    public void Visit(FuncDefNode n);
    public void Visit(FuncExprNode n);
    public void Visit(FuncStmtNode n);
    public void Visit(FuncsNode n);
    public void Visit(RetNode n);
    public void Visit(IfNode n);
    public void Visit(ElseIfNode n);
    public void Visit(VarsNode n);
    public void Visit(ProgNode n);
    public void Visit(SetupNode n);
}