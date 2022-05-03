using Antlr4.Runtime;

namespace STEP;

public class STEPErrorListener : BaseErrorListener {
    // Throw exception upon error, ending compilation rather than continuing parse
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e) {
        throw new SemanticException(e, msg, line, charPositionInLine);
    }
}