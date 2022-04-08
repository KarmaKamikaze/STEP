grammar STEP;

tokens{
  tab = 9;
  cr = 13;
  lf = 10;
  line_terminator = lf | cr | cr lf;
  dblquote = 34;
  hashtag = 35;
  lowercase = ['a' .. 'z'];
  uppercase = ['A' .. 'Z'];
  letter = lowercase | uppercase;
  digit = ['0' .. '9'];
  id_body = letter | digit | '_';
  all_chars = [0 .. 65535];
  string_content = [all_chars - dblquote];
  ml_comment_content = [all_chars - hashtag];
  eol_comment_content = [all_chars - [cr + [lf + hashtag]]];

//Tokens
  end_of_line_comment = '#' eol_comment_content* line_terminator?;
  multiline_comment = '##' ml_comment_content* '##';
  lparen = '(';
  rparen = ')';
  lbrack = '[';
  rbrack = ']';
  assign = '=';
  plus = '+';
  minus = '-';
  divide = '/';
  mult = '*';
  pow = '^';
  grthan = '>';
  grthaneq = '>=';
  lthan = '<';
  lthaneq = '<=';
  eq = '==';
  neq = '!=';
  neg = '!';
  whitespace = (' ' | tab)+;
  nl = line_terminator;
  comma = ',';
  numliteral = digit+ | digit+ '.' digit*;
  strliteral = dblquote string_content* dblquote;
  boolliteral = '!'? 'true'| '!'? 'false';

// Keywords
  setup = 'setup';
  endsetup = 'end setup';
  loop = 'loop';
  endloop = 'end loop';
  functions = 'functions';
  endfunctions = 'end functions';
  function = 'function';
  endfunction = 'end function';
  variables = 'variables';
  endvariables = 'end variables';
  blank = 'blank';
  number = 'number';
  string = 'string';
  boolean = 'boolean';
  if = 'if';
  endif = 'end if';
  else = 'else';
  continue = 'continue';
  break = 'break';
  repeatwhile = 'repeat while';
  endwhile = 'end while';
  repeatfor = 'repeat for';
  endfor = 'end for';
  to = 'to';
  changeby = 'change by';
  switch = 'switch';
  endswitch = 'end switch';
  when = 'when';
  do = 'do';
  fallthrough = 'fallthrough';
  otherwisedo = 'otherwise do';
  return = 'return';
  and = 'and';
  or = 'or';
  constant = 'constant';
  id = letter id_body*;
  }

Ignored Tokens
  whitespace,
  end_of_line_comment,
  multiline_comment;

Productions
  // Program
  program = nl* P.variables? setuploop P.functions?;
  setuploop = {one} P.setup nl* | {two} P.loop nl* | {three} P.setup [fst]:nl* P.loop [snd]:nl*;
  setup = T.setup stmt* endsetup;
  loop = T.loop stmt* endloop;
  
  // Global variable declarations
  variables = T.variables var_or_nl* endvariables nl*;
  var_or_nl = {one} vardcl | {two} nl;
  
  // Function declarations
  functions = T.functions funcdcl_or_nl* endfunctions nl*;
  funcdcl = {one} type brackets? T.function id params stmt* retstmt [fst]:nl endfunction [snd]:nl | {two} blank T.function id params stmt* endfunction nl;
  funcdcl_or_nl = {one} funcdcl | {two} nl;
  
  brackets = lbrack rbrack;
  params = lparen params_content? rparen;
  params_content = type brackets? id params_multi*;
  params_multi = comma type brackets? id;
  type = {one} number | {two} string | {three} boolean;
  
  // Statements
  stmt = stmts nl;
  stmts = {one} ifstmt | {two} whilestmt | {three} forstmt | {four} vardcl | {five} assstmt | {six} funccall | {seven} retstmt | ;
  loop_stmt = loop_stmts nl;
  loop_stmts = {one} loopifstmt | {two} whilestmt | {three} forstmt | {four} vardcl | {five} assstmt | {six} funccall | {seven} retstmt | ;
  loopifbody = {one} loop_stmt | {two} continue nl | {three} break nl;
  ifstmt = {nonelse} if lparen logicexpr rparen stmt* endif| {withelse} if lparen logicexpr rparen [fst]:stmt* else [snd]:stmt* endif;
  loopifstmt = {noelse} if lparen logicexpr rparen loopifbody* endif | {withelse} if lparen logicexpr rparen [true]:loopifbody* else [false]:loopifbody* endif;
  whilestmt = repeatwhile lparen logicexpr rparen loop_stmt* endwhile;
  forstmt = repeatfor lparen for_iter_opt to [fst]:expr comma changeby [snd]:expr rparen loop_stmt* endfor;
  for_iter_opt = {one} numdcl | {two} assstmt | {three} id arrindex?;
  assstmt = id arrindex? assign logicexpr;
  funccall = id lparen params_options? rparen;
  params_options = logicexpr multi_expr*;
  multi_expr = comma logicexpr;
  retstmt = {one} return logicexpr | {two} return;
  arrindex = lbrack expr rbrack;
  
  // Arithmetic expressions
  expr = {one} expr plus term | {two} expr minus term | {three} term;
  term = {one} term mult factor | {two} term divide factor | {three} factor;
  factor = {one} factor pow value | {two} value;
  value = {one} P.constant | {two} id arrindex? | {three} lparen logicexpr rparen | {four} funccall;
  constant = {one} minus? numliteral | {two} strliteral | {three} boolliteral;
  
  // Boolean expressions
  logicexpr = {one} logicexpr and logicequal | {two} logicexpr or logicequal | {three} logicequal;
  logicequal = {one} [fst]:logiccomp eq [snd]:logiccomp | {two} [fst]:logiccomp neq [snd]:logiccomp | {three} logiccomp;
  logiccomp = {one} [fst]:logicvalue logiccompop [snd]:logicvalue | {two} logicvalue;
  logiccompop = {one} grthaneq | {two} grthan | {three} lthaneq | {four} lthan;
  logicvalue = neg? expr;
  
  // Variable declarations
  vardcl = T.constant? var_options;
  var_options = {one} numdcl | {two} stringdcl | {three} booldcl | {four} arrdcl;
  numdcl = number id assign expr;
  stringdcl = string id assign expr;
  booldcl = boolean id assign logicexpr;
  arrdcl = type arrsizedcl id assign arr_id_or_lit;
  arr_id_or_lit = {one} id | {two} lbrack params_options? rbrack;
  arrsizedcl = lbrack numliteral rbrack;