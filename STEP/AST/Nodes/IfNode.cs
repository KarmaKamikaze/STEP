using System.Collections.Specialized;

namespace STEP.AST.Nodes;

public class IfNode : StmtNode
{
    public ExprNode Condition { get; set; }
    public List<StmtNode> ThenClause { get; set; } = new();
    // The OrderedDictionary uses ExprNodes as keys and a List of StmtNodes as values
    public OrderedDictionary ElseIfClause { get; set; } = new();
    public List<StmtNode> ElseClause { get; set; } = new();
    public override void Accept(IVisitor v) {
        v.Visit(this);
    }
}