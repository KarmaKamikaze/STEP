using STEP.AST.Nodes;
using System.Runtime.Serialization;

namespace STEP;

internal class PinTableUnexpectedTypeException : Exception
{
    public PinTableUnexpectedTypeException()
    {
    }

    public PinTableUnexpectedTypeException(string message) : base(message)
    {
    }

    public PinTableUnexpectedTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected PinTableUnexpectedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PinTableUnexpectedTypeException(string message, TypeVal actual, params TypeVal[] expected) : base(message) 
    {
        Actual = actual;
        Expected = new(expected);
    }

    public List<TypeVal> Expected { get; }
    public TypeVal Actual { get; }

}

