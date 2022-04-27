namespace STEP.AST;

public class FunctionSymTableEntry : SymTableEntry
{
    public Dictionary<string, Type> Parameters = new();
}