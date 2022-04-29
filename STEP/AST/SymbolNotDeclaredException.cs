namespace STEP.AST;

public class SymbolNotDeclaredException : Exception
{
    public SymbolNotDeclaredException(string idName) : base($"{idName} is not declared in current scope")
    {
    }
}