using STEP.AST.Nodes;

namespace STEP.AST;

public static class StandardEnvironment
{
    // Constants
    public static SymTableEntry High => new SymTableEntry
    {
        Name = "HIGH",
        Type = new Type
        {
            ActualType = TypeVal.Number,
            IsConstant = true
        }
    };
    public static SymTableEntry Low => new SymTableEntry
    {
        Name = "LOW",
        Type = new Type
        {
            ActualType = TypeVal.Number,
            IsConstant = true
        }
    };
    
    // Digital I/O
    // digitalRead(), digitalWrite(), pinMode()
    public static FunctionSymTableEntry DigitalRead => new FunctionSymTableEntry()
    {
        Name = "digitalRead",
        Parameters = new Dictionary<string, Type>
        {
            {"pin", new PinType() {ActualType = TypeVal.Digitalpin}}
        },
        Type = new Type() { ActualType = TypeVal.Number, IsConstant = true}
    };

    // Analog I/O
    // analagRead(), analogReference(), analogWrite()

    // I/O (Serial)
    // print(), 

    // Time
    // delay(), delayMicroseconds(), micros(), millis()

    // Math
    // abs(), constrain(), map(), max(), min(), pow(), sq(), sqrt()

    // Trigonometry
    // cos(), sin(), tan()

    // Random numbers
    // random(), randomSeed()

    // 
}