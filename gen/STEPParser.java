// Generated from C:/Users/Mikkel/Documents/Codebase/STEP/STEP/ANTLR gens\STEP.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class STEPParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		WHITESPACE=1, END_OF_LINE_COMMENT=2, MULTILINE_COMMENT=3, LPAREN=4, RPAREN=5, 
		LBRACK=6, RBRACK=7, ASSIGN=8, PLUS=9, MINUS=10, DIVIDE=11, MULT=12, POW=13, 
		GRTHAN=14, GRTHANEQ=15, LTHAN=16, LTHANEQ=17, EQ=18, NEQ=19, NEG=20, NL=21, 
		COMMA=22, NUMLITERAL=23, STRLITERAL=24, BOOLLITERAL=25, SETUP=26, ENDSETUP=27, 
		LOOP=28, ENDLOOP=29, FUNCTIONS=30, ENDFUNCTIONS=31, FUNCTION=32, ENDFUNCTION=33, 
		VARIABLES=34, ENDVARIABLES=35, BLANK=36, NUMBER=37, STRING=38, BOOLEAN=39, 
		IF=40, ENDIF=41, ELSE=42, CONTINUE=43, BREAK=44, REPEATWHILE=45, ENDWHILE=46, 
		REPEATFOR=47, ENDFOR=48, TO=49, CHANGEBY=50, SWITCH=51, ENDSWITCH=52, 
		WHEN=53, DO=54, FALLTHROUGH=55, OTHERWISEDO=56, RETURN=57, AND=58, OR=59, 
		CONSTANT=60, ID=61;
	public static final int
		RULE_program = 0, RULE_setuploop = 1, RULE_setup = 2, RULE_loop = 3, RULE_variables = 4, 
		RULE_var_or_nl = 5, RULE_functions = 6, RULE_funcdcl = 7, RULE_funcdcl_or_nl = 8, 
		RULE_brackets = 9, RULE_params = 10, RULE_params_content = 11, RULE_params_multi = 12, 
		RULE_type = 13, RULE_stmt = 14, RULE_stmts = 15, RULE_loop_stmt = 16, 
		RULE_loop_stmts = 17, RULE_loopifbody = 18, RULE_ifstmt = 19, RULE_loopifstmt = 20, 
		RULE_whilestmt = 21, RULE_forstmt = 22, RULE_for_iter_opt = 23, RULE_assstmt = 24, 
		RULE_funccall = 25, RULE_params_options = 26, RULE_multi_expr = 27, RULE_retstmt = 28, 
		RULE_arrindex = 29, RULE_expr = 30, RULE_term = 31, RULE_factor = 32, 
		RULE_value = 33, RULE_constant = 34, RULE_logicexpr = 35, RULE_logicequal = 36, 
		RULE_logiccomp = 37, RULE_logiccompop = 38, RULE_logicvalue = 39, RULE_vardcl = 40, 
		RULE_var_options = 41, RULE_numdcl = 42, RULE_stringdcl = 43, RULE_booldcl = 44, 
		RULE_arrdcl = 45, RULE_arr_id_or_lit = 46, RULE_arrsizedcl = 47;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "setuploop", "setup", "loop", "variables", "var_or_nl", "functions", 
			"funcdcl", "funcdcl_or_nl", "brackets", "params", "params_content", "params_multi", 
			"type", "stmt", "stmts", "loop_stmt", "loop_stmts", "loopifbody", "ifstmt", 
			"loopifstmt", "whilestmt", "forstmt", "for_iter_opt", "assstmt", "funccall", 
			"params_options", "multi_expr", "retstmt", "arrindex", "expr", "term", 
			"factor", "value", "constant", "logicexpr", "logicequal", "logiccomp", 
			"logiccompop", "logicvalue", "vardcl", "var_options", "numdcl", "stringdcl", 
			"booldcl", "arrdcl", "arr_id_or_lit", "arrsizedcl"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, null, null, "'('", "')'", "'['", "']'", "'='", "'+'", "'-'", 
			"'/'", "'*'", "'^'", "'>'", "'>='", "'<'", "'<='", "'=='", "'!:'", "'!'", 
			null, "','", null, null, null, "'setup'", "'end setup'", "'loop'", "'end loop'", 
			"'functions'", "'end functions'", "'function'", "'end function'", "'variables'", 
			"'end variables'", "'blank'", "'number'", "'string'", "'boolean'", "'if'", 
			"'end if'", "'else'", "'continue'", "'break'", "'repeat while'", "'end while'", 
			"'repeat for'", "'end for'", "'to'", "'change by'", "'switch'", "'end switch'", 
			"'when'", "'do'", "'fallthrough'", "'otherwise do'", "'return'", "'and'", 
			"'or'", "'constant'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "WHITESPACE", "END_OF_LINE_COMMENT", "MULTILINE_COMMENT", "LPAREN", 
			"RPAREN", "LBRACK", "RBRACK", "ASSIGN", "PLUS", "MINUS", "DIVIDE", "MULT", 
			"POW", "GRTHAN", "GRTHANEQ", "LTHAN", "LTHANEQ", "EQ", "NEQ", "NEG", 
			"NL", "COMMA", "NUMLITERAL", "STRLITERAL", "BOOLLITERAL", "SETUP", "ENDSETUP", 
			"LOOP", "ENDLOOP", "FUNCTIONS", "ENDFUNCTIONS", "FUNCTION", "ENDFUNCTION", 
			"VARIABLES", "ENDVARIABLES", "BLANK", "NUMBER", "STRING", "BOOLEAN", 
			"IF", "ENDIF", "ELSE", "CONTINUE", "BREAK", "REPEATWHILE", "ENDWHILE", 
			"REPEATFOR", "ENDFOR", "TO", "CHANGEBY", "SWITCH", "ENDSWITCH", "WHEN", 
			"DO", "FALLTHROUGH", "OTHERWISEDO", "RETURN", "AND", "OR", "CONSTANT", 
			"ID"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "STEP.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public STEPParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public SetuploopContext setuploop() {
			return getRuleContext(SetuploopContext.class,0);
		}
		public List<TerminalNode> NL() { return getTokens(STEPParser.NL); }
		public TerminalNode NL(int i) {
			return getToken(STEPParser.NL, i);
		}
		public VariablesContext variables() {
			return getRuleContext(VariablesContext.class,0);
		}
		public FunctionsContext functions() {
			return getRuleContext(FunctionsContext.class,0);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterProgram(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitProgram(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitProgram(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(99);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==NL) {
				{
				{
				setState(96);
				match(NL);
				}
				}
				setState(101);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(103);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==VARIABLES) {
				{
				setState(102);
				variables();
				}
			}

			setState(105);
			setuploop();
			setState(107);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==FUNCTIONS) {
				{
				setState(106);
				functions();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SetuploopContext extends ParserRuleContext {
		public SetupContext setup() {
			return getRuleContext(SetupContext.class,0);
		}
		public List<TerminalNode> NL() { return getTokens(STEPParser.NL); }
		public TerminalNode NL(int i) {
			return getToken(STEPParser.NL, i);
		}
		public LoopContext loop() {
			return getRuleContext(LoopContext.class,0);
		}
		public SetuploopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_setuploop; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterSetuploop(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitSetuploop(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitSetuploop(this);
			else return visitor.visitChildren(this);
		}
	}

	public final SetuploopContext setuploop() throws RecognitionException {
		SetuploopContext _localctx = new SetuploopContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_setuploop);
		int _la;
		try {
			setState(137);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(109);
				setup();
				setState(113);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NL) {
					{
					{
					setState(110);
					match(NL);
					}
					}
					setState(115);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(116);
				loop();
				setState(120);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NL) {
					{
					{
					setState(117);
					match(NL);
					}
					}
					setState(122);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(123);
				setup();
				setState(127);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NL) {
					{
					{
					setState(124);
					match(NL);
					}
					}
					setState(129);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(130);
				loop();
				setState(134);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NL) {
					{
					{
					setState(131);
					match(NL);
					}
					}
					setState(136);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SetupContext extends ParserRuleContext {
		public TerminalNode SETUP() { return getToken(STEPParser.SETUP, 0); }
		public TerminalNode ENDSETUP() { return getToken(STEPParser.ENDSETUP, 0); }
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public SetupContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_setup; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterSetup(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitSetup(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitSetup(this);
			else return visitor.visitChildren(this);
		}
	}

	public final SetupContext setup() throws RecognitionException {
		SetupContext _localctx = new SetupContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_setup);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(139);
			match(SETUP);
			setState(143);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				{
				setState(140);
				stmt();
				}
				}
				setState(145);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(146);
			match(ENDSETUP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LoopContext extends ParserRuleContext {
		public TerminalNode LOOP() { return getToken(STEPParser.LOOP, 0); }
		public TerminalNode ENDLOOP() { return getToken(STEPParser.ENDLOOP, 0); }
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public LoopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_loop; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLoop(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLoop(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLoop(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LoopContext loop() throws RecognitionException {
		LoopContext _localctx = new LoopContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_loop);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(148);
			match(LOOP);
			setState(152);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				{
				setState(149);
				stmt();
				}
				}
				setState(154);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(155);
			match(ENDLOOP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VariablesContext extends ParserRuleContext {
		public TerminalNode VARIABLES() { return getToken(STEPParser.VARIABLES, 0); }
		public TerminalNode ENDVARIABLES() { return getToken(STEPParser.ENDVARIABLES, 0); }
		public List<Var_or_nlContext> var_or_nl() {
			return getRuleContexts(Var_or_nlContext.class);
		}
		public Var_or_nlContext var_or_nl(int i) {
			return getRuleContext(Var_or_nlContext.class,i);
		}
		public List<TerminalNode> NL() { return getTokens(STEPParser.NL); }
		public TerminalNode NL(int i) {
			return getToken(STEPParser.NL, i);
		}
		public VariablesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variables; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterVariables(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitVariables(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitVariables(this);
			else return visitor.visitChildren(this);
		}
	}

	public final VariablesContext variables() throws RecognitionException {
		VariablesContext _localctx = new VariablesContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_variables);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(157);
			match(VARIABLES);
			setState(161);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << CONSTANT))) != 0)) {
				{
				{
				setState(158);
				var_or_nl();
				}
				}
				setState(163);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(164);
			match(ENDVARIABLES);
			setState(168);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==NL) {
				{
				{
				setState(165);
				match(NL);
				}
				}
				setState(170);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Var_or_nlContext extends ParserRuleContext {
		public VardclContext vardcl() {
			return getRuleContext(VardclContext.class,0);
		}
		public TerminalNode NL() { return getToken(STEPParser.NL, 0); }
		public Var_or_nlContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_var_or_nl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterVar_or_nl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitVar_or_nl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitVar_or_nl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Var_or_nlContext var_or_nl() throws RecognitionException {
		Var_or_nlContext _localctx = new Var_or_nlContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_var_or_nl);
		try {
			setState(173);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NUMBER:
			case STRING:
			case BOOLEAN:
			case CONSTANT:
				enterOuterAlt(_localctx, 1);
				{
				setState(171);
				vardcl();
				}
				break;
			case NL:
				enterOuterAlt(_localctx, 2);
				{
				setState(172);
				match(NL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionsContext extends ParserRuleContext {
		public TerminalNode FUNCTIONS() { return getToken(STEPParser.FUNCTIONS, 0); }
		public TerminalNode ENDFUNCTIONS() { return getToken(STEPParser.ENDFUNCTIONS, 0); }
		public List<Funcdcl_or_nlContext> funcdcl_or_nl() {
			return getRuleContexts(Funcdcl_or_nlContext.class);
		}
		public Funcdcl_or_nlContext funcdcl_or_nl(int i) {
			return getRuleContext(Funcdcl_or_nlContext.class,i);
		}
		public List<TerminalNode> NL() { return getTokens(STEPParser.NL); }
		public TerminalNode NL(int i) {
			return getToken(STEPParser.NL, i);
		}
		public FunctionsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functions; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFunctions(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFunctions(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFunctions(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FunctionsContext functions() throws RecognitionException {
		FunctionsContext _localctx = new FunctionsContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_functions);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(175);
			match(FUNCTIONS);
			setState(179);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << BLANK) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN))) != 0)) {
				{
				{
				setState(176);
				funcdcl_or_nl();
				}
				}
				setState(181);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(182);
			match(ENDFUNCTIONS);
			setState(186);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==NL) {
				{
				{
				setState(183);
				match(NL);
				}
				}
				setState(188);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FuncdclContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode FUNCTION() { return getToken(STEPParser.FUNCTION, 0); }
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public ParamsContext params() {
			return getRuleContext(ParamsContext.class,0);
		}
		public RetstmtContext retstmt() {
			return getRuleContext(RetstmtContext.class,0);
		}
		public List<TerminalNode> NL() { return getTokens(STEPParser.NL); }
		public TerminalNode NL(int i) {
			return getToken(STEPParser.NL, i);
		}
		public TerminalNode ENDFUNCTION() { return getToken(STEPParser.ENDFUNCTION, 0); }
		public BracketsContext brackets() {
			return getRuleContext(BracketsContext.class,0);
		}
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public TerminalNode BLANK() { return getToken(STEPParser.BLANK, 0); }
		public FuncdclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcdcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFuncdcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFuncdcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFuncdcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FuncdclContext funcdcl() throws RecognitionException {
		FuncdclContext _localctx = new FuncdclContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_funcdcl);
		int _la;
		try {
			int _alt;
			setState(220);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NUMBER:
			case STRING:
			case BOOLEAN:
				enterOuterAlt(_localctx, 1);
				{
				setState(189);
				type();
				setState(191);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LBRACK) {
					{
					setState(190);
					brackets();
					}
				}

				setState(193);
				match(FUNCTION);
				setState(194);
				match(ID);
				setState(195);
				params();
				setState(199);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,16,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(196);
						stmt();
						}
						} 
					}
					setState(201);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,16,_ctx);
				}
				setState(202);
				retstmt();
				setState(203);
				match(NL);
				setState(204);
				match(ENDFUNCTION);
				setState(205);
				match(NL);
				}
				break;
			case BLANK:
				enterOuterAlt(_localctx, 2);
				{
				setState(207);
				match(BLANK);
				setState(208);
				match(FUNCTION);
				setState(209);
				match(ID);
				setState(210);
				params();
				setState(214);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(211);
					stmt();
					}
					}
					setState(216);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(217);
				match(ENDFUNCTION);
				setState(218);
				match(NL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Funcdcl_or_nlContext extends ParserRuleContext {
		public FuncdclContext funcdcl() {
			return getRuleContext(FuncdclContext.class,0);
		}
		public TerminalNode NL() { return getToken(STEPParser.NL, 0); }
		public Funcdcl_or_nlContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcdcl_or_nl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFuncdcl_or_nl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFuncdcl_or_nl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFuncdcl_or_nl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Funcdcl_or_nlContext funcdcl_or_nl() throws RecognitionException {
		Funcdcl_or_nlContext _localctx = new Funcdcl_or_nlContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_funcdcl_or_nl);
		try {
			setState(224);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case BLANK:
			case NUMBER:
			case STRING:
			case BOOLEAN:
				enterOuterAlt(_localctx, 1);
				{
				setState(222);
				funcdcl();
				}
				break;
			case NL:
				enterOuterAlt(_localctx, 2);
				{
				setState(223);
				match(NL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BracketsContext extends ParserRuleContext {
		public TerminalNode LBRACK() { return getToken(STEPParser.LBRACK, 0); }
		public TerminalNode RBRACK() { return getToken(STEPParser.RBRACK, 0); }
		public BracketsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_brackets; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterBrackets(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitBrackets(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitBrackets(this);
			else return visitor.visitChildren(this);
		}
	}

	public final BracketsContext brackets() throws RecognitionException {
		BracketsContext _localctx = new BracketsContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_brackets);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(226);
			match(LBRACK);
			setState(227);
			match(RBRACK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParamsContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public Params_contentContext params_content() {
			return getRuleContext(Params_contentContext.class,0);
		}
		public ParamsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_params; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterParams(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitParams(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitParams(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ParamsContext params() throws RecognitionException {
		ParamsContext _localctx = new ParamsContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_params);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(229);
			match(LPAREN);
			setState(231);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN))) != 0)) {
				{
				setState(230);
				params_content();
				}
			}

			setState(233);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Params_contentContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public BracketsContext brackets() {
			return getRuleContext(BracketsContext.class,0);
		}
		public List<Params_multiContext> params_multi() {
			return getRuleContexts(Params_multiContext.class);
		}
		public Params_multiContext params_multi(int i) {
			return getRuleContext(Params_multiContext.class,i);
		}
		public Params_contentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_params_content; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterParams_content(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitParams_content(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitParams_content(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Params_contentContext params_content() throws RecognitionException {
		Params_contentContext _localctx = new Params_contentContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_params_content);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(235);
			type();
			setState(237);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LBRACK) {
				{
				setState(236);
				brackets();
				}
			}

			setState(239);
			match(ID);
			setState(243);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(240);
				params_multi();
				}
				}
				setState(245);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Params_multiContext extends ParserRuleContext {
		public TerminalNode COMMA() { return getToken(STEPParser.COMMA, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public BracketsContext brackets() {
			return getRuleContext(BracketsContext.class,0);
		}
		public Params_multiContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_params_multi; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterParams_multi(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitParams_multi(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitParams_multi(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Params_multiContext params_multi() throws RecognitionException {
		Params_multiContext _localctx = new Params_multiContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_params_multi);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(246);
			match(COMMA);
			setState(247);
			type();
			setState(249);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LBRACK) {
				{
				setState(248);
				brackets();
				}
			}

			setState(251);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public TerminalNode NUMBER() { return getToken(STEPParser.NUMBER, 0); }
		public TerminalNode STRING() { return getToken(STEPParser.STRING, 0); }
		public TerminalNode BOOLEAN() { return getToken(STEPParser.BOOLEAN, 0); }
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitType(this);
			else return visitor.visitChildren(this);
		}
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(253);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StmtContext extends ParserRuleContext {
		public TerminalNode NL() { return getToken(STEPParser.NL, 0); }
		public StmtsContext stmts() {
			return getRuleContext(StmtsContext.class,0);
		}
		public StmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterStmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitStmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitStmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StmtContext stmt() throws RecognitionException {
		StmtContext _localctx = new StmtContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_stmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(256);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				setState(255);
				stmts();
				}
			}

			setState(258);
			match(NL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StmtsContext extends ParserRuleContext {
		public IfstmtContext ifstmt() {
			return getRuleContext(IfstmtContext.class,0);
		}
		public WhilestmtContext whilestmt() {
			return getRuleContext(WhilestmtContext.class,0);
		}
		public ForstmtContext forstmt() {
			return getRuleContext(ForstmtContext.class,0);
		}
		public VardclContext vardcl() {
			return getRuleContext(VardclContext.class,0);
		}
		public AssstmtContext assstmt() {
			return getRuleContext(AssstmtContext.class,0);
		}
		public FunccallContext funccall() {
			return getRuleContext(FunccallContext.class,0);
		}
		public RetstmtContext retstmt() {
			return getRuleContext(RetstmtContext.class,0);
		}
		public StmtsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmts; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterStmts(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitStmts(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitStmts(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StmtsContext stmts() throws RecognitionException {
		StmtsContext _localctx = new StmtsContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_stmts);
		try {
			setState(267);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(260);
				ifstmt();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(261);
				whilestmt();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(262);
				forstmt();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(263);
				vardcl();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(264);
				assstmt();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(265);
				funccall();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(266);
				retstmt();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Loop_stmtContext extends ParserRuleContext {
		public TerminalNode NL() { return getToken(STEPParser.NL, 0); }
		public Loop_stmtsContext loop_stmts() {
			return getRuleContext(Loop_stmtsContext.class,0);
		}
		public Loop_stmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_loop_stmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLoop_stmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLoop_stmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLoop_stmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Loop_stmtContext loop_stmt() throws RecognitionException {
		Loop_stmtContext _localctx = new Loop_stmtContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_loop_stmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(270);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				setState(269);
				loop_stmts();
				}
			}

			setState(272);
			match(NL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Loop_stmtsContext extends ParserRuleContext {
		public LoopifstmtContext loopifstmt() {
			return getRuleContext(LoopifstmtContext.class,0);
		}
		public WhilestmtContext whilestmt() {
			return getRuleContext(WhilestmtContext.class,0);
		}
		public ForstmtContext forstmt() {
			return getRuleContext(ForstmtContext.class,0);
		}
		public VardclContext vardcl() {
			return getRuleContext(VardclContext.class,0);
		}
		public AssstmtContext assstmt() {
			return getRuleContext(AssstmtContext.class,0);
		}
		public FunccallContext funccall() {
			return getRuleContext(FunccallContext.class,0);
		}
		public RetstmtContext retstmt() {
			return getRuleContext(RetstmtContext.class,0);
		}
		public Loop_stmtsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_loop_stmts; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLoop_stmts(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLoop_stmts(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLoop_stmts(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Loop_stmtsContext loop_stmts() throws RecognitionException {
		Loop_stmtsContext _localctx = new Loop_stmtsContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_loop_stmts);
		try {
			setState(281);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(274);
				loopifstmt();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(275);
				whilestmt();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(276);
				forstmt();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(277);
				vardcl();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(278);
				assstmt();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(279);
				funccall();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(280);
				retstmt();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LoopifbodyContext extends ParserRuleContext {
		public Loop_stmtContext loop_stmt() {
			return getRuleContext(Loop_stmtContext.class,0);
		}
		public TerminalNode CONTINUE() { return getToken(STEPParser.CONTINUE, 0); }
		public TerminalNode NL() { return getToken(STEPParser.NL, 0); }
		public TerminalNode BREAK() { return getToken(STEPParser.BREAK, 0); }
		public LoopifbodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_loopifbody; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLoopifbody(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLoopifbody(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLoopifbody(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LoopifbodyContext loopifbody() throws RecognitionException {
		LoopifbodyContext _localctx = new LoopifbodyContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_loopifbody);
		try {
			setState(288);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NL:
			case NUMBER:
			case STRING:
			case BOOLEAN:
			case IF:
			case REPEATWHILE:
			case REPEATFOR:
			case RETURN:
			case CONSTANT:
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(283);
				loop_stmt();
				}
				break;
			case CONTINUE:
				enterOuterAlt(_localctx, 2);
				{
				setState(284);
				match(CONTINUE);
				setState(285);
				match(NL);
				}
				break;
			case BREAK:
				enterOuterAlt(_localctx, 3);
				{
				setState(286);
				match(BREAK);
				setState(287);
				match(NL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfstmtContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(STEPParser.IF, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public TerminalNode ENDIF() { return getToken(STEPParser.ENDIF, 0); }
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(STEPParser.ELSE, 0); }
		public IfstmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifstmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterIfstmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitIfstmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitIfstmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final IfstmtContext ifstmt() throws RecognitionException {
		IfstmtContext _localctx = new IfstmtContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_ifstmt);
		int _la;
		try {
			setState(321);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(290);
				match(IF);
				setState(291);
				match(LPAREN);
				setState(292);
				logicexpr(0);
				setState(293);
				match(RPAREN);
				setState(297);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(294);
					stmt();
					}
					}
					setState(299);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(300);
				match(ENDIF);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(302);
				match(IF);
				setState(303);
				match(LPAREN);
				setState(304);
				logicexpr(0);
				setState(305);
				match(RPAREN);
				setState(309);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(306);
					stmt();
					}
					}
					setState(311);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(312);
				match(ELSE);
				setState(316);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(313);
					stmt();
					}
					}
					setState(318);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(319);
				match(ENDIF);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LoopifstmtContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(STEPParser.IF, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public TerminalNode ENDIF() { return getToken(STEPParser.ENDIF, 0); }
		public List<LoopifbodyContext> loopifbody() {
			return getRuleContexts(LoopifbodyContext.class);
		}
		public LoopifbodyContext loopifbody(int i) {
			return getRuleContext(LoopifbodyContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(STEPParser.ELSE, 0); }
		public LoopifstmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_loopifstmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLoopifstmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLoopifstmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLoopifstmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LoopifstmtContext loopifstmt() throws RecognitionException {
		LoopifstmtContext _localctx = new LoopifstmtContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_loopifstmt);
		int _la;
		try {
			setState(354);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,36,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(323);
				match(IF);
				setState(324);
				match(LPAREN);
				setState(325);
				logicexpr(0);
				setState(326);
				match(RPAREN);
				setState(330);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << CONTINUE) | (1L << BREAK) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(327);
					loopifbody();
					}
					}
					setState(332);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(333);
				match(ENDIF);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(335);
				match(IF);
				setState(336);
				match(LPAREN);
				setState(337);
				logicexpr(0);
				setState(338);
				match(RPAREN);
				setState(342);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << CONTINUE) | (1L << BREAK) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(339);
					loopifbody();
					}
					}
					setState(344);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(345);
				match(ELSE);
				setState(349);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << CONTINUE) | (1L << BREAK) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
					{
					{
					setState(346);
					loopifbody();
					}
					}
					setState(351);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(352);
				match(ENDIF);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class WhilestmtContext extends ParserRuleContext {
		public TerminalNode REPEATWHILE() { return getToken(STEPParser.REPEATWHILE, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public TerminalNode ENDWHILE() { return getToken(STEPParser.ENDWHILE, 0); }
		public List<Loop_stmtContext> loop_stmt() {
			return getRuleContexts(Loop_stmtContext.class);
		}
		public Loop_stmtContext loop_stmt(int i) {
			return getRuleContext(Loop_stmtContext.class,i);
		}
		public WhilestmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_whilestmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterWhilestmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitWhilestmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitWhilestmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final WhilestmtContext whilestmt() throws RecognitionException {
		WhilestmtContext _localctx = new WhilestmtContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_whilestmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(356);
			match(REPEATWHILE);
			setState(357);
			match(LPAREN);
			setState(358);
			logicexpr(0);
			setState(359);
			match(RPAREN);
			setState(363);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				{
				setState(360);
				loop_stmt();
				}
				}
				setState(365);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(366);
			match(ENDWHILE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForstmtContext extends ParserRuleContext {
		public TerminalNode REPEATFOR() { return getToken(STEPParser.REPEATFOR, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public For_iter_optContext for_iter_opt() {
			return getRuleContext(For_iter_optContext.class,0);
		}
		public TerminalNode TO() { return getToken(STEPParser.TO, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public TerminalNode COMMA() { return getToken(STEPParser.COMMA, 0); }
		public TerminalNode CHANGEBY() { return getToken(STEPParser.CHANGEBY, 0); }
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public TerminalNode ENDFOR() { return getToken(STEPParser.ENDFOR, 0); }
		public List<Loop_stmtContext> loop_stmt() {
			return getRuleContexts(Loop_stmtContext.class);
		}
		public Loop_stmtContext loop_stmt(int i) {
			return getRuleContext(Loop_stmtContext.class,i);
		}
		public ForstmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forstmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterForstmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitForstmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitForstmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ForstmtContext forstmt() throws RecognitionException {
		ForstmtContext _localctx = new ForstmtContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_forstmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(368);
			match(REPEATFOR);
			setState(369);
			match(LPAREN);
			setState(370);
			for_iter_opt();
			setState(371);
			match(TO);
			setState(372);
			expr(0);
			setState(373);
			match(COMMA);
			setState(374);
			match(CHANGEBY);
			setState(375);
			expr(0);
			setState(376);
			match(RPAREN);
			setState(380);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NL) | (1L << NUMBER) | (1L << STRING) | (1L << BOOLEAN) | (1L << IF) | (1L << REPEATWHILE) | (1L << REPEATFOR) | (1L << RETURN) | (1L << CONSTANT) | (1L << ID))) != 0)) {
				{
				{
				setState(377);
				loop_stmt();
				}
				}
				setState(382);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(383);
			match(ENDFOR);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class For_iter_optContext extends ParserRuleContext {
		public NumdclContext numdcl() {
			return getRuleContext(NumdclContext.class,0);
		}
		public AssstmtContext assstmt() {
			return getRuleContext(AssstmtContext.class,0);
		}
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public ArrindexContext arrindex() {
			return getRuleContext(ArrindexContext.class,0);
		}
		public For_iter_optContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_for_iter_opt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFor_iter_opt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFor_iter_opt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFor_iter_opt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final For_iter_optContext for_iter_opt() throws RecognitionException {
		For_iter_optContext _localctx = new For_iter_optContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_for_iter_opt);
		int _la;
		try {
			setState(391);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,40,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(385);
				numdcl();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(386);
				assstmt();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(387);
				match(ID);
				setState(389);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LBRACK) {
					{
					setState(388);
					arrindex();
					}
				}

				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssstmtContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(STEPParser.ASSIGN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public ArrindexContext arrindex() {
			return getRuleContext(ArrindexContext.class,0);
		}
		public AssstmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assstmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterAssstmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitAssstmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitAssstmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final AssstmtContext assstmt() throws RecognitionException {
		AssstmtContext _localctx = new AssstmtContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_assstmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(393);
			match(ID);
			setState(395);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LBRACK) {
				{
				setState(394);
				arrindex();
				}
			}

			setState(397);
			match(ASSIGN);
			setState(398);
			logicexpr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunccallContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public Params_optionsContext params_options() {
			return getRuleContext(Params_optionsContext.class,0);
		}
		public FunccallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funccall; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFunccall(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFunccall(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFunccall(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FunccallContext funccall() throws RecognitionException {
		FunccallContext _localctx = new FunccallContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_funccall);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(400);
			match(ID);
			setState(401);
			match(LPAREN);
			setState(403);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LPAREN) | (1L << MINUS) | (1L << NEG) | (1L << NUMLITERAL) | (1L << STRLITERAL) | (1L << BOOLLITERAL) | (1L << ID))) != 0)) {
				{
				setState(402);
				params_options();
				}
			}

			setState(405);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Params_optionsContext extends ParserRuleContext {
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public List<Multi_exprContext> multi_expr() {
			return getRuleContexts(Multi_exprContext.class);
		}
		public Multi_exprContext multi_expr(int i) {
			return getRuleContext(Multi_exprContext.class,i);
		}
		public Params_optionsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_params_options; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterParams_options(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitParams_options(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitParams_options(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Params_optionsContext params_options() throws RecognitionException {
		Params_optionsContext _localctx = new Params_optionsContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_params_options);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(407);
			logicexpr(0);
			setState(411);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(408);
				multi_expr();
				}
				}
				setState(413);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Multi_exprContext extends ParserRuleContext {
		public TerminalNode COMMA() { return getToken(STEPParser.COMMA, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public Multi_exprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multi_expr; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterMulti_expr(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitMulti_expr(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitMulti_expr(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Multi_exprContext multi_expr() throws RecognitionException {
		Multi_exprContext _localctx = new Multi_exprContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_multi_expr);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(414);
			match(COMMA);
			setState(415);
			logicexpr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RetstmtContext extends ParserRuleContext {
		public TerminalNode RETURN() { return getToken(STEPParser.RETURN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public RetstmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_retstmt; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterRetstmt(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitRetstmt(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitRetstmt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final RetstmtContext retstmt() throws RecognitionException {
		RetstmtContext _localctx = new RetstmtContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_retstmt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(417);
			match(RETURN);
			setState(419);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LPAREN) | (1L << MINUS) | (1L << NEG) | (1L << NUMLITERAL) | (1L << STRLITERAL) | (1L << BOOLLITERAL) | (1L << ID))) != 0)) {
				{
				setState(418);
				logicexpr(0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArrindexContext extends ParserRuleContext {
		public TerminalNode LBRACK() { return getToken(STEPParser.LBRACK, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TerminalNode RBRACK() { return getToken(STEPParser.RBRACK, 0); }
		public ArrindexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrindex; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterArrindex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitArrindex(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitArrindex(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ArrindexContext arrindex() throws RecognitionException {
		ArrindexContext _localctx = new ArrindexContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_arrindex);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(421);
			match(LBRACK);
			setState(422);
			expr(0);
			setState(423);
			match(RBRACK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExprContext extends ParserRuleContext {
		public TermContext term() {
			return getRuleContext(TermContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TerminalNode PLUS() { return getToken(STEPParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(STEPParser.MINUS, 0); }
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterExpr(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitExpr(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitExpr(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 60;
		enterRecursionRule(_localctx, 60, RULE_expr, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(426);
			term(0);
			}
			_ctx.stop = _input.LT(-1);
			setState(436);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,46,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(434);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,45,_ctx) ) {
					case 1:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(428);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(429);
						match(PLUS);
						setState(430);
						term(0);
						}
						break;
					case 2:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(431);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(432);
						match(MINUS);
						setState(433);
						term(0);
						}
						break;
					}
					} 
				}
				setState(438);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,46,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class TermContext extends ParserRuleContext {
		public FactorContext factor() {
			return getRuleContext(FactorContext.class,0);
		}
		public TermContext term() {
			return getRuleContext(TermContext.class,0);
		}
		public TerminalNode MULT() { return getToken(STEPParser.MULT, 0); }
		public TerminalNode DIVIDE() { return getToken(STEPParser.DIVIDE, 0); }
		public TermContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_term; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterTerm(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitTerm(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitTerm(this);
			else return visitor.visitChildren(this);
		}
	}

	public final TermContext term() throws RecognitionException {
		return term(0);
	}

	private TermContext term(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		TermContext _localctx = new TermContext(_ctx, _parentState);
		TermContext _prevctx = _localctx;
		int _startState = 62;
		enterRecursionRule(_localctx, 62, RULE_term, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(440);
			factor(0);
			}
			_ctx.stop = _input.LT(-1);
			setState(450);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,48,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(448);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,47,_ctx) ) {
					case 1:
						{
						_localctx = new TermContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_term);
						setState(442);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(443);
						match(MULT);
						setState(444);
						factor(0);
						}
						break;
					case 2:
						{
						_localctx = new TermContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_term);
						setState(445);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(446);
						match(DIVIDE);
						setState(447);
						factor(0);
						}
						break;
					}
					} 
				}
				setState(452);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,48,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class FactorContext extends ParserRuleContext {
		public ValueContext value() {
			return getRuleContext(ValueContext.class,0);
		}
		public FactorContext factor() {
			return getRuleContext(FactorContext.class,0);
		}
		public TerminalNode POW() { return getToken(STEPParser.POW, 0); }
		public FactorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_factor; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterFactor(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitFactor(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitFactor(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FactorContext factor() throws RecognitionException {
		return factor(0);
	}

	private FactorContext factor(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		FactorContext _localctx = new FactorContext(_ctx, _parentState);
		FactorContext _prevctx = _localctx;
		int _startState = 64;
		enterRecursionRule(_localctx, 64, RULE_factor, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(454);
			value();
			}
			_ctx.stop = _input.LT(-1);
			setState(461);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,49,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new FactorContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_factor);
					setState(456);
					if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
					setState(457);
					match(POW);
					setState(458);
					value();
					}
					} 
				}
				setState(463);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,49,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ValueContext extends ParserRuleContext {
		public ConstantContext constant() {
			return getRuleContext(ConstantContext.class,0);
		}
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode LPAREN() { return getToken(STEPParser.LPAREN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(STEPParser.RPAREN, 0); }
		public FunccallContext funccall() {
			return getRuleContext(FunccallContext.class,0);
		}
		public TerminalNode MINUS() { return getToken(STEPParser.MINUS, 0); }
		public ArrindexContext arrindex() {
			return getRuleContext(ArrindexContext.class,0);
		}
		public ValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterValue(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitValue(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitValue(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ValueContext value() throws RecognitionException {
		ValueContext _localctx = new ValueContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_value);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(465);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==MINUS) {
				{
				setState(464);
				match(MINUS);
				}
			}

			setState(477);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,52,_ctx) ) {
			case 1:
				{
				setState(467);
				constant();
				}
				break;
			case 2:
				{
				setState(468);
				match(ID);
				setState(470);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
				case 1:
					{
					setState(469);
					arrindex();
					}
					break;
				}
				}
				break;
			case 3:
				{
				setState(472);
				match(LPAREN);
				setState(473);
				logicexpr(0);
				setState(474);
				match(RPAREN);
				}
				break;
			case 4:
				{
				setState(476);
				funccall();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstantContext extends ParserRuleContext {
		public TerminalNode NUMLITERAL() { return getToken(STEPParser.NUMLITERAL, 0); }
		public TerminalNode STRLITERAL() { return getToken(STEPParser.STRLITERAL, 0); }
		public TerminalNode BOOLLITERAL() { return getToken(STEPParser.BOOLLITERAL, 0); }
		public ConstantContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constant; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterConstant(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitConstant(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitConstant(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ConstantContext constant() throws RecognitionException {
		ConstantContext _localctx = new ConstantContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_constant);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(479);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMLITERAL) | (1L << STRLITERAL) | (1L << BOOLLITERAL))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogicexprContext extends ParserRuleContext {
		public LogicequalContext logicequal() {
			return getRuleContext(LogicequalContext.class,0);
		}
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public TerminalNode AND() { return getToken(STEPParser.AND, 0); }
		public TerminalNode OR() { return getToken(STEPParser.OR, 0); }
		public LogicexprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicexpr; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLogicexpr(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLogicexpr(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLogicexpr(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LogicexprContext logicexpr() throws RecognitionException {
		return logicexpr(0);
	}

	private LogicexprContext logicexpr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		LogicexprContext _localctx = new LogicexprContext(_ctx, _parentState);
		LogicexprContext _prevctx = _localctx;
		int _startState = 70;
		enterRecursionRule(_localctx, 70, RULE_logicexpr, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(482);
			logicequal();
			}
			_ctx.stop = _input.LT(-1);
			setState(492);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,54,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(490);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,53,_ctx) ) {
					case 1:
						{
						_localctx = new LogicexprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_logicexpr);
						setState(484);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(485);
						match(AND);
						setState(486);
						logicequal();
						}
						break;
					case 2:
						{
						_localctx = new LogicexprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_logicexpr);
						setState(487);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(488);
						match(OR);
						setState(489);
						logicequal();
						}
						break;
					}
					} 
				}
				setState(494);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,54,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class LogicequalContext extends ParserRuleContext {
		public List<LogiccompContext> logiccomp() {
			return getRuleContexts(LogiccompContext.class);
		}
		public LogiccompContext logiccomp(int i) {
			return getRuleContext(LogiccompContext.class,i);
		}
		public TerminalNode EQ() { return getToken(STEPParser.EQ, 0); }
		public TerminalNode NEQ() { return getToken(STEPParser.NEQ, 0); }
		public LogicequalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicequal; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLogicequal(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLogicequal(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLogicequal(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LogicequalContext logicequal() throws RecognitionException {
		LogicequalContext _localctx = new LogicequalContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_logicequal);
		try {
			setState(504);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,55,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(495);
				logiccomp();
				setState(496);
				match(EQ);
				setState(497);
				logiccomp();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(499);
				logiccomp();
				setState(500);
				match(NEQ);
				setState(501);
				logiccomp();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(503);
				logiccomp();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogiccompContext extends ParserRuleContext {
		public List<LogicvalueContext> logicvalue() {
			return getRuleContexts(LogicvalueContext.class);
		}
		public LogicvalueContext logicvalue(int i) {
			return getRuleContext(LogicvalueContext.class,i);
		}
		public LogiccompopContext logiccompop() {
			return getRuleContext(LogiccompopContext.class,0);
		}
		public LogiccompContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logiccomp; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLogiccomp(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLogiccomp(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLogiccomp(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LogiccompContext logiccomp() throws RecognitionException {
		LogiccompContext _localctx = new LogiccompContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_logiccomp);
		try {
			setState(511);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,56,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(506);
				logicvalue();
				setState(507);
				logiccompop();
				setState(508);
				logicvalue();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(510);
				logicvalue();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogiccompopContext extends ParserRuleContext {
		public TerminalNode GRTHANEQ() { return getToken(STEPParser.GRTHANEQ, 0); }
		public TerminalNode GRTHAN() { return getToken(STEPParser.GRTHAN, 0); }
		public TerminalNode LTHANEQ() { return getToken(STEPParser.LTHANEQ, 0); }
		public TerminalNode LTHAN() { return getToken(STEPParser.LTHAN, 0); }
		public LogiccompopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logiccompop; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLogiccompop(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLogiccompop(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLogiccompop(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LogiccompopContext logiccompop() throws RecognitionException {
		LogiccompopContext _localctx = new LogiccompopContext(_ctx, getState());
		enterRule(_localctx, 76, RULE_logiccompop);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(513);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << GRTHAN) | (1L << GRTHANEQ) | (1L << LTHAN) | (1L << LTHANEQ))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogicvalueContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TerminalNode NEG() { return getToken(STEPParser.NEG, 0); }
		public LogicvalueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicvalue; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterLogicvalue(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitLogicvalue(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitLogicvalue(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LogicvalueContext logicvalue() throws RecognitionException {
		LogicvalueContext _localctx = new LogicvalueContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_logicvalue);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(516);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NEG) {
				{
				setState(515);
				match(NEG);
				}
			}

			setState(518);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VardclContext extends ParserRuleContext {
		public Var_optionsContext var_options() {
			return getRuleContext(Var_optionsContext.class,0);
		}
		public TerminalNode CONSTANT() { return getToken(STEPParser.CONSTANT, 0); }
		public VardclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_vardcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterVardcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitVardcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitVardcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final VardclContext vardcl() throws RecognitionException {
		VardclContext _localctx = new VardclContext(_ctx, getState());
		enterRule(_localctx, 80, RULE_vardcl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(521);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==CONSTANT) {
				{
				setState(520);
				match(CONSTANT);
				}
			}

			setState(523);
			var_options();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Var_optionsContext extends ParserRuleContext {
		public NumdclContext numdcl() {
			return getRuleContext(NumdclContext.class,0);
		}
		public StringdclContext stringdcl() {
			return getRuleContext(StringdclContext.class,0);
		}
		public BooldclContext booldcl() {
			return getRuleContext(BooldclContext.class,0);
		}
		public ArrdclContext arrdcl() {
			return getRuleContext(ArrdclContext.class,0);
		}
		public Var_optionsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_var_options; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterVar_options(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitVar_options(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitVar_options(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Var_optionsContext var_options() throws RecognitionException {
		Var_optionsContext _localctx = new Var_optionsContext(_ctx, getState());
		enterRule(_localctx, 82, RULE_var_options);
		try {
			setState(529);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,59,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(525);
				numdcl();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(526);
				stringdcl();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(527);
				booldcl();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(528);
				arrdcl();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumdclContext extends ParserRuleContext {
		public TerminalNode NUMBER() { return getToken(STEPParser.NUMBER, 0); }
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(STEPParser.ASSIGN, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public NumdclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numdcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterNumdcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitNumdcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitNumdcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final NumdclContext numdcl() throws RecognitionException {
		NumdclContext _localctx = new NumdclContext(_ctx, getState());
		enterRule(_localctx, 84, RULE_numdcl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(531);
			match(NUMBER);
			setState(532);
			match(ID);
			setState(533);
			match(ASSIGN);
			setState(534);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StringdclContext extends ParserRuleContext {
		public TerminalNode STRING() { return getToken(STEPParser.STRING, 0); }
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(STEPParser.ASSIGN, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StringdclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stringdcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterStringdcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitStringdcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitStringdcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StringdclContext stringdcl() throws RecognitionException {
		StringdclContext _localctx = new StringdclContext(_ctx, getState());
		enterRule(_localctx, 86, RULE_stringdcl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(536);
			match(STRING);
			setState(537);
			match(ID);
			setState(538);
			match(ASSIGN);
			setState(539);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BooldclContext extends ParserRuleContext {
		public TerminalNode BOOLEAN() { return getToken(STEPParser.BOOLEAN, 0); }
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(STEPParser.ASSIGN, 0); }
		public LogicexprContext logicexpr() {
			return getRuleContext(LogicexprContext.class,0);
		}
		public BooldclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_booldcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterBooldcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitBooldcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitBooldcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final BooldclContext booldcl() throws RecognitionException {
		BooldclContext _localctx = new BooldclContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_booldcl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(541);
			match(BOOLEAN);
			setState(542);
			match(ID);
			setState(543);
			match(ASSIGN);
			setState(544);
			logicexpr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArrdclContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public ArrsizedclContext arrsizedcl() {
			return getRuleContext(ArrsizedclContext.class,0);
		}
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(STEPParser.ASSIGN, 0); }
		public Arr_id_or_litContext arr_id_or_lit() {
			return getRuleContext(Arr_id_or_litContext.class,0);
		}
		public ArrdclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrdcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterArrdcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitArrdcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitArrdcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ArrdclContext arrdcl() throws RecognitionException {
		ArrdclContext _localctx = new ArrdclContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_arrdcl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(546);
			type();
			setState(547);
			arrsizedcl();
			setState(548);
			match(ID);
			setState(549);
			match(ASSIGN);
			setState(550);
			arr_id_or_lit();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Arr_id_or_litContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(STEPParser.ID, 0); }
		public TerminalNode LBRACK() { return getToken(STEPParser.LBRACK, 0); }
		public TerminalNode RBRACK() { return getToken(STEPParser.RBRACK, 0); }
		public Params_optionsContext params_options() {
			return getRuleContext(Params_optionsContext.class,0);
		}
		public Arr_id_or_litContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arr_id_or_lit; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterArr_id_or_lit(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitArr_id_or_lit(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitArr_id_or_lit(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Arr_id_or_litContext arr_id_or_lit() throws RecognitionException {
		Arr_id_or_litContext _localctx = new Arr_id_or_litContext(_ctx, getState());
		enterRule(_localctx, 92, RULE_arr_id_or_lit);
		int _la;
		try {
			setState(558);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(552);
				match(ID);
				}
				break;
			case LBRACK:
				enterOuterAlt(_localctx, 2);
				{
				setState(553);
				match(LBRACK);
				setState(555);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LPAREN) | (1L << MINUS) | (1L << NEG) | (1L << NUMLITERAL) | (1L << STRLITERAL) | (1L << BOOLLITERAL) | (1L << ID))) != 0)) {
					{
					setState(554);
					params_options();
					}
				}

				setState(557);
				match(RBRACK);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArrsizedclContext extends ParserRuleContext {
		public TerminalNode LBRACK() { return getToken(STEPParser.LBRACK, 0); }
		public TerminalNode NUMLITERAL() { return getToken(STEPParser.NUMLITERAL, 0); }
		public TerminalNode RBRACK() { return getToken(STEPParser.RBRACK, 0); }
		public ArrsizedclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrsizedcl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).enterArrsizedcl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof STEPListener ) ((STEPListener)listener).exitArrsizedcl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof STEPVisitor ) return ((STEPVisitor<? extends T>)visitor).visitArrsizedcl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ArrsizedclContext arrsizedcl() throws RecognitionException {
		ArrsizedclContext _localctx = new ArrsizedclContext(_ctx, getState());
		enterRule(_localctx, 94, RULE_arrsizedcl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(560);
			match(LBRACK);
			setState(561);
			match(NUMLITERAL);
			setState(562);
			match(RBRACK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 30:
			return expr_sempred((ExprContext)_localctx, predIndex);
		case 31:
			return term_sempred((TermContext)_localctx, predIndex);
		case 32:
			return factor_sempred((FactorContext)_localctx, predIndex);
		case 35:
			return logicexpr_sempred((LogicexprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 3);
		case 1:
			return precpred(_ctx, 2);
		}
		return true;
	}
	private boolean term_sempred(TermContext _localctx, int predIndex) {
		switch (predIndex) {
		case 2:
			return precpred(_ctx, 3);
		case 3:
			return precpred(_ctx, 2);
		}
		return true;
	}
	private boolean factor_sempred(FactorContext _localctx, int predIndex) {
		switch (predIndex) {
		case 4:
			return precpred(_ctx, 2);
		}
		return true;
	}
	private boolean logicexpr_sempred(LogicexprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 5:
			return precpred(_ctx, 3);
		case 6:
			return precpred(_ctx, 2);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3?\u0237\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\3\2\7\2d\n\2\f\2\16\2g\13\2"+
		"\3\2\5\2j\n\2\3\2\3\2\5\2n\n\2\3\3\3\3\7\3r\n\3\f\3\16\3u\13\3\3\3\3\3"+
		"\7\3y\n\3\f\3\16\3|\13\3\3\3\3\3\7\3\u0080\n\3\f\3\16\3\u0083\13\3\3\3"+
		"\3\3\7\3\u0087\n\3\f\3\16\3\u008a\13\3\5\3\u008c\n\3\3\4\3\4\7\4\u0090"+
		"\n\4\f\4\16\4\u0093\13\4\3\4\3\4\3\5\3\5\7\5\u0099\n\5\f\5\16\5\u009c"+
		"\13\5\3\5\3\5\3\6\3\6\7\6\u00a2\n\6\f\6\16\6\u00a5\13\6\3\6\3\6\7\6\u00a9"+
		"\n\6\f\6\16\6\u00ac\13\6\3\7\3\7\5\7\u00b0\n\7\3\b\3\b\7\b\u00b4\n\b\f"+
		"\b\16\b\u00b7\13\b\3\b\3\b\7\b\u00bb\n\b\f\b\16\b\u00be\13\b\3\t\3\t\5"+
		"\t\u00c2\n\t\3\t\3\t\3\t\3\t\7\t\u00c8\n\t\f\t\16\t\u00cb\13\t\3\t\3\t"+
		"\3\t\3\t\3\t\3\t\3\t\3\t\3\t\3\t\7\t\u00d7\n\t\f\t\16\t\u00da\13\t\3\t"+
		"\3\t\3\t\5\t\u00df\n\t\3\n\3\n\5\n\u00e3\n\n\3\13\3\13\3\13\3\f\3\f\5"+
		"\f\u00ea\n\f\3\f\3\f\3\r\3\r\5\r\u00f0\n\r\3\r\3\r\7\r\u00f4\n\r\f\r\16"+
		"\r\u00f7\13\r\3\16\3\16\3\16\5\16\u00fc\n\16\3\16\3\16\3\17\3\17\3\20"+
		"\5\20\u0103\n\20\3\20\3\20\3\21\3\21\3\21\3\21\3\21\3\21\3\21\5\21\u010e"+
		"\n\21\3\22\5\22\u0111\n\22\3\22\3\22\3\23\3\23\3\23\3\23\3\23\3\23\3\23"+
		"\5\23\u011c\n\23\3\24\3\24\3\24\3\24\3\24\5\24\u0123\n\24\3\25\3\25\3"+
		"\25\3\25\3\25\7\25\u012a\n\25\f\25\16\25\u012d\13\25\3\25\3\25\3\25\3"+
		"\25\3\25\3\25\3\25\7\25\u0136\n\25\f\25\16\25\u0139\13\25\3\25\3\25\7"+
		"\25\u013d\n\25\f\25\16\25\u0140\13\25\3\25\3\25\5\25\u0144\n\25\3\26\3"+
		"\26\3\26\3\26\3\26\7\26\u014b\n\26\f\26\16\26\u014e\13\26\3\26\3\26\3"+
		"\26\3\26\3\26\3\26\3\26\7\26\u0157\n\26\f\26\16\26\u015a\13\26\3\26\3"+
		"\26\7\26\u015e\n\26\f\26\16\26\u0161\13\26\3\26\3\26\5\26\u0165\n\26\3"+
		"\27\3\27\3\27\3\27\3\27\7\27\u016c\n\27\f\27\16\27\u016f\13\27\3\27\3"+
		"\27\3\30\3\30\3\30\3\30\3\30\3\30\3\30\3\30\3\30\3\30\7\30\u017d\n\30"+
		"\f\30\16\30\u0180\13\30\3\30\3\30\3\31\3\31\3\31\3\31\5\31\u0188\n\31"+
		"\5\31\u018a\n\31\3\32\3\32\5\32\u018e\n\32\3\32\3\32\3\32\3\33\3\33\3"+
		"\33\5\33\u0196\n\33\3\33\3\33\3\34\3\34\7\34\u019c\n\34\f\34\16\34\u019f"+
		"\13\34\3\35\3\35\3\35\3\36\3\36\5\36\u01a6\n\36\3\37\3\37\3\37\3\37\3"+
		" \3 \3 \3 \3 \3 \3 \3 \3 \7 \u01b5\n \f \16 \u01b8\13 \3!\3!\3!\3!\3!"+
		"\3!\3!\3!\3!\7!\u01c3\n!\f!\16!\u01c6\13!\3\"\3\"\3\"\3\"\3\"\3\"\7\""+
		"\u01ce\n\"\f\"\16\"\u01d1\13\"\3#\5#\u01d4\n#\3#\3#\3#\5#\u01d9\n#\3#"+
		"\3#\3#\3#\3#\5#\u01e0\n#\3$\3$\3%\3%\3%\3%\3%\3%\3%\3%\3%\7%\u01ed\n%"+
		"\f%\16%\u01f0\13%\3&\3&\3&\3&\3&\3&\3&\3&\3&\5&\u01fb\n&\3\'\3\'\3\'\3"+
		"\'\3\'\5\'\u0202\n\'\3(\3(\3)\5)\u0207\n)\3)\3)\3*\5*\u020c\n*\3*\3*\3"+
		"+\3+\3+\3+\5+\u0214\n+\3,\3,\3,\3,\3,\3-\3-\3-\3-\3-\3.\3.\3.\3.\3.\3"+
		"/\3/\3/\3/\3/\3/\3\60\3\60\3\60\5\60\u022e\n\60\3\60\5\60\u0231\n\60\3"+
		"\61\3\61\3\61\3\61\3\61\2\6>@BH\62\2\4\6\b\n\f\16\20\22\24\26\30\32\34"+
		"\36 \"$&(*,.\60\62\64\668:<>@BDFHJLNPRTVXZ\\^`\2\5\3\2\')\3\2\31\33\3"+
		"\2\20\23\2\u0256\2e\3\2\2\2\4\u008b\3\2\2\2\6\u008d\3\2\2\2\b\u0096\3"+
		"\2\2\2\n\u009f\3\2\2\2\f\u00af\3\2\2\2\16\u00b1\3\2\2\2\20\u00de\3\2\2"+
		"\2\22\u00e2\3\2\2\2\24\u00e4\3\2\2\2\26\u00e7\3\2\2\2\30\u00ed\3\2\2\2"+
		"\32\u00f8\3\2\2\2\34\u00ff\3\2\2\2\36\u0102\3\2\2\2 \u010d\3\2\2\2\"\u0110"+
		"\3\2\2\2$\u011b\3\2\2\2&\u0122\3\2\2\2(\u0143\3\2\2\2*\u0164\3\2\2\2,"+
		"\u0166\3\2\2\2.\u0172\3\2\2\2\60\u0189\3\2\2\2\62\u018b\3\2\2\2\64\u0192"+
		"\3\2\2\2\66\u0199\3\2\2\28\u01a0\3\2\2\2:\u01a3\3\2\2\2<\u01a7\3\2\2\2"+
		">\u01ab\3\2\2\2@\u01b9\3\2\2\2B\u01c7\3\2\2\2D\u01d3\3\2\2\2F\u01e1\3"+
		"\2\2\2H\u01e3\3\2\2\2J\u01fa\3\2\2\2L\u0201\3\2\2\2N\u0203\3\2\2\2P\u0206"+
		"\3\2\2\2R\u020b\3\2\2\2T\u0213\3\2\2\2V\u0215\3\2\2\2X\u021a\3\2\2\2Z"+
		"\u021f\3\2\2\2\\\u0224\3\2\2\2^\u0230\3\2\2\2`\u0232\3\2\2\2bd\7\27\2"+
		"\2cb\3\2\2\2dg\3\2\2\2ec\3\2\2\2ef\3\2\2\2fi\3\2\2\2ge\3\2\2\2hj\5\n\6"+
		"\2ih\3\2\2\2ij\3\2\2\2jk\3\2\2\2km\5\4\3\2ln\5\16\b\2ml\3\2\2\2mn\3\2"+
		"\2\2n\3\3\2\2\2os\5\6\4\2pr\7\27\2\2qp\3\2\2\2ru\3\2\2\2sq\3\2\2\2st\3"+
		"\2\2\2t\u008c\3\2\2\2us\3\2\2\2vz\5\b\5\2wy\7\27\2\2xw\3\2\2\2y|\3\2\2"+
		"\2zx\3\2\2\2z{\3\2\2\2{\u008c\3\2\2\2|z\3\2\2\2}\u0081\5\6\4\2~\u0080"+
		"\7\27\2\2\177~\3\2\2\2\u0080\u0083\3\2\2\2\u0081\177\3\2\2\2\u0081\u0082"+
		"\3\2\2\2\u0082\u0084\3\2\2\2\u0083\u0081\3\2\2\2\u0084\u0088\5\b\5\2\u0085"+
		"\u0087\7\27\2\2\u0086\u0085\3\2\2\2\u0087\u008a\3\2\2\2\u0088\u0086\3"+
		"\2\2\2\u0088\u0089\3\2\2\2\u0089\u008c\3\2\2\2\u008a\u0088\3\2\2\2\u008b"+
		"o\3\2\2\2\u008bv\3\2\2\2\u008b}\3\2\2\2\u008c\5\3\2\2\2\u008d\u0091\7"+
		"\34\2\2\u008e\u0090\5\36\20\2\u008f\u008e\3\2\2\2\u0090\u0093\3\2\2\2"+
		"\u0091\u008f\3\2\2\2\u0091\u0092\3\2\2\2\u0092\u0094\3\2\2\2\u0093\u0091"+
		"\3\2\2\2\u0094\u0095\7\35\2\2\u0095\7\3\2\2\2\u0096\u009a\7\36\2\2\u0097"+
		"\u0099\5\36\20\2\u0098\u0097\3\2\2\2\u0099\u009c\3\2\2\2\u009a\u0098\3"+
		"\2\2\2\u009a\u009b\3\2\2\2\u009b\u009d\3\2\2\2\u009c\u009a\3\2\2\2\u009d"+
		"\u009e\7\37\2\2\u009e\t\3\2\2\2\u009f\u00a3\7$\2\2\u00a0\u00a2\5\f\7\2"+
		"\u00a1\u00a0\3\2\2\2\u00a2\u00a5\3\2\2\2\u00a3\u00a1\3\2\2\2\u00a3\u00a4"+
		"\3\2\2\2\u00a4\u00a6\3\2\2\2\u00a5\u00a3\3\2\2\2\u00a6\u00aa\7%\2\2\u00a7"+
		"\u00a9\7\27\2\2\u00a8\u00a7\3\2\2\2\u00a9\u00ac\3\2\2\2\u00aa\u00a8\3"+
		"\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\13\3\2\2\2\u00ac\u00aa\3\2\2\2\u00ad"+
		"\u00b0\5R*\2\u00ae\u00b0\7\27\2\2\u00af\u00ad\3\2\2\2\u00af\u00ae\3\2"+
		"\2\2\u00b0\r\3\2\2\2\u00b1\u00b5\7 \2\2\u00b2\u00b4\5\22\n\2\u00b3\u00b2"+
		"\3\2\2\2\u00b4\u00b7\3\2\2\2\u00b5\u00b3\3\2\2\2\u00b5\u00b6\3\2\2\2\u00b6"+
		"\u00b8\3\2\2\2\u00b7\u00b5\3\2\2\2\u00b8\u00bc\7!\2\2\u00b9\u00bb\7\27"+
		"\2\2\u00ba\u00b9\3\2\2\2\u00bb\u00be\3\2\2\2\u00bc\u00ba\3\2\2\2\u00bc"+
		"\u00bd\3\2\2\2\u00bd\17\3\2\2\2\u00be\u00bc\3\2\2\2\u00bf\u00c1\5\34\17"+
		"\2\u00c0\u00c2\5\24\13\2\u00c1\u00c0\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2"+
		"\u00c3\3\2\2\2\u00c3\u00c4\7\"\2\2\u00c4\u00c5\7?\2\2\u00c5\u00c9\5\26"+
		"\f\2\u00c6\u00c8\5\36\20\2\u00c7\u00c6\3\2\2\2\u00c8\u00cb\3\2\2\2\u00c9"+
		"\u00c7\3\2\2\2\u00c9\u00ca\3\2\2\2\u00ca\u00cc\3\2\2\2\u00cb\u00c9\3\2"+
		"\2\2\u00cc\u00cd\5:\36\2\u00cd\u00ce\7\27\2\2\u00ce\u00cf\7#\2\2\u00cf"+
		"\u00d0\7\27\2\2\u00d0\u00df\3\2\2\2\u00d1\u00d2\7&\2\2\u00d2\u00d3\7\""+
		"\2\2\u00d3\u00d4\7?\2\2\u00d4\u00d8\5\26\f\2\u00d5\u00d7\5\36\20\2\u00d6"+
		"\u00d5\3\2\2\2\u00d7\u00da\3\2\2\2\u00d8\u00d6\3\2\2\2\u00d8\u00d9\3\2"+
		"\2\2\u00d9\u00db\3\2\2\2\u00da\u00d8\3\2\2\2\u00db\u00dc\7#\2\2\u00dc"+
		"\u00dd\7\27\2\2\u00dd\u00df\3\2\2\2\u00de\u00bf\3\2\2\2\u00de\u00d1\3"+
		"\2\2\2\u00df\21\3\2\2\2\u00e0\u00e3\5\20\t\2\u00e1\u00e3\7\27\2\2\u00e2"+
		"\u00e0\3\2\2\2\u00e2\u00e1\3\2\2\2\u00e3\23\3\2\2\2\u00e4\u00e5\7\b\2"+
		"\2\u00e5\u00e6\7\t\2\2\u00e6\25\3\2\2\2\u00e7\u00e9\7\6\2\2\u00e8\u00ea"+
		"\5\30\r\2\u00e9\u00e8\3\2\2\2\u00e9\u00ea\3\2\2\2\u00ea\u00eb\3\2\2\2"+
		"\u00eb\u00ec\7\7\2\2\u00ec\27\3\2\2\2\u00ed\u00ef\5\34\17\2\u00ee\u00f0"+
		"\5\24\13\2\u00ef\u00ee\3\2\2\2\u00ef\u00f0\3\2\2\2\u00f0\u00f1\3\2\2\2"+
		"\u00f1\u00f5\7?\2\2\u00f2\u00f4\5\32\16\2\u00f3\u00f2\3\2\2\2\u00f4\u00f7"+
		"\3\2\2\2\u00f5\u00f3\3\2\2\2\u00f5\u00f6\3\2\2\2\u00f6\31\3\2\2\2\u00f7"+
		"\u00f5\3\2\2\2\u00f8\u00f9\7\30\2\2\u00f9\u00fb\5\34\17\2\u00fa\u00fc"+
		"\5\24\13\2\u00fb\u00fa\3\2\2\2\u00fb\u00fc\3\2\2\2\u00fc\u00fd\3\2\2\2"+
		"\u00fd\u00fe\7?\2\2\u00fe\33\3\2\2\2\u00ff\u0100\t\2\2\2\u0100\35\3\2"+
		"\2\2\u0101\u0103\5 \21\2\u0102\u0101\3\2\2\2\u0102\u0103\3\2\2\2\u0103"+
		"\u0104\3\2\2\2\u0104\u0105\7\27\2\2\u0105\37\3\2\2\2\u0106\u010e\5(\25"+
		"\2\u0107\u010e\5,\27\2\u0108\u010e\5.\30\2\u0109\u010e\5R*\2\u010a\u010e"+
		"\5\62\32\2\u010b\u010e\5\64\33\2\u010c\u010e\5:\36\2\u010d\u0106\3\2\2"+
		"\2\u010d\u0107\3\2\2\2\u010d\u0108\3\2\2\2\u010d\u0109\3\2\2\2\u010d\u010a"+
		"\3\2\2\2\u010d\u010b\3\2\2\2\u010d\u010c\3\2\2\2\u010e!\3\2\2\2\u010f"+
		"\u0111\5$\23\2\u0110\u010f\3\2\2\2\u0110\u0111\3\2\2\2\u0111\u0112\3\2"+
		"\2\2\u0112\u0113\7\27\2\2\u0113#\3\2\2\2\u0114\u011c\5*\26\2\u0115\u011c"+
		"\5,\27\2\u0116\u011c\5.\30\2\u0117\u011c\5R*\2\u0118\u011c\5\62\32\2\u0119"+
		"\u011c\5\64\33\2\u011a\u011c\5:\36\2\u011b\u0114\3\2\2\2\u011b\u0115\3"+
		"\2\2\2\u011b\u0116\3\2\2\2\u011b\u0117\3\2\2\2\u011b\u0118\3\2\2\2\u011b"+
		"\u0119\3\2\2\2\u011b\u011a\3\2\2\2\u011c%\3\2\2\2\u011d\u0123\5\"\22\2"+
		"\u011e\u011f\7-\2\2\u011f\u0123\7\27\2\2\u0120\u0121\7.\2\2\u0121\u0123"+
		"\7\27\2\2\u0122\u011d\3\2\2\2\u0122\u011e\3\2\2\2\u0122\u0120\3\2\2\2"+
		"\u0123\'\3\2\2\2\u0124\u0125\7*\2\2\u0125\u0126\7\6\2\2\u0126\u0127\5"+
		"H%\2\u0127\u012b\7\7\2\2\u0128\u012a\5\36\20\2\u0129\u0128\3\2\2\2\u012a"+
		"\u012d\3\2\2\2\u012b\u0129\3\2\2\2\u012b\u012c\3\2\2\2\u012c\u012e\3\2"+
		"\2\2\u012d\u012b\3\2\2\2\u012e\u012f\7+\2\2\u012f\u0144\3\2\2\2\u0130"+
		"\u0131\7*\2\2\u0131\u0132\7\6\2\2\u0132\u0133\5H%\2\u0133\u0137\7\7\2"+
		"\2\u0134\u0136\5\36\20\2\u0135\u0134\3\2\2\2\u0136\u0139\3\2\2\2\u0137"+
		"\u0135\3\2\2\2\u0137\u0138\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u0137\3\2"+
		"\2\2\u013a\u013e\7,\2\2\u013b\u013d\5\36\20\2\u013c\u013b\3\2\2\2\u013d"+
		"\u0140\3\2\2\2\u013e\u013c\3\2\2\2\u013e\u013f\3\2\2\2\u013f\u0141\3\2"+
		"\2\2\u0140\u013e\3\2\2\2\u0141\u0142\7+\2\2\u0142\u0144\3\2\2\2\u0143"+
		"\u0124\3\2\2\2\u0143\u0130\3\2\2\2\u0144)\3\2\2\2\u0145\u0146\7*\2\2\u0146"+
		"\u0147\7\6\2\2\u0147\u0148\5H%\2\u0148\u014c\7\7\2\2\u0149\u014b\5&\24"+
		"\2\u014a\u0149\3\2\2\2\u014b\u014e\3\2\2\2\u014c\u014a\3\2\2\2\u014c\u014d"+
		"\3\2\2\2\u014d\u014f\3\2\2\2\u014e\u014c\3\2\2\2\u014f\u0150\7+\2\2\u0150"+
		"\u0165\3\2\2\2\u0151\u0152\7*\2\2\u0152\u0153\7\6\2\2\u0153\u0154\5H%"+
		"\2\u0154\u0158\7\7\2\2\u0155\u0157\5&\24\2\u0156\u0155\3\2\2\2\u0157\u015a"+
		"\3\2\2\2\u0158\u0156\3\2\2\2\u0158\u0159\3\2\2\2\u0159\u015b\3\2\2\2\u015a"+
		"\u0158\3\2\2\2\u015b\u015f\7,\2\2\u015c\u015e\5&\24\2\u015d\u015c\3\2"+
		"\2\2\u015e\u0161\3\2\2\2\u015f\u015d\3\2\2\2\u015f\u0160\3\2\2\2\u0160"+
		"\u0162\3\2\2\2\u0161\u015f\3\2\2\2\u0162\u0163\7+\2\2\u0163\u0165\3\2"+
		"\2\2\u0164\u0145\3\2\2\2\u0164\u0151\3\2\2\2\u0165+\3\2\2\2\u0166\u0167"+
		"\7/\2\2\u0167\u0168\7\6\2\2\u0168\u0169\5H%\2\u0169\u016d\7\7\2\2\u016a"+
		"\u016c\5\"\22\2\u016b\u016a\3\2\2\2\u016c\u016f\3\2\2\2\u016d\u016b\3"+
		"\2\2\2\u016d\u016e\3\2\2\2\u016e\u0170\3\2\2\2\u016f\u016d\3\2\2\2\u0170"+
		"\u0171\7\60\2\2\u0171-\3\2\2\2\u0172\u0173\7\61\2\2\u0173\u0174\7\6\2"+
		"\2\u0174\u0175\5\60\31\2\u0175\u0176\7\63\2\2\u0176\u0177\5> \2\u0177"+
		"\u0178\7\30\2\2\u0178\u0179\7\64\2\2\u0179\u017a\5> \2\u017a\u017e\7\7"+
		"\2\2\u017b\u017d\5\"\22\2\u017c\u017b\3\2\2\2\u017d\u0180\3\2\2\2\u017e"+
		"\u017c\3\2\2\2\u017e\u017f\3\2\2\2\u017f\u0181\3\2\2\2\u0180\u017e\3\2"+
		"\2\2\u0181\u0182\7\62\2\2\u0182/\3\2\2\2\u0183\u018a\5V,\2\u0184\u018a"+
		"\5\62\32\2\u0185\u0187\7?\2\2\u0186\u0188\5<\37\2\u0187\u0186\3\2\2\2"+
		"\u0187\u0188\3\2\2\2\u0188\u018a\3\2\2\2\u0189\u0183\3\2\2\2\u0189\u0184"+
		"\3\2\2\2\u0189\u0185\3\2\2\2\u018a\61\3\2\2\2\u018b\u018d\7?\2\2\u018c"+
		"\u018e\5<\37\2\u018d\u018c\3\2\2\2\u018d\u018e\3\2\2\2\u018e\u018f\3\2"+
		"\2\2\u018f\u0190\7\n\2\2\u0190\u0191\5H%\2\u0191\63\3\2\2\2\u0192\u0193"+
		"\7?\2\2\u0193\u0195\7\6\2\2\u0194\u0196\5\66\34\2\u0195\u0194\3\2\2\2"+
		"\u0195\u0196\3\2\2\2\u0196\u0197\3\2\2\2\u0197\u0198\7\7\2\2\u0198\65"+
		"\3\2\2\2\u0199\u019d\5H%\2\u019a\u019c\58\35\2\u019b\u019a\3\2\2\2\u019c"+
		"\u019f\3\2\2\2\u019d\u019b\3\2\2\2\u019d\u019e\3\2\2\2\u019e\67\3\2\2"+
		"\2\u019f\u019d\3\2\2\2\u01a0\u01a1\7\30\2\2\u01a1\u01a2\5H%\2\u01a29\3"+
		"\2\2\2\u01a3\u01a5\7;\2\2\u01a4\u01a6\5H%\2\u01a5\u01a4\3\2\2\2\u01a5"+
		"\u01a6\3\2\2\2\u01a6;\3\2\2\2\u01a7\u01a8\7\b\2\2\u01a8\u01a9\5> \2\u01a9"+
		"\u01aa\7\t\2\2\u01aa=\3\2\2\2\u01ab\u01ac\b \1\2\u01ac\u01ad\5@!\2\u01ad"+
		"\u01b6\3\2\2\2\u01ae\u01af\f\5\2\2\u01af\u01b0\7\13\2\2\u01b0\u01b5\5"+
		"@!\2\u01b1\u01b2\f\4\2\2\u01b2\u01b3\7\f\2\2\u01b3\u01b5\5@!\2\u01b4\u01ae"+
		"\3\2\2\2\u01b4\u01b1\3\2\2\2\u01b5\u01b8\3\2\2\2\u01b6\u01b4\3\2\2\2\u01b6"+
		"\u01b7\3\2\2\2\u01b7?\3\2\2\2\u01b8\u01b6\3\2\2\2\u01b9\u01ba\b!\1\2\u01ba"+
		"\u01bb\5B\"\2\u01bb\u01c4\3\2\2\2\u01bc\u01bd\f\5\2\2\u01bd\u01be\7\16"+
		"\2\2\u01be\u01c3\5B\"\2\u01bf\u01c0\f\4\2\2\u01c0\u01c1\7\r\2\2\u01c1"+
		"\u01c3\5B\"\2\u01c2\u01bc\3\2\2\2\u01c2\u01bf\3\2\2\2\u01c3\u01c6\3\2"+
		"\2\2\u01c4\u01c2\3\2\2\2\u01c4\u01c5\3\2\2\2\u01c5A\3\2\2\2\u01c6\u01c4"+
		"\3\2\2\2\u01c7\u01c8\b\"\1\2\u01c8\u01c9\5D#\2\u01c9\u01cf\3\2\2\2\u01ca"+
		"\u01cb\f\4\2\2\u01cb\u01cc\7\17\2\2\u01cc\u01ce\5D#\2\u01cd\u01ca\3\2"+
		"\2\2\u01ce\u01d1\3\2\2\2\u01cf\u01cd\3\2\2\2\u01cf\u01d0\3\2\2\2\u01d0"+
		"C\3\2\2\2\u01d1\u01cf\3\2\2\2\u01d2\u01d4\7\f\2\2\u01d3\u01d2\3\2\2\2"+
		"\u01d3\u01d4\3\2\2\2\u01d4\u01df\3\2\2\2\u01d5\u01e0\5F$\2\u01d6\u01d8"+
		"\7?\2\2\u01d7\u01d9\5<\37\2\u01d8\u01d7\3\2\2\2\u01d8\u01d9\3\2\2\2\u01d9"+
		"\u01e0\3\2\2\2\u01da\u01db\7\6\2\2\u01db\u01dc\5H%\2\u01dc\u01dd\7\7\2"+
		"\2\u01dd\u01e0\3\2\2\2\u01de\u01e0\5\64\33\2\u01df\u01d5\3\2\2\2\u01df"+
		"\u01d6\3\2\2\2\u01df\u01da\3\2\2\2\u01df\u01de\3\2\2\2\u01e0E\3\2\2\2"+
		"\u01e1\u01e2\t\3\2\2\u01e2G\3\2\2\2\u01e3\u01e4\b%\1\2\u01e4\u01e5\5J"+
		"&\2\u01e5\u01ee\3\2\2\2\u01e6\u01e7\f\5\2\2\u01e7\u01e8\7<\2\2\u01e8\u01ed"+
		"\5J&\2\u01e9\u01ea\f\4\2\2\u01ea\u01eb\7=\2\2\u01eb\u01ed\5J&\2\u01ec"+
		"\u01e6\3\2\2\2\u01ec\u01e9\3\2\2\2\u01ed\u01f0\3\2\2\2\u01ee\u01ec\3\2"+
		"\2\2\u01ee\u01ef\3\2\2\2\u01efI\3\2\2\2\u01f0\u01ee\3\2\2\2\u01f1\u01f2"+
		"\5L\'\2\u01f2\u01f3\7\24\2\2\u01f3\u01f4\5L\'\2\u01f4\u01fb\3\2\2\2\u01f5"+
		"\u01f6\5L\'\2\u01f6\u01f7\7\25\2\2\u01f7\u01f8\5L\'\2\u01f8\u01fb\3\2"+
		"\2\2\u01f9\u01fb\5L\'\2\u01fa\u01f1\3\2\2\2\u01fa\u01f5\3\2\2\2\u01fa"+
		"\u01f9\3\2\2\2\u01fbK\3\2\2\2\u01fc\u01fd\5P)\2\u01fd\u01fe\5N(\2\u01fe"+
		"\u01ff\5P)\2\u01ff\u0202\3\2\2\2\u0200\u0202\5P)\2\u0201\u01fc\3\2\2\2"+
		"\u0201\u0200\3\2\2\2\u0202M\3\2\2\2\u0203\u0204\t\4\2\2\u0204O\3\2\2\2"+
		"\u0205\u0207\7\26\2\2\u0206\u0205\3\2\2\2\u0206\u0207\3\2\2\2\u0207\u0208"+
		"\3\2\2\2\u0208\u0209\5> \2\u0209Q\3\2\2\2\u020a\u020c\7>\2\2\u020b\u020a"+
		"\3\2\2\2\u020b\u020c\3\2\2\2\u020c\u020d\3\2\2\2\u020d\u020e\5T+\2\u020e"+
		"S\3\2\2\2\u020f\u0214\5V,\2\u0210\u0214\5X-\2\u0211\u0214\5Z.\2\u0212"+
		"\u0214\5\\/\2\u0213\u020f\3\2\2\2\u0213\u0210\3\2\2\2\u0213\u0211\3\2"+
		"\2\2\u0213\u0212\3\2\2\2\u0214U\3\2\2\2\u0215\u0216\7\'\2\2\u0216\u0217"+
		"\7?\2\2\u0217\u0218\7\n\2\2\u0218\u0219\5> \2\u0219W\3\2\2\2\u021a\u021b"+
		"\7(\2\2\u021b\u021c\7?\2\2\u021c\u021d\7\n\2\2\u021d\u021e\5> \2\u021e"+
		"Y\3\2\2\2\u021f\u0220\7)\2\2\u0220\u0221\7?\2\2\u0221\u0222\7\n\2\2\u0222"+
		"\u0223\5H%\2\u0223[\3\2\2\2\u0224\u0225\5\34\17\2\u0225\u0226\5`\61\2"+
		"\u0226\u0227\7?\2\2\u0227\u0228\7\n\2\2\u0228\u0229\5^\60\2\u0229]\3\2"+
		"\2\2\u022a\u0231\7?\2\2\u022b\u022d\7\b\2\2\u022c\u022e\5\66\34\2\u022d"+
		"\u022c\3\2\2\2\u022d\u022e\3\2\2\2\u022e\u022f\3\2\2\2\u022f\u0231\7\t"+
		"\2\2\u0230\u022a\3\2\2\2\u0230\u022b\3\2\2\2\u0231_\3\2\2\2\u0232\u0233"+
		"\7\b\2\2\u0233\u0234\7\31\2\2\u0234\u0235\7\t\2\2\u0235a\3\2\2\2@eims"+
		"z\u0081\u0088\u008b\u0091\u009a\u00a3\u00aa\u00af\u00b5\u00bc\u00c1\u00c9"+
		"\u00d8\u00de\u00e2\u00e9\u00ef\u00f5\u00fb\u0102\u010d\u0110\u011b\u0122"+
		"\u012b\u0137\u013e\u0143\u014c\u0158\u015f\u0164\u016d\u017e\u0187\u0189"+
		"\u018d\u0195\u019d\u01a5\u01b4\u01b6\u01c2\u01c4\u01cf\u01d3\u01d8\u01df"+
		"\u01ec\u01ee\u01fa\u0201\u0206\u020b\u0213\u022d\u0230";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}