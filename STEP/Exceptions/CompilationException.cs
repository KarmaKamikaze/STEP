using STEP.AST;
using System.Runtime.Serialization;

namespace STEP.Exceptions;

public class CompilationException : Exception
{
    public CompilationException()
    {
    }

    public CompilationException(string message) : base(message)
    {
    }

    public CompilationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CompilationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CompilationException(string message, SourcePosition sourcePosition) : base(message)
    {
        SourcePosition = sourcePosition;
    }

    public SourcePosition SourcePosition { get; }
}

