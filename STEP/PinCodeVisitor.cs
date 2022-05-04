using System.Globalization;
using System.Text;
using STEP.AST.Nodes;
using STEP.CodeGeneration;

namespace STEP;

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