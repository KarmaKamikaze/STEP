using Antlr4.Runtime;

namespace STEP;

public class SemanticException : Exception {
    public readonly RecognitionException RecognitionException;
    public readonly int Line;
    public readonly int CharPositionInLine;
    
    public SemanticException(RecognitionException recognitionException, string msg, int line, int charPositionInLine) : base(msg) {
        RecognitionException = recognitionException;
        Line = line;
        CharPositionInLine = charPositionInLine;
    }
}