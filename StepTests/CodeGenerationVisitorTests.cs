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

    #region FuncDefNode
    [Fact]
    public void FuncDefNode_WithMultipleParameters_OutputsCorrectCode()
    {
        /* number function x(number a, number b)
         * end function
         * 
         * double x(double a, double b) {
         * }
         */
        
        // Arrange
        const string expected = "double x(double a, double b) {\r\n}\r\n";
        var param1 = new IdNode {Id = "a", Type = new STEP.Type {ActualType = TypeVal.Number}};
        var param2 = new IdNode {Id = "b", Type = new STEP.Type {ActualType = TypeVal.Number}};
        var funcId = new IdNode {Id = "x", Type = new STEP.Type {ActualType = TypeVal.Number}};
        var funcDcl = new FuncDefNode
        {
            Name = funcId, 
            Stmts = new List<StmtNode>(), 
            FormalParams = new List<IdNode> {param1, param2},
            ReturnType = new STEP.Type {ActualType = TypeVal.Number}
        };

        // Act
        _visitor.Visit(funcDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void FuncDefNode_NoParameters_OutputsCorrectCode()
    {
        /* number function x()
         * end function
         * 
         * double x() {
         * }
         */
        
        // Arrange
        const string expected = "double x() {\r\n}\r\n";
        var funcId = new IdNode {Id = "x", Type = new STEP.Type {ActualType = TypeVal.Number}};
        var funcDcl = new FuncDefNode
        {
            Name = funcId, 
            Stmts = new List<StmtNode>(), 
            FormalParams = new List<IdNode>(),
            ReturnType = new STEP.Type {ActualType = TypeVal.Number}
        };

        // Act
        _visitor.Visit(funcDcl);
        string actual = _visitor.OutputToString();

        // Assert
        Assert.Equal(expected, actual);
    }
    #endregion
}

