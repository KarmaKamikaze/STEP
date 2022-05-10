using STEP.AST;

namespace STEP.Exceptions;

public class DuplicateDeclarationException : CompilationException
{
    public DuplicateDeclarationException()
    {
    }

    public DuplicateDeclarationException(string message) : base(message)
    {
    }

    public DuplicateDeclarationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public DuplicateDeclarationException(string message, SourcePosition sourcePosition) : base(message, sourcePosition)
    {
    }

    public DuplicateDeclarationException(string message, SourcePosition sourcePosition, string variableId)
        : base(message, sourcePosition)
    {
        VariableId = variableId;
    }

    public string VariableId { get; set; }
}
