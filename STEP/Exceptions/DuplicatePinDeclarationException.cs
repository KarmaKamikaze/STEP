using STEP.AST;

namespace STEP.Exceptions;

public class DuplicatePinDeclarationException : DuplicateDeclarationException
{
    public DuplicatePinDeclarationException()
    {
    }

    public DuplicatePinDeclarationException(string message) : base(message)
    {
    }

    public DuplicatePinDeclarationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public DuplicatePinDeclarationException(string message, SourcePosition sourcePosition) : base(message, sourcePosition)
    {
    }

    public DuplicatePinDeclarationException(string message, SourcePosition sourcePosition, string variableId, int pin) : base(message, sourcePosition, variableId)
    {
        Pin = pin;
    }

    public int Pin { get; }
}