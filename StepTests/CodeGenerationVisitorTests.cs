using STEP.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StepTests;

public class CodeGenerationVisitorTests
{
    private readonly CodeGenerationVisitor _visitor = new();


    [Fact]
    public void ForNodeInitializerIsCreatedCorrectly()
    {
        const string expected = "for()";

    }
    
}

