using STEP.analysis;
using STEP.node;

namespace STEP;

public class AstPrinter : DepthFirstAdapter
{
    private int _indent;

    private void PrintIndent()
    {
        Console.Write("".PadLeft(_indent, 't'));
    }

    private void PrintNode(Node node)
    {
        PrintIndent();

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(node.GetType().ToString().Replace("STEP.node.", ""));
        
        Console.WriteLine();
    }

    public override void DefaultIn(Node node)
    {
        PrintNode(node);
        _indent++;
    }

    public override void DefaultOut(Node node)
    {
        _indent--;
    }
}