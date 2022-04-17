namespace STEP.AST;

public class CloseGlobalScopeException : Exception
{
    public CloseGlobalScopeException(string message) : base(message)
    {
    }
}