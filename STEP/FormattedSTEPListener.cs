namespace STEP;

public class FormattedSTEPListener : STEPBaseListener
{
    public override void EnterProgram(STEPParser.ProgramContext context)
    {
        Console.WriteLine(context.GetText());
    }
}