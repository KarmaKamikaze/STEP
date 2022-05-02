using STEP.AST;

namespace STEP;

public sealed class SymbolNotDeclaredException : CompilationException
{
    public SymbolNotDeclaredException()
    {
    }

    public SymbolNotDeclaredException(string message) : base(message)
    {
    }

    public SymbolNotDeclaredException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public SymbolNotDeclaredException(string message, SourcePosition sourcePosition) : base(message, sourcePosition)
    {
    }

    public SymbolNotDeclaredException(string message, SourcePosition sourcePosition, string variableId)
        : base(message, sourcePosition)
    {
        VariableId = variableId;
    }

    public string VariableId { get; }
}