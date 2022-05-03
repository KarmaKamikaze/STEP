using Antlr4.Runtime;

namespace STEP;

public class SyntaxException : Exception {
    public readonly RecognitionException RecognitionException;
    public readonly int Line;
    public readonly int CharPositionInLine;
    
    public SyntaxException(RecognitionException recognitionException, string msg, int line, int charPositionInLine) : base(msg) {
        RecognitionException = recognitionException;
        Line = line;
        CharPositionInLine = charPositionInLine;
    }
}