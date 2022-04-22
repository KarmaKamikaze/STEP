using STEP.AST.Nodes;

namespace STEP.AST; 

public class PinTable : IPinTable {
    private readonly HashSet<int> _analogTable = new();
    private readonly HashSet<int> _digitalTable = new();

    public void RegisterPin(TypeVal type, int pinVal) {
        switch (type) {
            case TypeVal.Analogpin:
                if (!_analogTable.Add(pinVal)) 
                    throw new DuplicateDeclarationException($"Analog pin {pinVal} already declared");
                break;
            case TypeVal.Digitalpin:
                if (!_digitalTable.Add(pinVal)) 
                    throw new DuplicateDeclarationException($"Digital pin {pinVal} already declared");
                break;
            default:
                throw new UnexpectedTypeException($"Did not expect {type} in pin table");
        }
    }
}