grammar STEP;

// Parser Rules

  // Program
  program 
        : NL* variables? setuploop functions?
        ;
        
  setuploop 
        : setup NL* 
        | loop NL* 
        | setup NL* loop NL*
        ;
        
  setup 
        : SETUP stmt* ENDSETUP
        ;
        
  loop 
        : LOOP stmt* ENDLOOP
        ;
  
  // Global variable declarations
  variables 
        : VARIABLES var_or_nl* ENDVARIABLES NL*
        ;
        
  var_or_nl 
        : vardcl 
        | pindcl
        | NL
        ;
  
  // Function declarations
  functions 
        : FUNCTIONS funcdcl_or_nl* ENDFUNCTIONS NL*
        ;
        
  funcdcl 
        : type brackets? FUNCTION ID params stmt* retstmt NL ENDFUNCTION NL 
        | BLANK FUNCTION ID params stmt* ENDFUNCTION NL
        ;
        
  funcdcl_or_nl 
        : funcdcl 
        | NL
        ;
  
  brackets 
        : LBRACK RBRACK
        ;
        
  params 
        : LPAREN params_content? RPAREN
        ;
        
  params_content 
        : paramstype brackets? ID params_multi*
        ;
        
  params_multi 
        : COMMA paramstype brackets? ID
        ;
  
  type 
        : NUMBER 
        | STRING 
        | BOOLEAN
        ;
        
  paramstype
        : pintype
        | type
        ;
  
  // Statements
  stmt 
        : stmts? NL
        ;
        
  stmts 
        : ifstmt 
        | whilestmt 
        | forstmt 
        | vardcl 
        | assstmt 
        | funccall 
        | retstmt
        ;
  
  loop_stmt 
        : loop_stmts? NL
        ;
        
  loop_stmts
        : loopifstmt 
        | whilestmt 
        | forstmt 
        | vardcl 
        | assstmt 
        | funccall 
        | retstmt
        ;
  
  loopifbody 
        : loop_stmt 
        | CONTINUE NL 
        | BREAK NL
        ;
  
  ifstmt
        : IF LPAREN logicexpr RPAREN stmt* elseifstmt* elsestmt? ENDIF;

  elseifstmt
        : ELSE IF LPAREN logicexpr RPAREN stmt*;

  elsestmt
        : ELSE stmt*;

  loopifstmt
        : IF LPAREN logicexpr RPAREN loopifbody* loopelseifstmt* loopelsestmt? ENDIF;

  loopelseifstmt
        : ELSE IF LPAREN logicexpr RPAREN loopifbody*;

  loopelsestmt
        : ELSE loopifbody*;

//  ifstmt 
//        : IF LPAREN logicexpr RPAREN stmt* ENDIF
//        | IF LPAREN logicexpr RPAREN stmt* ELSE stmt* ENDIF
//        ;
//  
//  loopifstmt 
//        : IF LPAREN logicexpr RPAREN loopifbody* ENDIF 
//        | IF LPAREN logicexpr RPAREN loopifbody* ELSE loopifbody* ENDIF
//        ;

  whilestmt 
        : REPEATWHILE LPAREN logicexpr RPAREN loop_stmt* ENDWHILE
        ;
  
  forstmt 
        :  REPEATFOR LPAREN for_iter_opt TO expr COMMA CHANGEBY expr RPAREN loop_stmt* ENDFOR
        ;
  
  for_iter_opt 
        : numdcl 
        | assstmt 
        | ID arrindex?
        ;
  
  assstmt 
        : ID arrindex? ASSIGN logicexpr
        ;
  
  funccall 
        : ID LPAREN params_options? RPAREN
        ;
  
  params_options 
        : logicexpr multi_expr*
        ;
  
  multi_expr 
        : COMMA logicexpr
        ;
  
  retstmt 
        : RETURN logicexpr?
        ;
  
  arrindex 
        : LBRACK expr RBRACK
        ;
  
  // Arithmetic expressions
  expr 
        : expr PLUS term 
        | expr MINUS term 
        | term
        ;
  
  term 
        : term MULT factor 
        | term DIVIDE factor 
        | factor
        ;
  
  factor 
        : factor POW value 
        | value
        ;
  
  value 
        : MINUS? (constant 
        | ID arrindex? 
        | LPAREN logicexpr RPAREN 
        | funccall)
        ;
  
  constant 
        : NUMLITERAL 
        | INTLITERAL
        | STRLITERAL 
        | BOOLLITERAL
        ;
  
  // Boolean expressions
  logicexpr 
        : logicexpr AND logicequal 
        | logicexpr OR logicequal 
        | logicequal
        ;
  
  logicequal 
        : logiccomp EQ logiccomp 
        | logiccomp NEQ logiccomp 
        | logiccomp
        ;
  
  logiccomp 
        : logicvalue logiccompop logicvalue 
        | logicvalue
        ;
  
  logiccompop 
        : GRTHANEQ 
        | GRTHAN 
        | LTHANEQ 
        | LTHAN
        ;
  
  logicvalue 
        : NEG? expr
        ;
  
  
  // Variable declarations
  vardcl 
        : CONSTANT? var_options
        ;
  
  var_options 
        : numdcl 
        | stringdcl 
        | booldcl 
        | arrdcl
        ;
  
  numdcl 
        : NUMBER ID ASSIGN expr
        ;
  
  stringdcl 
        : STRING ID ASSIGN expr
        ;
  
  booldcl 
        : BOOLEAN ID ASSIGN logicexpr
        ;
        
  pindcl
        : pinmode pintype ID ASSIGN INTLITERAL
        ;
        
  pinmode
        : INPUT
        | OUTPUT
        ;
        
  pintype
        : ANALOGPIN
        | DIGITALPIN
        ;
  
  arrdcl 
        : type arrsizedcl ID (ASSIGN arr_id_or_lit)?
        ;
  
  arr_id_or_lit
        : ID 
        | LBRACK params_options? RBRACK
        | funccall
        ;
  
  arrsizedcl 
        : LBRACK INTLITERAL RBRACK
        ;

// Fragments
  fragment LINE_TERMINATOR      : ('\r'? '\n' | '\r');
  fragment DBLQUOTE             : '"';
  fragment HASHTAG              : '#';
  fragment LOWERCASE            : [a-z];
  fragment UPPERCASE            : [A-Z];
  fragment LETTER               : (LOWERCASE | UPPERCASE);
  fragment DIGIT                : [0-9];
  fragment ID_BODY              : LETTER | DIGIT | '_';
  fragment STRING_CONTENT       : ~["];
  fragment ML_COMMENT_CONTENT   : ~[#];
  fragment EOL_COMMENT_CONTENT  : ~[\r\n#];

// Lexer Rules (Tokens)
  WHITESPACE                    : (' ' | '\t') -> skip;
  END_OF_LINE_COMMENT           : '#' EOL_COMMENT_CONTENT* LINE_TERMINATOR? -> skip;
  MULTILINE_COMMENT             : '##' ML_COMMENT_CONTENT* '##' -> skip;
  LPAREN                        : '(';
  RPAREN                        : ')';
  LBRACK                        : '[';
  RBRACK                        : ']';
  ASSIGN                        : '=';
  PLUS                          : '+';
  MINUS                         : '-';
  DIVIDE                        : '/';
  MULT                          : '*';
  POW                           : '^';
  GRTHAN                        : '>';
  GRTHANEQ                      : '>=';
  LTHAN                         : '<';
  LTHANEQ                       : '<=';
  EQ                            : '==';
  NEQ                           : '!:';
  NEG                           : '!';
  NL                            : LINE_TERMINATOR;
  COMMA                         : ',';
  INTLITERAL                    : ('0' | [1-9] DIGIT*);
  NUMLITERAL                    : ('0' | [1-9] DIGIT*) ('.' DIGIT+)?;
  STRLITERAL                    : DBLQUOTE STRING_CONTENT* DBLQUOTE;
  BOOLLITERAL                   : NEG? ('true' | 'false');

// Keywords
  SETUP                         : 'setup';
  ENDSETUP                      : 'end setup';
  LOOP                          : 'loop';
  ENDLOOP                       : 'end loop';
  FUNCTIONS                     : 'functions';
  ENDFUNCTIONS                  : 'end functions';
  FUNCTION                      : 'function';
  ENDFUNCTION                   : 'end function';
  VARIABLES                     : 'variables';
  ENDVARIABLES                  : 'end variables';
  BLANK                         : 'blank';
  NUMBER                        : 'number';
  STRING                        : 'string';
  BOOLEAN                       : 'boolean';
  ANALOGPIN                     : 'analogpin';
  DIGITALPIN                    : 'digitalpin';
  IF                            : 'if';
  ENDIF                         : 'end if';
  ELSE                          : 'else';
  CONTINUE                      : 'continue';
  BREAK                         : 'break';
  REPEATWHILE                   : 'repeat while';
  ENDWHILE                      : 'end while';
  REPEATFOR                     : 'repeat for';
  ENDFOR                        : 'end for';
  TO                            : 'to';
  CHANGEBY                      : 'change by';
  SWITCH                        : 'switch';
  ENDSWITCH                     : 'end switch';
  WHEN                          : 'when';
  DO                            : 'do';
  FALLTHROUGH                   : 'fallthrough';
  OTHERWISEDO                   : 'otherwise do';
  RETURN                        : 'return';
  AND                           : 'and';
  OR                            : 'or';
  CONSTANT                      : 'constant';
  INPUT                         : 'input';
  OUTPUT                        : 'output';
  ID                            : LETTER ID_BODY*;