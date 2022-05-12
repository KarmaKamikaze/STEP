using STEP.AST;

namespace STEP.Exceptions;

public class ParameterCountMismatchException : CompilationException
{
    public ParameterCountMismatchException(string message, SourcePosition sourcePosition, int expected, int actual)
        : base(message, sourcePosition)
    {
        ExpectedCount = expected;
        ActualCount = actual;
    }

    public ParameterCountMismatchException(string message, SourcePosition sourcePosition, int expected, int actual, string variableId)
        : this(message, sourcePosition, expected, actual)
    {
        VariableId = variableId;
    }

    public int ExpectedCount { get; }
    public int ActualCount { get; }
    public string VariableId { get; }
}