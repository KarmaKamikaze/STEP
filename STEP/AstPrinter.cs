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
        Console.Write(RemoveNodeIndividualizers(node.GetType().ToString()));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  " + node);
        
        // switch (node)
        // {
        //     case AOneExpr:
        //         goto NodeValuePrint;
        //     case ATwoExpr:
        //         goto NodeValuePrint;
        //     case AThreeExpr:
        //         goto NodeValuePrint;
        //     case AMultiExpr:
        //         goto NodeValuePrint;
        //     case AOneFactor:
        //         goto NodeValuePrint;
        //     case ATwoFactor:
        //         goto NodeValuePrint;
        //     case AOneConstant:
        //         goto NodeValuePrint;
        //     case ATwoConstant:
        //         goto NodeValuePrint;
        //     case AThreeConstant:
        //         NodeValuePrint:
        //         Console.ForegroundColor = ConsoleColor.DarkGray;
        //         Console.WriteLine("  " + node);
        //         break;
        //     default:
        //         Console.WriteLine();
        //         break;
        // }
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

    private string RemoveNodeIndividualizers(string nodeName)
    {
        string result = nodeName.Replace("STEP.node.A", "");


        if (result.Contains("One"))
            result = result.Replace("One", "");
        else if (result.Contains("Two"))
            result = result.Replace("Two", "");
        else if (result.Contains("Three"))
            result = result.Replace("Three", "");
        else if (result.Contains("Four"))
            result = result.Replace("Four", "");
        else if (result.Contains("Five"))
            result = result.Replace("Five", "");
        else if (result.Contains("Six"))
            result = result.Replace("Six", "");
        else if (result.Contains("Nonelse"))
            result = result.Replace("Nonelse", "");
        else if (result.Contains("Withelse"))
            result = result.Replace("Withelse", "");

        return result;
    }
}