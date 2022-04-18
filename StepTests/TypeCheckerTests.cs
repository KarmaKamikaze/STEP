using STEP;
using Xunit;

namespace StepTests; 

public class TypeCheckerTests {
    private IVisitor _typeVisitor;
    public TypeCheckerTests() {
        _typeVisitor = new TypeVisitor();
    }
}