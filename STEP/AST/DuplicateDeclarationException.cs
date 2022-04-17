namespace STEP.AST;

public class DuplicateDeclarationException : Exception 
{
    public DuplicateDeclarationException(string message, string variableId) : base(message) 
    {
        VariableId = variableId;
    }
    public string VariableId { get; set; }
}