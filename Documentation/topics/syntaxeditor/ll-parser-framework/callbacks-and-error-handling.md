---
title: "Callbacks and Error Handling"
page-title: "Callbacks and Error Handling - SyntaxEditor LL(*) Parser Framework"
order: 9
---
# Callbacks and Error Handling

The next step in building a grammar is to make sure that it properly handles errors.  After all, since this grammar framework is intended to be used with [SyntaxEditor](../index.md), our code editor control, we have to assume that most of the time the document's code passed to our grammar parser will be in an invalid state.  The user is continuously typing and modifying it.

In this topic, we will look at the various callbacks that are available to you, probably the most important of which are the error handling callbacks.  We'll also dig into error handling options.

## What is a Callback?

As we've seen previously, our entire grammar is built directly in C# or VB code.  We do not do code generation like a lot of other parser generators do.  A benefit of this is that you can interact directly with objects in the grammar.

One way to interact with objects is to assign callbacks to them.  All [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm)-based objects support four callbacks:

- Initialize
- Success
- Error
- Complete

And as shown in other topics, [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) objects can be assigned a can-match callback.

Callbacks are simply delegates that get called when certain events occur.  You can point them to methods you declare or can inject lambda expressions as well.

Let's look at each of the five callbacks.

## EbnfTerm Callbacks

### Initialize and Complete callbacks

The `Initialize` callback is called right before an [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) is about to be parsed.  The `Complete` callback is called right after an [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) has been parsed.  Thus, they are always paired.

It is important to note that `Complete` is called regardless of whether the term's parsing succeeded or failed.

### Success and Error callbacks

The `Success` callback is called right after an [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) is parsed successfully.  Alternatively, if the [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) was not parsed successfully, the `Error` callback is called.  Thus, each term that is attempted to be parsed will either have its `Success` or `Error` callbacks fired.

The `Success` and `Error` callbacks occur immediately before the `Complete` callback does.

### Summary of EbnfTerm Callbacks

A term that is successfully parsed will offer this sequence of callbacks:

- Initialize
- (parsing attempt here)
- Success
- Complete

A term that is not successfully parsed will offer this sequence of callbacks:

- Initialize
- (parsing attempt here)
- Error
- Complete

### Definitions

The `Initialize`, `Success`, and `Complete` callbacks have this definition:

```csharp
public delegate void ParserCallback(IParserState state);
```

It is passed an [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) that gives you access to look-ahead tokens, custom data, and the matches that have been made at the current scope level.  You can update custom data, or even modify the matches collection if you wish in any of these callbacks.

Custom data can be anything you wish.  Perhaps as you traverse through certain non-terminals, you want to maintain a stack of which ones you've visited.  Your custom data could contain such a stack.  In the `Initialize` callback for the non-terminals you wish to track, you could push an item on the stack.  In the `Complete` callback for the non-terminals you wish to track, you could pop an item off the stack.

The `Error` callback has this definition:

```csharp
public delegate IParserErrorResult ParserErrorCallback(IParserState state);
```

The `Error` callback also gets an [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) passed to it.  However, it differs from the others in that it expects an [IParserErrorResult](xref:ActiproSoftware.Text.Parsing.LLParser.IParserErrorResult) object returned.  Since the `Error` callback is called when an error occurs, this result tells the parser how to proceed.  There are options for preventing any errors from being reported and options for whether to continue on as if no error occurred.

The standard set of options are provided in the [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults) object via static properties:

- [Default](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Default) – Potentially report errors and return a match failure.
- [Continue](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Continue) – Potentially report errors but continue on.
- [Ignore](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Ignore) – Never report errors and continue on.
- [NoReport](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.NoReport) – Never report errors and return a match failure.

### Sample callback

Callbacks can be assigned with the [OnInitialize](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnInitialize*), [OnSuccess](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnSuccess*), [OnError](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnError*), and [OnComplete](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnComplete*) methods.

This root production shows how an Error callback can be assigned by calling the [OnError](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnError*) method and passing it the delegate to use.  In this case the method that will be called is AdvanceToDefaultState.

```csharp
this.Root.Production = functionDeclaration.OnError(AdvanceToDefaultState).ZeroOrMore().SetLabel("decl")
	> Ast("CompilationUnit", AstChildrenFrom("decl"));
```

What happens is that if an error occurs while parsing FunctionDeclaration, the AdvanceToDefaultState method is called, which does this:

```csharp
/// <summary>
/// Advances the token reader to the next 'function' token from where parsing
/// can resume.
/// </summary>
/// <param name="state">A <see cref="IParserState"/> that provides information
/// about the parser's current state.</param>
/// <returns>An <see cref="IParserErrorResult"/> value indicating a result.</returns>
private IParserErrorResult AdvanceToDefaultState(IParserState state) {
	state.TokenReader.AdvanceTo(SimpleTokenId.Function);
	return ParserErrorResults.Continue;
}
```

You can see how it tells the token reader to advance to the next Function  token.  We have skipped over any potentially "bad" tokens and have gone right to the next token that we know will successfully start a FunctionDeclaration.

The callback returns [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults).[Continue](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Continue), which means potentially report an error, but continue on instead of breaking out of the [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ZeroOrMore*) quantifier that contains the FunctionDeclaration non-terminal.

## Important Notes on Callback Assignments

Both the [Symbol](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol) (terminals and non-terminals) and the various [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm)-based classes have methods to be able to assign the callbacks above, along with the built-in error callbacks described below in this topic.

If the callback assign method is called on a [Symbol](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol)-based class then a related [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm)-based class (either [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal) or [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal)) is created, assigned the callback, and is returned by the method.  It is important to note that the callback is not assigned on the [Symbol](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol) instance itself, only on the [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) class that is returned by the method.

On the other hand, if the callback assign method is called on an [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm)-based class, the callback is directly assigned to it and the [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) same instance is returned.

Say that you want to assign a `Complete` callback onto a `CompilationUnit` non-terminal so you can examine the AST that was created by the parse and report any additional parse errors as needed.  You may be tempted to do this:

```csharp
compilationUnit.Production = [your EBNF production here];
compilationUnit.OnComplete(CompilationUnitComplete);
```

Per above, the various `OnXX` calls on a symbol like [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) will create an [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal) instance and will assign the callback to that.  Since we're not capturing the return value of the [OnComplete](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnComplete*) call here within a production, the EBNF term is discarded and the callback is never used.

One way to do what was intended is to do this instead:

```csharp
compilationUnit.Production = [your EBNF production here];
compilationUnit.Production.CompleteCallback = CompilationUnitComplete;
```

This code assigns the callback to the [EbnfProduction](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfProduction) for the `CompilationUnit` non-terminal, and will thus always get called properly.

Another way to accomplish the same thing is inline like this:

```csharp
compilationUnit.Production = ([your EBNF production here]).OnComplete(CompilationUnitComplete);
```

## Built-in Error Callbacks

There are also some built-in `Error` callbacks that you can assign.  They don't do anything other than return the various related [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults) values:

- [OnErrorContinue](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnErrorContinue*) – Returns [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults).[Continue](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Continue).
- [OnErrorIgnore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnErrorIgnore*) – Returns [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults).[Ignore](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Ignore).
- [OnErrorNoReport](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnErrorNoReport*) – Returns [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults).[NoReport](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.NoReport).

This example shows the use of [OnErrorContinue](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.OnErrorContinue*), where we will report an error if the semi-colon isn't matched, but we'll continue on with parsing as if it was there:

```csharp
variableDeclarationStatement.Production = @var + @identifier["name"] + @semiColon.OnErrorContinue()
	> Ast("VariableDeclarationStatement", AstFrom("name"));
```

## Advanced Error Handling

Sometimes errors will occur where a non-terminal is referenced, however that non-terminal is capable of starting with multiple different terminals.  In that case, the parser doesn't report an error by default since it doesn't know what it should say.  Here's a perfect example:

```csharp
returnStatement.Production = @return + expression["exp"].OnErrorContinue() + @semiColon.OnErrorContinue()
	> Ast("ReturnStatement", AstFrom("exp"));
```

Say the input for this production was `return return`.  Obviously, that is invalid as the second `return` keyword doesn't fit into an expression. `Expression` can start with numerous terminals so an error occurs, but no parse error is reported into the parse errors collection since the parser doesn't know what to tell the UI.

We have two options for handling this scenario.

### Option 1 - Use an Error Alias

When a [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) is assigned an [ErrorAlias](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ErrorAlias), it will report an error by default if it fails to match.  You only want to set error aliases on higher-level non-terminals such as `Expression` or `Statement` non-terminals.  You can do so like this:

```csharp
var expression = new NonTerminal("Expression") { ErrorAlias = "Expression" };
```

That will tell the parser to automatically report an `"Expression expected"` parse error if `Expression` fails to match.  This is the easiest way to handle this scenario.

### Option 2 - Custom Error Callback

The second option that can be used if we need to customize the error message more is to use an error callback:

```csharp
returnStatement.Production = @return + expression["exp"].OnError(ExpressionExpected) + @semiColon.OnErrorContinue()
	> Ast("ReturnStatement", AstFrom("exp"));
```

The error callback can be implemented like this:

```csharp
/// <summary>
/// Occurs when an expression is expected but not found.
/// </summary>
/// <param name="state">A <see cref="IParserState"/> that provides information
/// about the parser's current state.</param>
/// <returns>An <see cref="IParserErrorResult"/> value indicating a result.</returns>
private IParserErrorResult ExpressionExpected(IParserState state) {
	// Report a custom error, and return a value telling the parser to not report
	// errors and continue on
	state.ReportError(ParseErrorLevel.Error, "Expression should have been here.");
	return ParserErrorResults.Ignore;
}
```

Note that here we report an error `"Expression should have been here"` instead of the `"Expression expected"` message that comes from option #1.  We also return [ParserErrorResults](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults).[Ignore](xref:ActiproSoftware.Text.Parsing.LLParser.ParserErrorResults.Ignore) to ensure that no other error message is reported, and tell the parser to continue on.

> [!NOTE]
> The [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) interface defines multiple methods that can be used to report parse errors within callbacks.

## Error Precedence

We've now seen how both terminals and non-terminals are capable of reporting parse errors that can be displayed in the user interface.  In some scenarios, multiple errors may be reported for a given text offset.  Allowing this can really confuse the end user.  The grammar framework has built in functionality such that it will only report the first parse error for a given offset, since that is the most important one.

The parse error collection returned in the parse data result back to the document will also be sorted by each error's location in the document.

## Non-Terminal Can-Match Callbacks

Can-match callbacks can optionally be assigned to any [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal).  Since our grammar is LL-based, each [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) maintains a set of terminals that it knows are able to start it.  This is called the "first set".  For instance, a Simple language `FunctionDeclaration` production always starts with a `Function` terminal.  Thus, the `FunctionDeclaration`'s first set consists of a single `Function` terminal.

Sometimes you may have an alternation EBNF term with two or more non-terminal references that have intersecting first sets.  We see this in the Simple language where both the `SimpleName` and `FunctionAccessExpression` non-terminal productions start with `Identifier` terminals, and the `PrimaryExpression` non-terminal production is an alternation that contains both of them.  This situation is called ambiguity and the grammar will warn you when it detects the scenario so that you can fix it.

When a can-match callback is specified, it effectively overrides the "first set" of the non-terminal.  Thus, in the Simple language where the ambiguity occurred, the ambiguity is resolved by applying a can-match callback to one of the ambiguous non-terminals.

> [!NOTE]
> The [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal).[CanMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.CanMatch*) method can be used to determine if a specified [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState)'s look-ahead token can start to match the [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal).  The method examines the non-terminal's "first set" as well as its can-match callbacks to determine the result.

### Definition

A can-match callback has this definition:

```csharp
public delegate bool ParserCanMatchCallback(IParserState state);
```

It is passed an [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) and the result is a boolean value indicating whether the non-terminal can match with the current state.  Logic in the callback is generally implemented such that it examines look-ahead tokens to see what the next several tokens are.  Since you are able to look ahead all the way to the end of the document if you wish, that is the reason our grammar is LL(*).  The \* means infinite look-ahead.

### Sample callback

The Simple language grammar's `FunctionAccessExpression` has a can-match callback.  This code can be used to assign the callback:

```csharp
functionAccessExpression.CanMatchCallback = CanMatchFunctionAccessExpression;
```

And here is the callback implementation:

```csharp
/// <summary>
/// Returns whether the <c>FunctionAccessExpression</c> non-terminal can match.
/// </summary>
/// <param name="state">A <see cref="IParserState"/> that provides information
/// about the parser's current state.</param>
/// <returns>
/// <c>true</c> if the <see cref="NonTerminal"/> can match with the current state;
/// otherwise, <c>false</c>.
/// </returns>
private bool CanMatchFunctionAccessExpression(IParserState state) {
	return (state.TokenReader.LookAheadToken.Id == SimpleTokenId.Identifier) &&
		(state.TokenReader.GetLookAheadToken(2).Id == SimpleTokenId.OpenParenthesis);
}
```

Smaller callback implementations can be entered directly in a single statement via lambdas like this:

```csharp
functionAccessExpression.CanMatchCallback = (state =>
	(state.TokenReader.LookAheadToken.Id == SimpleTokenId.Identifier) &&
	(state.TokenReader.GetLookAheadToken(2).Id == SimpleTokenId.OpenParenthesis) );
```

### Token Reader AreNext Helper Method

A lot of times, can-match callbacks just involve checking for a specific sequence of two or more tokens such as in the example above.  The [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader).[AreNext](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.AreNext*) helper method is available for these scenarios.  It accepts two or more token IDs and the returns if that exact sequence of tokens is next.

The previous example could therefore be simplified to:

```csharp
functionAccessExpression.CanMatchCallback = (state =>
	state.TokenReader.AreNext(SimpleTokenId.Identifier, SimpleTokenId.OpenParenthesis));
```

### Token Reader Push/Pop Methods

Sometimes a can-match callback may require multiple token look-ahead, where special conditions are checked along the way.  Special [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader).[Push](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.Push*) and related [Pop](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.Pop*) methods are available for this scenario.

Normally reading ahead through tokens in a can-match callback would consume the token, preventing it from later being parsed.  By calling [Push](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.Push*), the state of the token reader is saved.  You then can advance through as many tokens as you wish and then [Pop](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.Pop*) the old state back into place.

Here is an example of their usage for a C# language grammar that checks to see if a local variable declaration can start:

```csharp
private bool CanMatchLocalVariableDeclaration(IParserState state) {
	state.TokenReader.Push();
	try {
		if (state.TokenReader.LookAheadToken.Id == CSharpTokenId.Var) {
			// Advance past 'var'
			state.TokenReader.Advance();
		}
		else if (!this.IsType(state))
			return false;

		// Ensure that an identifier or contextual keyword can match at this point
		return identifierOrContextualKeyword.CanMatch(state);
	}
	finally {
		state.TokenReader.Pop();
	}
}
```

Note we are able to freely advance through tokens and do other calls (like to the `IsType` method) that also advance through more tokens to tell us if the conditions to start this non-terminal are met.

### CanAlwaysMatch Callback

There is a built-in method called [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar).[CanAlwaysMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.CanAlwaysMatch*) which is a can-match callback that always returns `true`.  This callback is useful as described in the next section.

## Terminal Can-Match Callbacks

Although a lesser-used feature, terminals can also support can-match callbacks, similar to non-terminals.  Normally when no can-match callback is specified on a terminal, the terminal's first set is considered the token ID value associated with it.

An example use for this feature is say that you have some contextual keywords, meaning identifiers that turn into keywords but only in certain parser contexts.  The lexer in use may always mark them as identifiers but in certain grammar productions you want to look for these identifiers that have a certain value.  In this case you would define a terminal that had a can-match callback to first make sure the look-ahead token is an identifier.  If so, it would check its text value to see if it matches the contextual keyword text.  That is one way to handle contextual keywords like `var` or `from` in C#.

## Proper Design of Iterative Productions

Very often a root compilation unit has some other set of non-terminals that repeat within it.  In this case we set up a non-terminal EBNF term with an error handler and place it in a [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ZeroOrMore*) quantifier like this:

```csharp
this.Root.Production = functionDeclaration.OnError(AdvanceToDefaultState).ZeroOrMore().SetLabel("decl")
	> Ast("CompilationUnit", AstChildrenFrom("decl"));
```

What happens here is that if an error occurs in `FunctionDeclaration` it will advance to the next `function` token (per above) and will continue on with the next `FunctionDeclaration` match.

But what happens if at the start of the document we have an invalid token instead, such as an `Identifier`? `FunctionDeclaration` doesn't start with an `Identifier` terminal.  It only starts with a `Function` terminal.  Thus, the entire [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ZeroOrMore*) quantifier will never be entered and your `CompilationUnit` AST node output will be empty, even if there are a lot of valid function declarations after that initial `Identifier`.

We can easily handle this scenario by using the [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar).[CanAlwaysMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.CanAlwaysMatch*) callback on `FunctionDeclaration`:

```csharp
// Make sure FunctionDeclaration will always be examined,
// even if the next token is not 'function'
functionDeclaration.CanMatchCallback = CanAlwaysMatch;
```

Thus, we have forced the "first set" of `FunctionDeclaration` to be overridden and even tokens like `Identifier` will cause us to enter `FunctionDeclaration`.  In that scenario, `Identifier` won't match with the `Function` terminal and an error will be reported that indicates `'function' expected`.  This scenario is now properly handled.

### Advanced Implementations

What about languages such as C# where you could have a using statement, namespace, or type declaration at the root compilation unit level?  We'll apply the same concepts.

Make a new non-terminal called `CompilationUnitContent` that has an alternation between those other non-terminals.  Make the root production call `CompilationUnitContent` in the same way `FunctionDeclaration` was called above, with an error handler.  Then likewise, we set the [CanAlwaysMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.CanAlwaysMatch*) callback on the `CompilationUnitContent` non-terminal.

The `AdvanceToDefaultState` method that we use needs to be designed to advance to the next `Using`, `Namespace`, etc. token.  This is easy as the [AdvanceTo](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.AdvanceTo*) method we provide on [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) can accept any number of token ID values.

Finally, we can use one of the two options listed in the Advanced Error Handling section above to report a helpful parse error to the end user.

## Best Practices for Error Handling

This section discusses some best practices to use when implementing error handling in a grammar.

### Looping

When looping on a child non-terminal, it is advisable to have an error handler on the inside of the [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifiableTerm.ZeroOrMore*)/[OneOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifiableTerm.OneOrMore*) so that if an error occurs inside the loop, it can continue on when appropriate.

This sample shows a production that includes an error handler within the quantifier loop.  The custom `CompilationUnitError` error handler has code in it  that  advances the token reader to the next token that can start a `CompilationUnitContent` non-terminal.

```csharp
compilationUnit.Production = compilationUnitContent.OnError(CompilationUnitError).ZeroOrMore();
```

### Consuming Minimum Important Terms

Don't add error handling that always continues on until a minimum number of EBNF terms have been matched to justify an AST result.

In this case, we need a namespace name before it makes sense to build an AST result.

```csharp
namespaceDeclaration.Production = @keywordNamespace + qualifiedIdentifier["name"] + namespaceBody["body"].OnErrorContinue() + @semiColon.Optional();
```

### Don't Add Error Handlers to Everything

Don't put error handlers on all EBNF terms, thus forcing the parser to continue through all productions.  Sometimes it's better to let a bad production fail due to a syntax error and bubble up to a container that has a generalized handler.

### No Error Handling on Optional Terminals

Never add error handling on an optional terminal since there is no way it can error.

In this sample, the `semiColon` terminal doesn't have an error handler on it since it is optional.

```csharp
namespaceDeclaration.Production = @keywordNamespace + qualifiedIdentifier["name"] + namespaceBody["body"].OnErrorContinue() + @semiColon.Optional();
```

### No Error Handling on First Terminal in a Production

Don't put error handlers on a first single terminal in a production.

In this sample, the `keywordNamespace` terminal doesn't have an error handler on it since in order for this non-terminal to be called, a `namespace` keyword must have been present.

```csharp
namespaceDeclaration.Production = @keywordNamespace + qualifiedIdentifier["name"] + namespaceBody["body"].OnErrorContinue() + @semiColon.Optional();
```

### Add Error Handling to Insignificant Terminals at a Production End

Required terminals that end non-terminal productions like `)`, `}` or `;` should always have an [OnErrorContinue](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm.OnErrorContinue*) on them since they are insignificant to the meaning of what is in the document.  A parse error will still be reported for them if they are not present in the code.

In this sample, the `semiColon` terminal has an error handler on it.

```csharp
usingNamespaceDirective.Production = @keywordUsing + namespaceName["name"] + @semiColon.OnErrorContinue();
```

### Watch For .OnErrorContinue().OneOrMore() Infinite Loop Possibilities

Be careful of `.OnErrorContinue().OneOrMore()` scenarios.  Since at least one match is required, if it doesn't perform any token consumption (due to a syntax error), then an infinite loop can occur due to the error handler.  Any [OneOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifiableTerm.OneOrMore*) calls with inner error handlers need to have the error handlers ensure that at least one token is consumed to void the possibility of infinite loops.

### Watch For Can-Match Callback Infinite Loop Possibilities

Similar to the previous tip, when setting a can-match that forces a non-terminal to match (such as is described in the "Proper Design of Iterative Productions" section above), callers of that non-terminal need to have a custom error handler that will force at least one token to be consumed, or else an infinite loop can occur if there is a syntax error.

### Break Out Alternation Options to Separate Non-Terminals

For improved error reporting, break out alternations to their own non-terminal productions and give each new non-terminal an [ErrorAlias](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ErrorAlias) as appropriate.
