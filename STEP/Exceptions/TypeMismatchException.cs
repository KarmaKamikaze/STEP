using STEP.AST;
using System.Runtime.Serialization;
using Type = STEP.AST.Type;

namespace STEP.Exceptions;

public class TypeMismatchException : CompilationException
{
    public TypeMismatchException()
    {
    }

    public TypeMismatchException(string message) : base(message)
    {
    }

    public TypeMismatchException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TypeMismatchException(string message, SourcePosition sourcePosition) : base(message, sourcePosition)
    {

    }

    public TypeMismatchException(string message, SourcePosition sourcePosition, Type actual, params Type[] expected)
        : this(message, sourcePosition)
    {
        Expected = new(expected);
        Actual = actual;
    }

    //public TypeMismatchException(string message, SourcePosition sourcePosition, Type expected, Type actual)
    //    : this(message, sourcePosition)
    //{
    //    Expected = new List<Type> { expected };
    //    Actual = actual;
    //}

    public TypeMismatchException(string message, SourcePosition sourcePosition, Type expected, Type actual, string variableId)
        : this(message, sourcePosition, expected, actual)
    {
        VariableId = variableId;
    }

    protected TypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public string VariableId { get; }
    public List<Type> Expected { get; }
    public Type Actual { get; }
}