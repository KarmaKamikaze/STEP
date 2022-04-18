using STEP.AST.Nodes;

namespace STEP; 

public class TypeVisitor : IVisitor {
    // Logic nodes
    
    public void Visit(AndNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean && n.Right.ExprType == TypeVal.Boolean) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(OrNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean && n.Right.ExprType == TypeVal.Boolean) {
                n.ExprType = TypeVal.Boolean;
        } else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(EqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == n.Right.ExprType && n.Left.ExprType != TypeVal.String) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(NeqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == n.Right.ExprType && n.Left.ExprType != TypeVal.String) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(GThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(GThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(LThanNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(LThanEqNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(NegNode n) {
        n.Left.Accept(this);
        if (n.Left.ExprType == TypeVal.Boolean) {
            n.ExprType = TypeVal.Boolean;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }
    
    // Expression nodes

    public void Visit(NumberNode n) { }

    public void Visit(StringNode n) { }

    public void Visit(BoolNode n) { }

    public void Visit(ArrDclNode n) {
        // Halp 
    }

    public void Visit(VarDclNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.Type == n.Right.ExprType) {
            n.Type = TypeVal.Ok;
        }
        else {
            n.Type = TypeVal.Error;
        }
    }

    public void Visit(ExprNode n) {
        throw new NotImplementedException();
    }

    public void Visit(AssNode n) {
        n.Expr.Left.Accept(this);
        n.Expr.Right.Accept(this);
        if (n.Id.Type == n.Expr.ExprType) {
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }

    public void Visit(IdNode n) {
        throw new NotImplementedException();
    }

    public void Visit(PlusNode n) {
         n.Left.Accept(this);
         n.Right.Accept(this);
         if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number)
         {
             n.ExprType = TypeVal.Number;
         }
         else if (n.Left.ExprType == TypeVal.String || n.Right.ExprType == TypeVal.String) {
             n.ExprType = TypeVal.String;
         }
         else {
             n.ExprType = TypeVal.Error;
         }
    }

    public void Visit(MinusNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(MultNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(DivNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(PowNode n) {
        n.Left.Accept(this);
        n.Right.Accept(this);
        if (n.Left.ExprType == TypeVal.Number && n.Right.ExprType == TypeVal.Number) {
            n.ExprType = TypeVal.Number;
        }
        else {
            n.ExprType = TypeVal.Error;
        }
    }

    public void Visit(ParenNode n) {
        n.Left.Accept(this);
        n.ExprType = n.Left.ExprType;
    }

    public void Visit(UMinusNode n) {
        n.Left.Accept(this);
        if (n.Left.TypeVal == TypeVal.Number) {
            n.TypeVal = TypeVal.Number;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }
    
    // Loop nodes

    public void Visit(WhileNode n) {
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this); // i sure hope dynamic dispatch works monkaW
        }
        n.Condition.Accept(this);
        if (n.Condition.TypeVal == TypeVal.Boolean) {
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }

    public void Visit(ForNode n) {
        foreach (var stmtNode in n.Body) {
            stmtNode.Accept(this);
        }

        n.Initializer.Accept(this);
        n.Limit.Accept(this);
        n.Update.Accept(this);
        if (n.Initializer.TypeVal == TypeVal.Number && n.Limit.TypeVal == TypeVal.Number && n.Update.TypeVal == TypeVal.Number) { // If it's a constant, it's number.. If it's vardcl, it's Ok??? 
            n.TypeVal = TypeVal.Ok;
        }
        else {
            n.TypeVal = TypeVal.Error;
        }
    }

    public void Visit(ContNode n) { } // ?

    public void Visit(BreakNode n) { } // ?
    
    // General nodes

    public void Visit(FuncDefNode n) {
        n.Name.Accept(this);
        foreach (var stmtNode in n.Stmts) {
            stmtNode.Accept(this);
        }
        foreach (var formalParam in n.FormalParams) {
            formalParam.Accept(this);
        }

        n.ReturnType.Accept(this);
        n.TypeVal = n.ReturnType.TypeVal; // ?
    }

    public void Visit(FuncExprNode n) {
        throw new NotImplementedException();
    }

    public void Visit(FuncStmtNode n) {
        throw new NotImplementedException();
    }

    public void Visit(StmtNode n) {
        throw new NotImplementedException();
    }

    public void Visit(RetNode n) {
        throw new NotImplementedException();
    }

    public void Visit(VarsNode n) {
        throw new NotImplementedException();
    }
}