using STEP.AST;
using Type = STEP.AST.Type;

namespace STEP.Symbols;

public class FunctionSymTableEntry : SymTableEntry
{
    public Dictionary<string, Type> Parameters = new();
}