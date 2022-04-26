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


    [Fact]
    public void ForNodeInitializerIsCreatedCorrectly()
    {
        //Arrange
        const string expected = "for(double i = 0; i <= 10; i = i + 1) {\n}";
        IdNode idNode = new IdNode() { Id = "i", Type = new STEP.Type() { ActualType = TypeVal.Number}};
        NumberNode exprNode = new NumberNode() { Value = 0, Type = new STEP.Type() { ActualType = TypeVal.Number }};
        VarDclNode varInit = new VarDclNode() { Left = idNode, Right = exprNode, Type = new STEP.Type() { ActualType = TypeVal.Number}};
        
        NumberNode limitNode = new NumberNode() { Value = 10, Type = new STEP.Type() { ActualType = TypeVal.Number }};
        NumberNode updateNode = new NumberNode() { Value = 1, Type = new STEP.Type() { ActualType = TypeVal.Number }};

        ForNode forNode = new ForNode() { Initializer = varInit, Limit = limitNode, Update = updateNode};
        
        //Act
        _visitor.Visit(forNode);
        string actual = _visitor.O


    }
    
}

