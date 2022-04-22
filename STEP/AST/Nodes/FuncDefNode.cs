namespace STEP.AST.Nodes;

public class FuncDefNode : AstNode
{
    public IdNode Name { get; set; }
    public IdNode ReturnType { get; set; }
    public Dictionary<IdNode, (TypeVal, bool)> FormalParams { get; set; } // Tuple boolean is true when param is array
    public List<StmtNode> Stmts { get; set; }
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is FuncDefNode other)
        {
            return Equals(other.Name, Name)
                && Equals(other.ReturnType, ReturnType)
                && FormalParams.SequenceEqual(other.FormalParams)
                && Stmts.SequenceEqual(other.Stmts);

        }
        return base.Equals(obj);
    }
}