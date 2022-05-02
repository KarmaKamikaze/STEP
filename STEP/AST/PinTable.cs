using STEP.AST.Nodes;

namespace STEP.AST;

public class PinTable : IPinTable
{
    private readonly HashSet<int> _analogTable = new();
    private readonly HashSet<int> _digitalTable = new();

    public void RegisterPin(PinDclNode pinDcl)
    {
        TypeVal type = pinDcl.Left.Type.ActualType;
        int pinVal = Convert.ToInt32((pinDcl.Right as NumberNode).Value);
        switch (type)
        {
            case TypeVal.Analogpin:
                if (!_analogTable.Add(pinVal))
                    throw new DuplicatePinDeclarationException($"Analog pin has already been declared", pinDcl.SourcePosition, pinDcl.Left.Name, pinVal);
                break;
            case TypeVal.Digitalpin:
                if (!_digitalTable.Add(pinVal))
                    throw new DuplicatePinDeclarationException($"Digital pin has already been declared", pinDcl.SourcePosition, pinDcl.Left.Name, pinVal);
                break;
            default:
                throw new PinTableUnexpectedTypeException($"Unexpected data type in Pin Table", type, new TypeVal[] { TypeVal.Digitalpin, TypeVal.Analogpin });
        }
    }
}