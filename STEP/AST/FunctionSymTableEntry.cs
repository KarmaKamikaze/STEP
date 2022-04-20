using STEP.AST.Nodes;

namespace STEP.AST;

public class FunctionSymTableEntry : SymTableEntry
{
    public Dictionary<string, TypeVal> Parameters = new();
}