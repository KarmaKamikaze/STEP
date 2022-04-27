using System.Runtime.CompilerServices;
using Antlr4.Runtime.Misc;

namespace STEP.AST.Nodes;

public abstract class AstNode
{
    public AstNodeType NodeType { get; init; }
    public Type Type { get; set; } = new();

    public abstract void Accept(IVisitor v);
}