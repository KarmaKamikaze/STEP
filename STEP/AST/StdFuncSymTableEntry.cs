namespace STEP.AST;

public class StdFuncSymTableEntry : FunctionSymTableEntry
{
    public string ArduinoName { get; set; }
    public Dictionary<string, System.Type> ArduinoParameters = new();
}
