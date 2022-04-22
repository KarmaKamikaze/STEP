namespace STEP.AST; 

public class UnexpectedTypeException : Exception {
    public UnexpectedTypeException(string msg) : base(msg) {}
}