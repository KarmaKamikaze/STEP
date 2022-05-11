using STEP.AST.Nodes;

namespace STEP.AST;

public static class StandardEnvironment
{
    #region Constants

    public static StdSymTableEntry High => new()
    {
        Name = "On",
        ArduinoName = "HIGH",
        Type = new Type
        {
            ActualType = TypeVal.PinLevel,
            IsConstant = true
        }
    };

    public static StdSymTableEntry Low => new()
    {
        Name = "Off",
        ArduinoName = "LOW",
        Type = new Type
        {
            ActualType = TypeVal.PinLevel,
            IsConstant = true
        }
    };

    #endregion

    #region I/O

    public static StdFuncSymTableEntry DigitalRead => new()
    {
        Name = "ReadFromDigitalPin",
        ArduinoName = "digitalRead",
        Parameters = new Dictionary<string, Type>
        {
            {"pin", new PinType() {ActualType = TypeVal.Digitalpin}}
        },
        Type = new Type() {ActualType = TypeVal.PinLevel, IsConstant = true}
    };

    public static StdFuncSymTableEntry DigitalWrite => new()
    {
        Name = "WriteToDigitalPin",
        ArduinoName = "digitalWrite",
        Parameters = new()
        {
            {"pin", new PinType {ActualType = TypeVal.Digitalpin}},
            {"value", new Type {ActualType = TypeVal.PinLevel}}
        },
        Type = new Type {ActualType = TypeVal.Blank}
    };

    // analagRead(), analogWrite()
    public static StdFuncSymTableEntry AnalogRead => new()
    {
        Name = "ReadFromAnalogPin",
        ArduinoName = "analogRead",
        Parameters = new Dictionary<string, Type>
        {
            {"pin", new PinType() {ActualType = TypeVal.Analogpin}}
        },
        Type = new Type() {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry AnalogWrite => new()
    {
        Name = "WriteToAnalogPin",
        ArduinoName = "analogWrite",
        Parameters = new()
        {
            {"pin", new PinType {ActualType = TypeVal.Analogpin}},
            {"value", new Type {ActualType = TypeVal.Number}}
        },
        Type = new Type {ActualType = TypeVal.Blank}
    };

    // Serial.println(string)
    public static StdFuncSymTableEntry Print => new()
    {
        Name = "Print",
        ArduinoName = "Serial.println",
        Parameters = new()
        {
            {"message", new Type {ActualType = TypeVal.Any}}
        },
        Type = new Type {ActualType = TypeVal.Blank}
    };

    #endregion

    #region Time

    // delay(unsigned long)
    public static StdFuncSymTableEntry Delay => new()
    {
        Name = "Wait",
        ArduinoName = "delay",
        Parameters = new()
        {
            {"duration", new Type {ActualType = TypeVal.Duration}} 
        },
        Type = new Type {ActualType = TypeVal.Blank}
    };

    #endregion

    #region Math

    public static StdFuncSymTableEntry Abs => new()
    {
        Name = "AbsoluteValue",
        ArduinoName = "abs",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}}
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Constrain => new()
    {
        Name = "ConstrainValue",
        ArduinoName = "constrain",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}},
            {"upperLimit", new Type {ActualType = TypeVal.Number}},
            {"lowerLimit", new Type {ActualType = TypeVal.Number}}
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Max => new()
    {
        Name = "Max",
        ArduinoName = "max",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}},
            {"y", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Min => new()
    {
        Name = "Min",
        ArduinoName = "min",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}},
            {"y", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Power => new()
    {
        Name = "Power",
        ArduinoName = "pow",
        Parameters = new()
        {
            {"base", new Type {ActualType = TypeVal.Number}},
            {"exponent", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Squared => new()
    {
        Name = "Squared",
        ArduinoName = "sq",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry SquareRoot => new()
    {
        Name = "SquareRoot",
        ArduinoName = "sqrt",
        Parameters = new()
        {
            {"x", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    // Trigonometry
    public static StdFuncSymTableEntry Cosine => new()
    {
        Name = "Cosine",
        ArduinoName = "cos",
        Parameters = new()
        {
            {"rad", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Sine => new()
    {
        Name = "Sine",
        ArduinoName = "sin",
        Parameters = new()
        {
            {"rad", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry Tangent => new()
    {
        Name = "Tangent",
        ArduinoName = "tan",
        Parameters = new()
        {
            {"rad", new Type {ActualType = TypeVal.Number}},
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    #endregion

    #region Random numbers

    public static StdFuncSymTableEntry Random => new()
    {
        Name = "Random",
        ArduinoName = "random",
        Parameters = new()
        {
            {"min", new Type {ActualType = TypeVal.Number}},
            {"max", new Type {ActualType = TypeVal.Number}}
        },
        Type = new Type {ActualType = TypeVal.Number}
    };

    public static StdFuncSymTableEntry RandomSeed => new()
    {
        Name = "RandomSeed",
        ArduinoName = "randomSeed",
        Parameters = new()
        {
            {"seed", new Type {ActualType = TypeVal.Number}}
        },
        Type = new Type {ActualType = TypeVal.Blank}
    };

    #endregion
}