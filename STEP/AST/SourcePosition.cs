namespace STEP.AST;

public class SourcePosition
{
    public SourcePosition(int line, int index)
    {
        Line = line;
        Index = index;
    }

    public int Line { get; }
    public int Index { get; }

    public override string ToString()
    {
        return $"(Line {Line}, position {Index})";
    }
}