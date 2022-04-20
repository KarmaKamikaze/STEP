// Generated from C:/Users/Mikkel/Documents/Codebase/STEP/STEP/ANTLR gens\STEP.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link STEPParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface STEPVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link STEPParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(STEPParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#setuploop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSetuploop(STEPParser.SetuploopContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#setup}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSetup(STEPParser.SetupContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#loop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoop(STEPParser.LoopContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#variables}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVariables(STEPParser.VariablesContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#var_or_nl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVar_or_nl(STEPParser.Var_or_nlContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#functions}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctions(STEPParser.FunctionsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#funcdcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFuncdcl(STEPParser.FuncdclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#funcdcl_or_nl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFuncdcl_or_nl(STEPParser.Funcdcl_or_nlContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#brackets}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBrackets(STEPParser.BracketsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#params}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParams(STEPParser.ParamsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#params_content}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParams_content(STEPParser.Params_contentContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#params_multi}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParams_multi(STEPParser.Params_multiContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitType(STEPParser.TypeContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#stmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStmt(STEPParser.StmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#stmts}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStmts(STEPParser.StmtsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#loop_stmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoop_stmt(STEPParser.Loop_stmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#loop_stmts}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoop_stmts(STEPParser.Loop_stmtsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#loopifbody}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoopifbody(STEPParser.LoopifbodyContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#ifstmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIfstmt(STEPParser.IfstmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#loopifstmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoopifstmt(STEPParser.LoopifstmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#whilestmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWhilestmt(STEPParser.WhilestmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#forstmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForstmt(STEPParser.ForstmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#for_iter_opt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFor_iter_opt(STEPParser.For_iter_optContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#assstmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssstmt(STEPParser.AssstmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#funccall}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunccall(STEPParser.FunccallContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#params_options}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParams_options(STEPParser.Params_optionsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#multi_expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMulti_expr(STEPParser.Multi_exprContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#retstmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRetstmt(STEPParser.RetstmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#arrindex}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrindex(STEPParser.ArrindexContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpr(STEPParser.ExprContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#term}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTerm(STEPParser.TermContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#factor}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFactor(STEPParser.FactorContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#value}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitValue(STEPParser.ValueContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#constant}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConstant(STEPParser.ConstantContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#logicexpr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogicexpr(STEPParser.LogicexprContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#logicequal}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogicequal(STEPParser.LogicequalContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#logiccomp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogiccomp(STEPParser.LogiccompContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#logiccompop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogiccompop(STEPParser.LogiccompopContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#logicvalue}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogicvalue(STEPParser.LogicvalueContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#vardcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVardcl(STEPParser.VardclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#var_options}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVar_options(STEPParser.Var_optionsContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#numdcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNumdcl(STEPParser.NumdclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#stringdcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStringdcl(STEPParser.StringdclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#booldcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBooldcl(STEPParser.BooldclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#arrdcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrdcl(STEPParser.ArrdclContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#arr_id_or_lit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArr_id_or_lit(STEPParser.Arr_id_or_litContext ctx);
	/**
	 * Visit a parse tree produced by {@link STEPParser#arrsizedcl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrsizedcl(STEPParser.ArrsizedclContext ctx);
}