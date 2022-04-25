using STEP.AST.Nodes;
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
    public void Test1()
    {
        // Arrange
        // string x = "This is a string" + 25 + 10 * 2 ->
        // String x = "This is a string" + String(25 + 10 * 2)\r\n;
        const string expected = "String x = \"This is a string\" + String(25 + 10 * 2);\r\n";
        var two = new NumberNode() { Value = 2, Type = new STEP.Type() { ActualType = TypeVal.Number } };
        var ten = new NumberNode() { Value = 10, Type = new STEP.Type() { ActualType = TypeVal.Number } };
        var multExpr = new MultNode()
        {
            Left = ten,
            Right = two,
            Type = new STEP.Type() { ActualType = TypeVal.Number }
        };
        var twentyFive = new NumberNode() { Value = 25 };
        var plusExpr = new PlusNode()
        {
            Left = twentyFive,
            Right = multExpr,
            Type = new STEP.Type() { ActualType = TypeVal.Number }
        };
        var str = new StringNode() { Value = "This is a string", Type = new STEP.Type() { ActualType = TypeVal.String } };
        var concatExpr = new PlusNode()
        {
            Left = str,
            Right = plusExpr,
            Type = new STEP.Type() { ActualType = TypeVal.String }
        };
        var id = new IdNode() { Id = "x", Type = new STEP.Type() { ActualType = TypeVal.String } };
        var varDcl = new VarDclNode()
        {
            IsConstant = false,
            Left = id,
            Right = concatExpr,
            Type = new STEP.Type() { ActualType = TypeVal.String }
        };

        // Act
        _visitor.Visit(varDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }


}

