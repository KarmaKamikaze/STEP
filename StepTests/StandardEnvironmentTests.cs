using System.Collections.Generic;
using STEP;
using STEP.AST;
using STEP.AST.Nodes;
using Xunit;

namespace StepTests;

public class StandardEnvironmentTests
{
    private readonly TypeVisitor _typeVisitor = new();

    [Fact]
    public void DigitalRead_ValidParameter_ThrowsNoException()
    {
        /*
         * boolean function x()
         *   input digitalpin p = 1
         *   if(ReadFromDigitalPin(p) == On)
         *     return true
         *   else
         *     return false
         * end function
         */

        // Arrange
        var retFalse = new RetNode
        {
            RetVal = new BoolNode {Value = false},
            SurroundingFuncType = new Type {ActualType = TypeVal.Boolean}
        };
        var retTrue = new RetNode
        {
            RetVal = new BoolNode {Value = true},
            SurroundingFuncType = new Type {ActualType = TypeVal.Boolean}
        };
        var digitalRead = new FuncExprNode
        {
            Id = new IdNode {Name = "ReadFromDigitalPin"},
            Params = new List<ExprNode> {new IdNode {Name = "p"}}
        };
        var condition = new EqNode
        {
            Left = digitalRead,
            Right = new IdNode {Name = "On"}
        };
        var ifStmt = new IfNode
        {
            Condition = condition,
            ThenClause = new List<StmtNode> {retTrue},
            ElseClause = new List<StmtNode> {retFalse}
        };
        var pinDcl = new PinDclNode
        {
            Left = new IdNode
            {
                Name = "p",
                Type = new PinType
                {
                    ActualType = TypeVal.Digitalpin,
                    IsConstant = true,
                    Mode = PinMode.INPUT
                }
            },
            Right = new NumberNode {Value = 1}
        };
        var funcDcl = new FuncDefNode
        {
            Id = new IdNode {Name = "x"},
            Stmts = new List<StmtNode> {pinDcl, ifStmt},
            FormalParams = new List<IdNode>(),
            ReturnType = new Type {ActualType = TypeVal.Boolean}
        };

        // Act, assert
        _typeVisitor.Visit(funcDcl);
    }

    [Fact]
    public void DigitalRead_InvalidParameter_ThrowsTypeException()
    {
        // ReadFromDigitalPin(1) -> type exception since 1 is not a digitalpin!

        // Arrange
        var digitalRead = new FuncExprNode
        {
            Id = new IdNode {Name = "ReadFromDigitalPin"},
            Params = new List<ExprNode> {new NumberNode {Value = 1}}
        };

        // Act
        var test = () => _typeVisitor.Visit(digitalRead);

        // Assert
        Assert.Throws<TypeMismatchException>(test);
    }

    [Fact]
    public void DigitalWrite_ValidParameters_ThrowsNoException()
    {
        /*
         * blank function x()
         *   input digitalpin p = 1
         *   WriteToDigitalPin(p, Low)
         * end function
         */

        // Arrange
        var digitalWrite = new FuncStmtNode
        {
            Id = new IdNode {Name = "WriteToDigitalPin"},
            Params = new List<ExprNode> {new IdNode {Name = "p"}, new IdNode {Name = "Off"}}
        };
        var pinDcl = new PinDclNode
        {
            Left = new IdNode
            {
                Name = "p",
                Type = new PinType
                {
                    ActualType = TypeVal.Digitalpin,
                    IsConstant = true,
                    Mode = PinMode.INPUT
                }
            },
            Right = new NumberNode {Value = 1}
        };
        var funcDcl = new FuncDefNode
        {
            Id = new IdNode {Name = "x"},
            Stmts = new List<StmtNode> {pinDcl, digitalWrite},
            FormalParams = new List<IdNode>(),
            ReturnType = new Type {ActualType = TypeVal.Blank}
        };

        // Act, assert
        _typeVisitor.Visit(funcDcl);
    }
    
    [Theory]
    [InlineData(DurationNode.DurationScale.Ms)]
    [InlineData(DurationNode.DurationScale.S)]
    [InlineData(DurationNode.DurationScale.M)]
    [InlineData(DurationNode.DurationScale.H)]
    [InlineData(DurationNode.DurationScale.D)]

    public void Wait_ValidParameters_ThrowsNoException(DurationNode.DurationScale scale)
    {
        /*
         * Wait(500ms) - ms, s, m, d and h all accepted
         * 
         */

        // Arrange
        var wait = new FuncStmtNode
        {
            Id = new IdNode {Name = "Wait"},
            Params = new List<ExprNode> {new DurationNode() {Value = 500, Scale = scale}}
        };

        // Act, assert
        _typeVisitor.Visit(wait);
    }
    
    [Fact]
    public void Wait_InvalidParameters_ThrowsTypeException()
    {
        /*
         * Wait(500) - number not accepted
         * 
         */

        // Arrange
        var wait = new FuncStmtNode
        {
            Id = new IdNode {Name = "Wait"},
            Params = new List<ExprNode> {new NumberNode() {Value = 500}}
        };

        // Act
        var test = () => _typeVisitor.Visit(wait);
        
        // Assert
        Assert.Throws<TypeMismatchException>(test);
    }
}