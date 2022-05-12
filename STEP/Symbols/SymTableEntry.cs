using STEP.AST;
using STEP.AST.Nodes;
using Type = STEP.AST.Type;

namespace STEP.Symbols;

public class SymTableEntry
{
    public string Name { get; set; }
    public Type Type { get; set; }
}