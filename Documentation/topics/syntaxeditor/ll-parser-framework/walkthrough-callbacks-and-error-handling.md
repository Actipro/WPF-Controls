---
title: "Walkthrough: Callbacks and Error Handling"
page-title: "Walkthrough: Callbacks and Error Handling - SyntaxEditor LL(*) Parser Framework"
order: 10
---
# Walkthrough: Callbacks and Error Handling

In the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic, we saw how the grammar framework supports callbacks nearly everywhere in the EBNF terms.  You are able to inject custom code before and after any term is matched.  One of the most important injection points is the Error callback since that allows you to tell the parser how to handle errors.

In this topic we’ll examine the importance of adding error handling.  We’ll enhance our Simple language grammar to properly recover from nearly any invalid syntax in a document so that it can continue parsing the rest of the document.

## Why Add Error Handling?

An error occurs when one or more terminals is expected by the parser (per the grammar) but a different token is found at the current location in the document.

By default when this scenario occurs, the parser will report a parse error if a single terminal is expected.  If there is more than one terminal that could be next, and if the containing non-terminal has an error alias set, it will report a parse error instead.  This behavior is described in the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic.

Following any possible error reporting, the parser will immediately exit the current non-terminal and will continue up the non-terminal stack, exiting each one as it goes until the root non-terminal is reached and exited itself.

Thus the result is that if any invalid syntax is found in a document, parsing stops at that point and no AST will be built.  That’s definitely not good since we expect code being parsed from our SyntaxEditor control editing to be invalid most of the time.  End users are continuously typing in it.

As shown in the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic, the grammar framework has a lot ways to recover from errors, and they are easy to add.

## Enhancing the Simple Language Grammar with Error Handling

### Adding an Error Alias to Some Non-Terminals

The first step in adding error handling is to identify which non-terminals should get error aliases (described in the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic).  When a non-terminal has an error alias and an error occurs within it, it will report a parse error.

In the Simple language grammar, we’ll add an error alias to the `Statement` and `PrimaryExpression` non-terminals like this:

```csharp
var statement = new NonTerminal("Statement") { ErrorAlias = "Statement" };
var primaryExpression = new NonTerminal("PrimaryExpression") { ErrorAlias = "Expression" };
```

Now say we are parsing and an `Expression` is expected next but our look-ahead token is `Var`.  The [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) for `Var` doesn’t start any expressions, so this is an error.  Thus an `Expression expected` error will be reported since we assigned an error alias to the `PrimaryExpression` non-terminal, which is indirectly required by the `Expression` non-terminal.

### Adding Iterative Error Handling in the Root Production

As described in the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic, when you have a scenario where some child non-terminal is being repeated, which often is the case in the root non-terminal, we want to force the child non-terminal being repeated to get parsed.  We do this, even if the look-ahead token doesn’t match the child non-terminal’s "first set."

Otherwise if some token was first in the document that didn’t match with the "first set" of the child non-terminal, the parsing would exit immediately, which is not good.

We can force a non-terminal to ignore its "first set" and always be matched by setting its can-match callback to the built in [CanAlwaysMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.CanAlwaysMatch*) method on [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar):

```csharp
// NOTE: Next line added so that FunctionDeclaration will always be examined, 
//   even if the next token is not 'function'... This ensures that a non-function 
//   token at the start of the document will not prevent the rest of the document
//   from being parsed
functionDeclaration.CanMatchCallback = CanAlwaysMatch;
```

The next step is to add an error callback to the `FunctionDeclaration` EBNF non-terminal:

```csharp
// Outputs a root 'CompilationUnit' node that contains the children of the ZeroOrMore quantifier, 
//   which is zero or more FunctionDeclarations...
// If an error occurs in a FunctionDeclaration we will call the AdvanceToDefaultState method,
//   that is defined below... it advances to the next 'Function' token and flags to continue parsing
this.Root.Production = functionDeclaration.OnError(AdvanceToDefaultState).ZeroOrMore().SetLabel("decl")
	> Ast("CompilationUnit", AstChildrenFrom("decl"));
```

The `AdvanceToDefaultState` method is called when an unhandled error occurs in `FunctionDeclaration`.  It advances to the next `Function` token, which is what properly starts a `FunctionDeclaration`, thus skipping over any other tokens that would cause more parse errors.  It then tells the parser to continue on.

```csharp
/// <summary>
/// Advances the token reader to the next 'function' token from where 
/// parsing can resume.
/// </summary>
/// <param name="state">An <c>IParserState</c> that provides information 
/// about the parser's current state.</param>
/// <returns>An <c>IParserErrorResult</c> value indicating a result.</returns>
private IParserErrorResult AdvanceToDefaultState(IParserState state) {
	state.TokenReader.AdvanceTo(SimpleTokenId.Function);
	return ParserErrorResults.Continue;
}
```

Let’s walk through what happens now.  Say there is an invalid look-ahead token while parsing in the middle of a `FunctionDeclaration`.  An error will occur and possibly be reported.  The error will bubble up to the root non-terminal, where the error callback will execute `AdvanceToDefaultState`.  That moves to the next `Function` token and then tells the parser to continue on.  Thus the [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ZeroOrMore*) quantifier in the root non-terminal will not be exited and the `FunctionDeclaration` non-terminal will be entered again.

This design is very important because it allows us recover from invalid syntax and to continue on parsing a document.

### Adding Iterative Error Handling for Statements

In block statements, we'll want to force the next token to be parsed as a statement as long as we aren't at the document end or if the token isn't a '}' character.  This ensures that any tokens that don't start a statement will be flagged as an error and skipped over.  To accomplish this, we add a new `BlockChildStatement` non-terminal and set a can-match callback:

```csharp
var blockChildStatement = new NonTerminal("BlockChildStatement");
blockChildStatement.CanMatchCallback = CanMatchBlockChildStatement;

// Forces a child statement to try to match via the CanMatch callback, unless the next token is a '}'
blockChildStatement.Production = statement["stmt"] > AstFrom("stmt");
```

```csharp
/// <summary>
/// Returns whether the <c>BlockChildStatement</c> non-terminal can match.
/// </summary>
/// <param name="state">A <see cref="IParserState"/> that provides information about the parser's current state.</param>
/// <returns>
/// <c>true</c> if the <see cref="NonTerminal"/> can match with the current state; otherwise, <c>false</c>.
/// </returns>
private bool CanMatchBlockChildStatement(IParserState state) {
	// Match as long as we aren't at the document end or at a curly brace
	return (!state.TokenReader.IsAtEnd) && (state.TokenReader.LookAheadToken.Id != SimpleTokenId.CloseCurlyBrace);
}
```

The `Block` production changes its `Statement` non-terminal reference to be the new `BlockChildStatement` non-terminal instead.  Finally, we need to inject a custom error handler so that when a child statement fails to match, the token reader is advanced to the next token that can start a statement or is the block close ('}').  The `Block` production is updated to call a custom `AdvanceToStatementOrBlockEnd` callback if an error occurs while parsing a child statement.

```csharp
// Outputs a 'Block' node whose children are the contained statements
block.Production = @openCurlyBrace + blockChildStatement.OnError(AdvanceToStatementOrBlockEnd).ZeroOrMore().SetLabel("stmt") + @closeCurlyBrace.OnErrorContinue()
	> Ast("Block", AstChildrenFrom("stmt"));
```

`AdvanceToStatementOrBlockEnd` is set up to advance through tokens until the next one is reached that can either close the block or start a new statement.  Note that part of this code requires the direct use of the `Statement` non-terminal, so the `statement` variable in the constructor has been changed to a class field instead.

```csharp
/// <summary>
/// Advances the token reader to the next statement or block end from where parsing can resume.
/// </summary>
/// <param name="state">An <c>IParserState</c> that provides information 
/// about the parser's current state.</param>
/// <returns>An <c>IParserErrorResult</c> value indicating a result.</returns>
private IParserErrorResult AdvanceToStatementOrBlockEnd(IParserState state) {
	while (!state.TokenReader.IsAtEnd) {
		// Quit when a statement or block end is reached
		if (
			(state.TokenReader.LookAheadToken.Id == SimpleTokenId.CloseCurlyBrace) ||
			(statement.CanMatch(state))
			)
			return ParserErrorResults.Continue;

		// Advance a token
		state.TokenReader.Advance();
	}

	return ParserErrorResults.Continue;
}
```

### Adding More Inline Error Handling to Productions

The last step we’ll do to add error handling to the Simple language grammar is to insert [OnErrorContinue](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm.OnErrorContinue*) handlers in various places throughout the productions.

It’s good to insert them on [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) objects that are generally insignificant we don’t want things like a missing semi-colon to prevent an entire function declaration from being parsed.  Here’s an example:

```csharp
// Outputs a 'VariableDeclarationStatement' node whose child is the variable name
variableDeclarationStatement.Production = @var + @identifier["name"] + @semiColon.OnErrorContinue()
	> Ast("VariableDeclarationStatement", AstFrom("name"));
```

It’s up to you to determine when you think a parser should just continue on (by specifying [OnErrorContinue](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm.OnErrorContinue*)) and when it should bubble up to the calling non-terminal.  The grammar framework gives you complete flexibility in how you wish to process things.

## Final Grammar Productions for the Simple Language

Here is our finalized set of grammar productions for the Simple language.  It completely parses Simple code, builds a concise AST, and properly recovers from syntax errors.

```csharp
// Outputs a root 'CompilationUnit' node that contains the children of the ZeroOrMore quantifier, 
//   which is zero or more FunctionDeclarations...
// If an error occurs in a FunctionDeclaration we will call the AdvanceToDefaultState method,
//   that is defined below... it advances to the next 'Function' token and flags to continue parsing
this.Root.Production = functionDeclaration.OnError(AdvanceToDefaultState).ZeroOrMore().SetLabel("decl")
	> Ast("CompilationUnit", AstChildrenFrom("decl"));

// Outputs a 'FunctionDeclaration' node with children:
//   1) The function declaration name value
//   2) A 'Parameters' node that contains each parameter name; or nothing gets inserted
//      if there are no parameters
//   3) The Block statement result
functionDeclaration.Production = @function + @identifier["name"] + @openParenthesis.OnErrorContinue() + functionParameterList["params"].Optional() + @closeParenthesis.OnErrorContinue() + block["block"].OnErrorContinue()
	> Ast("FunctionDeclaration", AstFrom("name"), AstConditionalFrom("Parameters", "params"), AstFrom("block"));

// Outputs a 'FunctionParameterList' node whose children are parameter name values...
//   Is an excellent sample of how to build node for a delimited list
functionParameterList.Production = @identifier["param"] + (@comma + @identifier["param"].OnErrorContinue() > AstFrom("param")).ZeroOrMore().SetLabel("moreparams")
	> Ast("FunctionParameterList", AstFrom("param"), AstChildrenFrom("moreparams"));

// Outputs the result that came from the appropriate statement type that was matched...
//   We don't want to wrap the results with a 'Statement' node so we use AstFrom
statement.Production = block["stmt"] > AstFrom("stmt")
	| emptyStatement > null
	| variableDeclarationStatement["stmt"] > AstFrom("stmt")
	| assignmentStatement["stmt"] > AstFrom("stmt")
	| returnStatement["stmt"] > AstFrom("stmt");

// Outputs a 'Block' node whose children are the contained statements
block.Production = @openCurlyBrace + blockChildStatement.OnError(AdvanceToStatementOrBlockEnd).ZeroOrMore().SetLabel("stmt") + @closeCurlyBrace.OnErrorContinue()
	> Ast("Block", AstChildrenFrom("stmt"));

// Forces a child statement to try to match via the CanMatch callback, unless the next token is a '}'
blockChildStatement.Production = statement["stmt"] > AstFrom("stmt");
		
// Don't output anything since we don't care about empty statements
emptyStatement.Production = semiColon > null;

// Outputs a 'VariableDeclarationStatement' node whose child is the variable name
variableDeclarationStatement.Production = @var + @identifier["name"] + @semiColon.OnErrorContinue()
	> Ast("VariableDeclarationStatement", AstFrom("name"));

// Outputs a 'AssignmentStatement' node whose children are the variable name and the assigned expression
assignmentStatement.Production = @identifier["varname"] + @assignment + expression["exp"].OnErrorContinue() + @semiColon.OnErrorContinue()
	> Ast("AssignmentStatement", AstFrom("varname"), AstFrom("exp"));

// Outputs a 'ReturnStatement' node whose child is the expression to return
returnStatement.Production = @return + expression["exp"].OnErrorContinue() + @semiColon.OnErrorContinue()
	> Ast("ReturnStatement", AstFrom("exp"));

// Outputs the resulting node from the EqualityExpression
expression.Production = equalityExpression["exp"] > AstFrom("exp");

// Outputs the resulting node from the AdditiveExpression; or if an equality operator is found, 
//   outputs a '==' or '!=' node whose children are the left and right child expressions of the operator
equalityExpression.Production = additiveExpression["leftexp"] + ((@equality | @inequality).SetLabel("op") + equalityExpression["rightexp"].OnErrorContinue()).Optional()
	> AstValueOfConditional(AstChildFrom("op"), AstFrom("leftexp"), AstFrom("rightexp"));

// Outputs the resulting node from the MultiplicativeExpression; or if an additive operator is found, 
//   outputs a '+' or '-' node whose children are the left and right child expressions of the operator
additiveExpression.Production = multiplicativeExpression["leftexp"] + ((@addition | @subtraction).SetLabel("op") + additiveExpression["rightexp"].OnErrorContinue()).Optional()
	> AstValueOfConditional(AstChildFrom("op"), AstFrom("leftexp"), AstFrom("rightexp"));
			
// Outputs the resulting node from the PrimaryExpression; or if an multiplicative operator is found, 
//   outputs a '*' or '/' node whose children are the left and right child expressions of the operator
multiplicativeExpression.Production = primaryExpression["leftexp"] + ((@multiplication | @division).SetLabel("op") + multiplicativeExpression["rightexp"].OnErrorContinue()).Optional()
	> AstValueOfConditional(AstChildFrom("op"), AstFrom("leftexp"), AstFrom("rightexp"));

// Outputs the result that came from the appropriate expression type that was matched...
//   We don't want to wrap the results with a 'Expression' node so we use AstFrom
primaryExpression.Production = numberExpression["exp"] > AstFrom("exp")
	| functionAccessExpression["exp"] > AstFrom("exp")
	| simpleName["exp"] > AstFrom("exp")
	| parenthesizedExpression["exp"] > AstFrom("exp");

// Outputs the numeric value
numberExpression.Production = @number.ToTerm().ToProduction();

// Outputs the name value
simpleName.Production = @identifier.ToTerm().ToProduction();

// Outputs a 'FunctionAccessExpression' node with children:
//   1) The function name
//   2) An 'Arguments' node that contains each argument name; or nothing gets inserted
//      if there are no arguments
functionAccessExpression.Production = @identifier["name"] + @openParenthesis + functionArgumentList["args"].Optional() + @closeParenthesis.OnErrorContinue()
	> Ast("FunctionAccessExpression", AstFrom("name"), AstConditionalFrom("Arguments", "args"));

// Outputs a 'FunctionArgumentList' node whose children are argument expressions...
//   Is an excellent sample of how to build node for a delimited list
functionArgumentList.Production = expression["exp"] + (@comma + expression["exp"].OnErrorContinue() > AstFrom("exp")).ZeroOrMore().SetLabel("moreexps")
	> Ast("FunctionArgumentList", AstFrom("exp"), AstChildrenFrom("moreexps"));

// Outputs a 'ParenthesizedExpression' node whose child is the contained expression
parenthesizedExpression.Production = @openParenthesis + expression["exp"] + @closeParenthesis.OnErrorContinue()
	> Ast("ParenthesizedExpression", AstFrom("exp"));
```

## Error Handling in Action

Now let’s see our error handling in action.  We’ll use this input code:

```
function Add(x, y {
	return x + y;
}
```

Note that there is a `)` missing in the function declaration and a `')' expected` error is reported.  But we still get this complete AST, as if there were no syntax error:

```
CompilationUnit[
	FunctionDeclaration[
		"Add"
		Parameters[
			"x"
			"y"
		]
		Block[
			ReturnStatement[
				+[
					SimpleName[
						"x"
					]
					SimpleName[
						"y"
					]
				]
			]
		]
	]
]
```

Let’s create some more complex syntax errors now:

```
invalid
function Add(x,) {
	return x +
}
```

Here we have three syntax errors:

1. `'function' expected` that highlights the invalid word

1. `Identifier expected` that highlights the `)`

1. `'Expression' expected` that highlights the `}`

Even with three syntax errors, we still get an AST:

```
CompilationUnit[
	FunctionDeclaration[
		"Add"
		Parameters[
			"x"
		]
		Block[
			ReturnStatement[
				SimpleName[
					"x"
				]
			]
		]
	]
]
```

## Conclusion

In this topic, we've seen how to enhance a grammar to properly support robust error handling.  Really, this is about the last thing you generally need to do when designing a grammar.
