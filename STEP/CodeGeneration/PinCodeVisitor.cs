using System.Globalization;
using System.Text;
using STEP.AST.Nodes;

namespace STEP.CodeGeneration;

public class PinCodeVisitor : CodeGenerationVisitor
{
    private readonly StringBuilder _stringBuilder = new();

    public override void Visit(NumberNode n)
    {
        _stringBuilder.Append(n.Value.ToString(CultureInfo.InvariantCulture));
    }

    public string GetPinCode()
    {
        return _stringBuilder.ToString();
    }
}