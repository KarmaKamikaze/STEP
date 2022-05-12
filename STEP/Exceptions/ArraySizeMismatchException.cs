using STEP.AST;
using STEP.AST.Nodes;

namespace STEP.Exceptions;

public sealed class ArraySizeMismatchException : CompilationException
{
    public ArraySizeMismatchException()
    {
    }

    public ArraySizeMismatchException(string message) : base(message)
    {
    }

    public ArraySizeMismatchException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ArraySizeMismatchException(string message, SourcePosition sourcePosition) : base(message, sourcePosition)
    {
    }

    public ArraySizeMismatchException(string message, SourcePosition sourcePosition, IdNode sourceArray, IdNode destinationArray)
        : base(message, sourcePosition)
    {
        DestinationArray = destinationArray;
        SourceArray = sourceArray;
    }

    public IdNode DestinationArray { get; }
    public IdNode SourceArray { get; }
}

