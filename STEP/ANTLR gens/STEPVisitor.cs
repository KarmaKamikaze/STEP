//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Mikkel/Documents/Codebase/STEP/STEP/ANTLR gens\STEP.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="STEPParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface ISTEPVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] STEPParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.setuploop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSetuploop([NotNull] STEPParser.SetuploopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.setup"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSetup([NotNull] STEPParser.SetupContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoop([NotNull] STEPParser.LoopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.variables"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariables([NotNull] STEPParser.VariablesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.var_or_nl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar_or_nl([NotNull] STEPParser.Var_or_nlContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.functions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctions([NotNull] STEPParser.FunctionsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.funcdcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncdcl([NotNull] STEPParser.FuncdclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.funcdcl_or_nl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncdcl_or_nl([NotNull] STEPParser.Funcdcl_or_nlContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.brackets"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBrackets([NotNull] STEPParser.BracketsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams([NotNull] STEPParser.ParamsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.params_content"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams_content([NotNull] STEPParser.Params_contentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.params_multi"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams_multi([NotNull] STEPParser.Params_multiContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] STEPParser.TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.paramstype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParamstype([NotNull] STEPParser.ParamstypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] STEPParser.StmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.stmts"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmts([NotNull] STEPParser.StmtsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loop_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoop_stmt([NotNull] STEPParser.Loop_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loop_stmts"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoop_stmts([NotNull] STEPParser.Loop_stmtsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loopifbody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopifbody([NotNull] STEPParser.LoopifbodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.ifstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfstmt([NotNull] STEPParser.IfstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.elseifstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseifstmt([NotNull] STEPParser.ElseifstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.elsestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElsestmt([NotNull] STEPParser.ElsestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loopifstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopifstmt([NotNull] STEPParser.LoopifstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loopelseifstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopelseifstmt([NotNull] STEPParser.LoopelseifstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.loopelsestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopelsestmt([NotNull] STEPParser.LoopelsestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.whilestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhilestmt([NotNull] STEPParser.WhilestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.forstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForstmt([NotNull] STEPParser.ForstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.for_iter_opt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFor_iter_opt([NotNull] STEPParser.For_iter_optContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.assstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssstmt([NotNull] STEPParser.AssstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.funccall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunccall([NotNull] STEPParser.FunccallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.params_options"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams_options([NotNull] STEPParser.Params_optionsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.multi_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMulti_expr([NotNull] STEPParser.Multi_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.retstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRetstmt([NotNull] STEPParser.RetstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.arrindex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrindex([NotNull] STEPParser.ArrindexContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] STEPParser.ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] STEPParser.TermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactor([NotNull] STEPParser.FactorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] STEPParser.ValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] STEPParser.ConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.logicexpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicexpr([NotNull] STEPParser.LogicexprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.logicequal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicequal([NotNull] STEPParser.LogicequalContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.logiccomp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogiccomp([NotNull] STEPParser.LogiccompContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.logiccompop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogiccompop([NotNull] STEPParser.LogiccompopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.logicvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicvalue([NotNull] STEPParser.LogicvalueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.vardcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVardcl([NotNull] STEPParser.VardclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.var_options"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar_options([NotNull] STEPParser.Var_optionsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.numdcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumdcl([NotNull] STEPParser.NumdclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.stringdcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringdcl([NotNull] STEPParser.StringdclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.booldcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooldcl([NotNull] STEPParser.BooldclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.pindcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPindcl([NotNull] STEPParser.PindclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.pinmode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPinmode([NotNull] STEPParser.PinmodeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.pintype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPintype([NotNull] STEPParser.PintypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.arrdcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrdcl([NotNull] STEPParser.ArrdclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.arr_id_or_lit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArr_id_or_lit([NotNull] STEPParser.Arr_id_or_litContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="STEPParser.arrsizedcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrsizedcl([NotNull] STEPParser.ArrsizedclContext context);
}
