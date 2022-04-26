using STEP.AST.Nodes;
using STEP.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STEP.AST.Nodes;
using Xunit;

namespace StepTests;

public class CodeGenerationVisitorTests
{
    private readonly CodeGenerationVisitor _visitor = new();

    #region String concatenation

    [Fact]
    public void PlusNode_StringConcat_Turns_simple_numeric_expression_into_string()
    {
        // Arrange
        // string x = "This is a string" + 25 + 10 * 2 ->
        // => String x = "This is a string" + String(25 + 10 * 2);\r\n
        const string expected = "String x = \"This is a string\" + String(25 + 10 * 2);\r\n";
        var two = new NumberNode() {Value = 2, Type = new STEP.Type() {ActualType = TypeVal.Number}};
        var ten = new NumberNode() {Value = 10, Type = new STEP.Type() {ActualType = TypeVal.Number}};
        var multExpr = new MultNode()
        {
            Left = ten,
            Right = two,
            Type = new STEP.Type() {ActualType = TypeVal.Number}
        };
        var twentyFive = new NumberNode() {Value = 25};
        var plusExpr = new PlusNode()
        {
            Left = twentyFive,
            Right = multExpr,
            Type = new STEP.Type() {ActualType = TypeVal.Number}
        };
        var str = new StringNode() {Value = "This is a string", Type = new STEP.Type() {ActualType = TypeVal.String}};
        var concatExpr = new PlusNode()
        {
            Left = str,
            Right = plusExpr,
            Type = new STEP.Type() {ActualType = TypeVal.String}
        };
        var id = new IdNode() {Id = "x", Type = new STEP.Type() {ActualType = TypeVal.String}};
        var varDcl = new VarDclNode()
        {
            Left = id,
            Right = concatExpr,
            Type = new STEP.Type() {ActualType = TypeVal.String}
        };
        // Act
        _visitor.Visit(varDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PlusNode_StringConcat_Turns_boolean_into_string()
    {
        // Arrange
        // string x = true + " " + "funk"
        // => String x = String(true) + " " + "funk";\r\n
        const string expected = "String x = String(true) + \" \" + \"funk\";\r\n";

        var trueBool = new BoolNode {Value = true, Type = new STEP.Type {ActualType = TypeVal.Boolean}};
        var space = new StringNode {Value = " ", Type = new STEP.Type {ActualType = TypeVal.String}};
        var firstConcat = new PlusNode
        {
            Left = trueBool,
            Right = space,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var funk = new StringNode {Value = "funk", Type = new STEP.Type {ActualType = TypeVal.String}};
        var secondConcat = new PlusNode
        {
            Left = firstConcat,
            Right = funk,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var id = new IdNode {Id = "x"};
        var varDcl = new VarDclNode
        {
            Left = id,
            Right = secondConcat,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };

        // Act
        _visitor.Visit(varDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PlusNode_StringConcat_Turns_multiple_numbers_surrounded_by_strings_into_strings()
    {
        // string x = "foo" + 15 + 30 + "bar"
        // => String x = "foo" + String(15) + String(30) + "bar";\r\n
        // Arrange
        const string expected = "String x = \"foo\" + String(15) + String(30) + \"bar\";\r\n";

        var foo = new StringNode {Value = "foo", Type = new STEP.Type {ActualType = TypeVal.String}};
        var num15 = new NumberNode {Value = 15, Type = new STEP.Type {ActualType = TypeVal.Number}};
        var firstConcat = new PlusNode
        {
            Left = foo,
            Right = num15,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var num30 = new NumberNode {Value = 30, Type = new STEP.Type {ActualType = TypeVal.Number}};
        var secondConcat = new PlusNode
        {
            Left = firstConcat,
            Right = num30,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var bar = new StringNode {Value = "bar", Type = new STEP.Type {ActualType = TypeVal.String}};
        var thirdConcat = new PlusNode
        {
            Left = secondConcat,
            Right = bar,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var id = new IdNode {Id = "x"};
        var varDcl = new VarDclNode
        {
            Left = id,
            Right = thirdConcat,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };

        // Act
        _visitor.Visit(varDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PlusNode_StringConcat_Turns_non_string_variables_and_functions_into_strings()
    {
        // string x = GetDay() + ", " + month
        // where GetDay returns a number and month is a number
        // => String x = String(GetDay()) + ", " + String(month);\r\n
        // Arrange
        const string expected = "String x = String(GetDay()) + \", \" + String(month);\r\n";
        var getDayId = new IdNode {Id = "GetDay"};
        var getDay = new FuncExprNode
            {Id = getDayId, Params = new(), Type = new STEP.Type {ActualType = TypeVal.Number}};
        var comma = new StringNode {Value = ", ", Type = new STEP.Type {ActualType = TypeVal.String}};
        var firstConcat = new PlusNode
        {
            Left = getDay,
            Right = comma,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var month = new IdNode {Id = "month", Type = new STEP.Type {ActualType = TypeVal.Number}};
        var secondConcat = new PlusNode
        {
            Left = firstConcat,
            Right = month,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };
        var id = new IdNode {Id = "x"};
        var varDcl = new VarDclNode
        {
            Left = id,
            Right = secondConcat,
            Type = new STEP.Type {ActualType = TypeVal.String}
        };

        // Act
        _visitor.Visit(varDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    #endregion
}





