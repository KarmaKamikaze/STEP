// Generated from C:/Users/Mikkel/Documents/Codebase/STEP/STEP/ANTLR gens\STEP.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link STEPParser}.
 */
public interface STEPListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link STEPParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(STEPParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(STEPParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#setuploop}.
	 * @param ctx the parse tree
	 */
	void enterSetuploop(STEPParser.SetuploopContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#setuploop}.
	 * @param ctx the parse tree
	 */
	void exitSetuploop(STEPParser.SetuploopContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#setup}.
	 * @param ctx the parse tree
	 */
	void enterSetup(STEPParser.SetupContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#setup}.
	 * @param ctx the parse tree
	 */
	void exitSetup(STEPParser.SetupContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#loop}.
	 * @param ctx the parse tree
	 */
	void enterLoop(STEPParser.LoopContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#loop}.
	 * @param ctx the parse tree
	 */
	void exitLoop(STEPParser.LoopContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#variables}.
	 * @param ctx the parse tree
	 */
	void enterVariables(STEPParser.VariablesContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#variables}.
	 * @param ctx the parse tree
	 */
	void exitVariables(STEPParser.VariablesContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#var_or_nl}.
	 * @param ctx the parse tree
	 */
	void enterVar_or_nl(STEPParser.Var_or_nlContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#var_or_nl}.
	 * @param ctx the parse tree
	 */
	void exitVar_or_nl(STEPParser.Var_or_nlContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#functions}.
	 * @param ctx the parse tree
	 */
	void enterFunctions(STEPParser.FunctionsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#functions}.
	 * @param ctx the parse tree
	 */
	void exitFunctions(STEPParser.FunctionsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#funcdcl}.
	 * @param ctx the parse tree
	 */
	void enterFuncdcl(STEPParser.FuncdclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#funcdcl}.
	 * @param ctx the parse tree
	 */
	void exitFuncdcl(STEPParser.FuncdclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#funcdcl_or_nl}.
	 * @param ctx the parse tree
	 */
	void enterFuncdcl_or_nl(STEPParser.Funcdcl_or_nlContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#funcdcl_or_nl}.
	 * @param ctx the parse tree
	 */
	void exitFuncdcl_or_nl(STEPParser.Funcdcl_or_nlContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#brackets}.
	 * @param ctx the parse tree
	 */
	void enterBrackets(STEPParser.BracketsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#brackets}.
	 * @param ctx the parse tree
	 */
	void exitBrackets(STEPParser.BracketsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#params}.
	 * @param ctx the parse tree
	 */
	void enterParams(STEPParser.ParamsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#params}.
	 * @param ctx the parse tree
	 */
	void exitParams(STEPParser.ParamsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#params_content}.
	 * @param ctx the parse tree
	 */
	void enterParams_content(STEPParser.Params_contentContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#params_content}.
	 * @param ctx the parse tree
	 */
	void exitParams_content(STEPParser.Params_contentContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#params_multi}.
	 * @param ctx the parse tree
	 */
	void enterParams_multi(STEPParser.Params_multiContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#params_multi}.
	 * @param ctx the parse tree
	 */
	void exitParams_multi(STEPParser.Params_multiContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(STEPParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(STEPParser.TypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterStmt(STEPParser.StmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitStmt(STEPParser.StmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#stmts}.
	 * @param ctx the parse tree
	 */
	void enterStmts(STEPParser.StmtsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#stmts}.
	 * @param ctx the parse tree
	 */
	void exitStmts(STEPParser.StmtsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#loop_stmt}.
	 * @param ctx the parse tree
	 */
	void enterLoop_stmt(STEPParser.Loop_stmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#loop_stmt}.
	 * @param ctx the parse tree
	 */
	void exitLoop_stmt(STEPParser.Loop_stmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#loop_stmts}.
	 * @param ctx the parse tree
	 */
	void enterLoop_stmts(STEPParser.Loop_stmtsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#loop_stmts}.
	 * @param ctx the parse tree
	 */
	void exitLoop_stmts(STEPParser.Loop_stmtsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#loopifbody}.
	 * @param ctx the parse tree
	 */
	void enterLoopifbody(STEPParser.LoopifbodyContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#loopifbody}.
	 * @param ctx the parse tree
	 */
	void exitLoopifbody(STEPParser.LoopifbodyContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#ifstmt}.
	 * @param ctx the parse tree
	 */
	void enterIfstmt(STEPParser.IfstmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#ifstmt}.
	 * @param ctx the parse tree
	 */
	void exitIfstmt(STEPParser.IfstmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#loopifstmt}.
	 * @param ctx the parse tree
	 */
	void enterLoopifstmt(STEPParser.LoopifstmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#loopifstmt}.
	 * @param ctx the parse tree
	 */
	void exitLoopifstmt(STEPParser.LoopifstmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#whilestmt}.
	 * @param ctx the parse tree
	 */
	void enterWhilestmt(STEPParser.WhilestmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#whilestmt}.
	 * @param ctx the parse tree
	 */
	void exitWhilestmt(STEPParser.WhilestmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#forstmt}.
	 * @param ctx the parse tree
	 */
	void enterForstmt(STEPParser.ForstmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#forstmt}.
	 * @param ctx the parse tree
	 */
	void exitForstmt(STEPParser.ForstmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#for_iter_opt}.
	 * @param ctx the parse tree
	 */
	void enterFor_iter_opt(STEPParser.For_iter_optContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#for_iter_opt}.
	 * @param ctx the parse tree
	 */
	void exitFor_iter_opt(STEPParser.For_iter_optContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#assstmt}.
	 * @param ctx the parse tree
	 */
	void enterAssstmt(STEPParser.AssstmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#assstmt}.
	 * @param ctx the parse tree
	 */
	void exitAssstmt(STEPParser.AssstmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#funccall}.
	 * @param ctx the parse tree
	 */
	void enterFunccall(STEPParser.FunccallContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#funccall}.
	 * @param ctx the parse tree
	 */
	void exitFunccall(STEPParser.FunccallContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#params_options}.
	 * @param ctx the parse tree
	 */
	void enterParams_options(STEPParser.Params_optionsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#params_options}.
	 * @param ctx the parse tree
	 */
	void exitParams_options(STEPParser.Params_optionsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#multi_expr}.
	 * @param ctx the parse tree
	 */
	void enterMulti_expr(STEPParser.Multi_exprContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#multi_expr}.
	 * @param ctx the parse tree
	 */
	void exitMulti_expr(STEPParser.Multi_exprContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#retstmt}.
	 * @param ctx the parse tree
	 */
	void enterRetstmt(STEPParser.RetstmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#retstmt}.
	 * @param ctx the parse tree
	 */
	void exitRetstmt(STEPParser.RetstmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#arrindex}.
	 * @param ctx the parse tree
	 */
	void enterArrindex(STEPParser.ArrindexContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#arrindex}.
	 * @param ctx the parse tree
	 */
	void exitArrindex(STEPParser.ArrindexContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterExpr(STEPParser.ExprContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitExpr(STEPParser.ExprContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#term}.
	 * @param ctx the parse tree
	 */
	void enterTerm(STEPParser.TermContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#term}.
	 * @param ctx the parse tree
	 */
	void exitTerm(STEPParser.TermContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#factor}.
	 * @param ctx the parse tree
	 */
	void enterFactor(STEPParser.FactorContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#factor}.
	 * @param ctx the parse tree
	 */
	void exitFactor(STEPParser.FactorContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#value}.
	 * @param ctx the parse tree
	 */
	void enterValue(STEPParser.ValueContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#value}.
	 * @param ctx the parse tree
	 */
	void exitValue(STEPParser.ValueContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#constant}.
	 * @param ctx the parse tree
	 */
	void enterConstant(STEPParser.ConstantContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#constant}.
	 * @param ctx the parse tree
	 */
	void exitConstant(STEPParser.ConstantContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#logicexpr}.
	 * @param ctx the parse tree
	 */
	void enterLogicexpr(STEPParser.LogicexprContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#logicexpr}.
	 * @param ctx the parse tree
	 */
	void exitLogicexpr(STEPParser.LogicexprContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#logicequal}.
	 * @param ctx the parse tree
	 */
	void enterLogicequal(STEPParser.LogicequalContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#logicequal}.
	 * @param ctx the parse tree
	 */
	void exitLogicequal(STEPParser.LogicequalContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#logiccomp}.
	 * @param ctx the parse tree
	 */
	void enterLogiccomp(STEPParser.LogiccompContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#logiccomp}.
	 * @param ctx the parse tree
	 */
	void exitLogiccomp(STEPParser.LogiccompContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#logiccompop}.
	 * @param ctx the parse tree
	 */
	void enterLogiccompop(STEPParser.LogiccompopContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#logiccompop}.
	 * @param ctx the parse tree
	 */
	void exitLogiccompop(STEPParser.LogiccompopContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#logicvalue}.
	 * @param ctx the parse tree
	 */
	void enterLogicvalue(STEPParser.LogicvalueContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#logicvalue}.
	 * @param ctx the parse tree
	 */
	void exitLogicvalue(STEPParser.LogicvalueContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#vardcl}.
	 * @param ctx the parse tree
	 */
	void enterVardcl(STEPParser.VardclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#vardcl}.
	 * @param ctx the parse tree
	 */
	void exitVardcl(STEPParser.VardclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#var_options}.
	 * @param ctx the parse tree
	 */
	void enterVar_options(STEPParser.Var_optionsContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#var_options}.
	 * @param ctx the parse tree
	 */
	void exitVar_options(STEPParser.Var_optionsContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#numdcl}.
	 * @param ctx the parse tree
	 */
	void enterNumdcl(STEPParser.NumdclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#numdcl}.
	 * @param ctx the parse tree
	 */
	void exitNumdcl(STEPParser.NumdclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#stringdcl}.
	 * @param ctx the parse tree
	 */
	void enterStringdcl(STEPParser.StringdclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#stringdcl}.
	 * @param ctx the parse tree
	 */
	void exitStringdcl(STEPParser.StringdclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#booldcl}.
	 * @param ctx the parse tree
	 */
	void enterBooldcl(STEPParser.BooldclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#booldcl}.
	 * @param ctx the parse tree
	 */
	void exitBooldcl(STEPParser.BooldclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#arrdcl}.
	 * @param ctx the parse tree
	 */
	void enterArrdcl(STEPParser.ArrdclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#arrdcl}.
	 * @param ctx the parse tree
	 */
	void exitArrdcl(STEPParser.ArrdclContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#arr_id_or_lit}.
	 * @param ctx the parse tree
	 */
	void enterArr_id_or_lit(STEPParser.Arr_id_or_litContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#arr_id_or_lit}.
	 * @param ctx the parse tree
	 */
	void exitArr_id_or_lit(STEPParser.Arr_id_or_litContext ctx);
	/**
	 * Enter a parse tree produced by {@link STEPParser#arrsizedcl}.
	 * @param ctx the parse tree
	 */
	void enterArrsizedcl(STEPParser.ArrsizedclContext ctx);
	/**
	 * Exit a parse tree produced by {@link STEPParser#arrsizedcl}.
	 * @param ctx the parse tree
	 */
	void exitArrsizedcl(STEPParser.ArrsizedclContext ctx);
}