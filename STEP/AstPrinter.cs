using STEP.analysis;
using STEP.node;

namespace STEP;

public class AstPrinter : DepthFirstAdapter
{
    private int indent;

    private void PrintIndent()
    {
        Console.Write("".PadLeft(indent, '\t'));
    }

    private void PrintNode(Node node)
    {
        PrintIndent();

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(node.GetType().ToString().Replace("STEP.node.", ""));

        switch (node)
        {
            case AOneConstant:
                goto ThreeConstant;
            case ATwoConstant:
                goto ThreeConstant;
            case AThreeConstant:
                ThreeConstant:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  " + node);
                break;
            default:
                Console.WriteLine();
                break;
        }
    }

    public override void DefaultIn(Node node)
    {
        PrintNode(node);
        indent++;
    }

    public override void DefaultOut(Node node)
    {
        indent--;
    }
}