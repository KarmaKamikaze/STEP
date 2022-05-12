using Antlr4.Runtime;
using STEP.Exceptions;

namespace STEP.SyntacticalAnalysis;

public class STEPErrorListener<T> : ConsoleErrorListener<T>
{
    // Throw exception upon error, ending compilation rather than continuing parse
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line,
        int charPositionInLine,
        string msg, RecognitionException e)
    {
        throw new SyntaxException(e, msg, line, charPositionInLine);
    }
}