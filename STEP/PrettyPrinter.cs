using Antlr4.Runtime;

namespace STEP;

public class PrettyPrinter : STEPBaseListener
{
    public override void EnterSetup(STEPParser.SetupContext context)
    {
        IndentScopes("setup", context);
    }

    public override void EnterLoop(STEPParser.LoopContext context)
    {
        IndentScopes("loop", context);
    }

    public override void EnterVariables(STEPParser.VariablesContext context)
    {
        IndentScopes("variables", context);
    }

    public override void EnterFunctions(STEPParser.FunctionsContext context)
    {
        IndentScopes("functions", context);
    }

    private void IndentScopes(string scopeName, ParserRuleContext context)
    {
        string[] tokenText = context.GetText().Split("\n");
        foreach (string token in tokenText)
        {
            if (token != scopeName && token != $"end {scopeName}")
                Console.Write("    ");

            Console.WriteLine(token);
        }
    }
}