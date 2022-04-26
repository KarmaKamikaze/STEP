using STEP.AST.Nodes;

namespace STEP.AST; 

public class TypeException : Exception {
    public TypeException(AstNode n, string s) : base(s) {
        ErrorNode = n;
    }

    public TypeException(AstNode n, Type a1, Type a2) : base(
        $"Type mismatch, expected matching types - actual types: ({a1}, {a2})") {
        ErrorNode = n;
        Actual.Add(a1);
        Actual.Add(a2);
    }

    public TypeException(AstNode n, Type expected, Type a1, Type a2) : base(
        $"Type mismatch, expected types: ({expected}, {expected}) - actual types: ({a1}, {a2})") {
        ErrorNode = n;
        Expected.Add(expected);
        Actual.Add(a1);
        Actual.Add(a2);
    }

    public TypeException(AstNode n, Type expected1, Type expected2, Type a1, Type a2) : base(
        $"Type mismatch, allowed types: {expected1} or {expected2} - actual types: ({a1}, {a2})") {
        ErrorNode = n;
        Expected.Add(expected1);
        Expected.Add(expected2);
        Actual.Add(a1);
        Actual.Add(a2);
    }
    public AstNode ErrorNode { get; }
    public List<Type> Expected { get; } = new();
    public List<Type> Actual { get; } = new();
}