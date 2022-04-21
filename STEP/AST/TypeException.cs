using STEP.AST.Nodes;

namespace STEP.AST; 

public class TypeException : Exception {
    public TypeException(string idName) : base($"{idName} is not declared in current scope") {}
    public TypeException(TypeVal expected, AstNode actual) : base($"Type mismatch, expected type: {expected} - actual type: {actual.Type}") { } // Kinda hacky - different message
    public TypeException(TypeVal a1, TypeVal a2) : base($"Type mismatch, expected matching types - actual types: ({a1}, {a2})") { }
    public TypeException(TypeVal expected, TypeVal a1, TypeVal a2) : base($"Type mismatch, expected types: ({expected}, {expected}) - actual types: ({a1}, {a2})") { }
    public TypeException(TypeVal expected, TypeVal a1, TypeVal a2, AstNode a3) : base($"Type mismatch, expected types: ({expected}, {expected}, {expected}) - actual types: ({a1}, {a2}, {a3.Type})") { } // Hacky again - three types. Counting on you guys, the code reviewers, to fix this!
    public TypeException(TypeVal expected1, TypeVal expected2, TypeVal a1, TypeVal a2) : base($"Type mismatch, allowed types: {expected1} or {expected2} - actual types: ({a1}, {a2})") { }
}