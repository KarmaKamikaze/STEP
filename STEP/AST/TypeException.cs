using STEP.AST.Nodes;

namespace STEP.AST; 

public class TypeException : Exception {
    public TypeException(AstNode n, string s) : base(s) {
        ErrorNode = n;
    }

    public TypeException(AstNode n, TypeVal a1, TypeVal a2) : base(
        $"Type mismatch, expected matching types - actual types: ({a1}, {a2})") {
        ErrorNode = n;
    }

    public TypeException(AstNode n, TypeVal expected, TypeVal a1, TypeVal a2) : base(
        $"Type mismatch, expected types: ({expected}, {expected}) - actual types: ({a1}, {a2})") {
        ErrorNode = n;
    }

    public TypeException(AstNode n, TypeVal expected1, TypeVal expected2, TypeVal a1, TypeVal a2) : base(
        $"Type mismatch, allowed types: {expected1} or {expected2} - actual types: ({a1}, {a2})") {
        ErrorNode = n;
    }
    public AstNode ErrorNode { get; set; }
}