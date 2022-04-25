namespace STEP.AST;

public class DuplicateDeclarationException : Exception 
{
    public DuplicateDeclarationException(string message) : base(message) {}
    public DuplicateDeclarationException(string message, string variableId) : this(message) 
    {
        VariableId = variableId;
    }
    public string VariableId { get; set; }
}