---
title: "Symbols and EBNF Terms"
page-title: "Symbols and EBNF Terms - SyntaxEditor LL(*) Parser Framework"
order: 5
---
# Symbols and EBNF Terms

A grammar is a set of symbols, both terminals or non-terminals, along with production rules that determine how to parse text for a formal language, where there is a single starting symbol from which parsing should begin.

Terminals are the lowest-level fixed-input lexical element used in a grammar production rule.  In the case of the LL(*) Parser Framework, terminals equate to tokens that are read in from a lexer.

Non-terminals each have a production rule that is comprised of some combination of EBNF-like terminal and non-terminal symbol references.

## What is EBNF?

Extended Backus–Naur Form (EBNF) is a meta-syntax notation used for expressing context-free grammars.  Nearly all programming languages have some form of EBNF grammar specification available for reference on the Internet, so that compilers and other products know how to read the code and interpret it.

EBNF supports concatenation, alternation (options), quantifiers (optional, zero-or-more, one-or-more, etc.), and symbol (terminal and non-terminal) references.

### Sample EBNF

Take this EBNF for a Simple language `FunctionDeclaration` non-terminal:

```
FunctionDeclaration:
	"function" "identifier" "(" FunctionParameterList? ")" Block
```

That is saying the non-terminal's name is `FunctionDeclaration` and a function declaration is built by a concatenation of a "function" terminal, followed by an "identifier" terminal, followed by an "open parenthesis" terminal, followed by an optional `FunctionParameterList` non-terminal, followed by a "close parenthesis" terminal, and ending with a `Block` non-terminal.

## EBNF Terms

In the LL(*) Parser Framework, we have the concept of EBNF terms, all of which inherit an [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) base class.  There are multiple classes used to represent various types of EBNF terms, each of which can be used within a production rule for a non-terminal:

- [EbnfProduction](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfProduction)
- [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal)
- [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal)
- [EbnfQuantifier](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifier)
- [EbnfConcatenation](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfConcatenation)
- [EbnfAlternation](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfAlternation)

### EbnfProduction

An [EbnfProduction](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfProduction) represents a production rule.  Each [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) has exactly one [EbnfProduction](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfProduction) associated with it that defines the EBNF for that non-terminal.

[EbnfProduction](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfProduction) objects have one child [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) that provides the EBNF pattern to match for the production rule.  Productions can also optionally be assigned custom tree constructors, but we'll get into that more in the [Tree Constructors](tree-constructors.md) topic.

### EbnfTerminal

An [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal) represents the usage of a pre-defined [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) instance within a production rule.

For instance, a [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) for an `Identifier` is likely to be used many places in a grammar.  The [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) for an `Identifier` is defined once at the top of the grammar declaration and each usage of it within a production is made via separate [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal) instances that reference the [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal).

### EbnfNonTerminal

An [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal) is the same as an [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal) except that instead of referencing a [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal), it is referencing a [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal).

### EbnfQuantifier

An [EbnfQuantifier](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifier) wraps a child [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) and allows you to specify the minimum and maximum number of matches that can be made of the child term.

There are pre-defined helpers for standard Kleene operators such as optional (`?`), zero-or-more (`*`), and one-or-more (`+`).

### EbnfConcatenation

An [EbnfConcatenation](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfConcatenation) is a sequence of other [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) objects that are matched at the same scope level when performing tree construction.

### EbnfAlternation

An [EbnfAlternation](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfAlternation) is a way for one of multiple supplied [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) options to match.  In standard EBNF notation, alternations are typically represented with a vertical bar (`|`).

## Steps to Initialize a Grammar

Now that we've covered the basics, let's dig in and see how it works.  We'll write some grammar creation code that builds the `FunctionDeclaration` non-terminal example above.

A grammar is generally initialized in its constructor with four steps:

1. Create terminals
1. Create non-terminals
1. Configure non-terminal productions
1. Configure non-terminal can-match callbacks

## Step 1: Create Terminals

The first step is where we simply declare [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) variables, one for each token that will be supplied to us by a lexer and used in our non-terminal productions.

For the above example EBNF, we have four terminals, so let's declare them:

```csharp
var @closeParenthesis = new Terminal(SimpleTokenId.CloseParenthesis, "CloseParenthesis")
	{ ErrorAlias = "')'" };
var @function = new Terminal(SimpleTokenId.Function, "Function")
	{ ErrorAlias = "'function'" };
var @identifier = new Terminal(SimpleTokenId.Identifier, "Identifier");
var @openParenthesis = new Terminal(SimpleTokenId.OpenParenthesis, "OpenParenthesis")
	{ ErrorAlias = "'('" };
```

You'll notice that each variable has a `@` in front of it in the C# snippet.  In C#, the `@` character is simply an escape character in case the identifier is a keyword.  In our case, we're going to use it just so later in our non-terminal productions, we can easily tell which variables are terminals and which are non-terminals.  Similar notation could be done in VB if desired using the square brace escape for identifiers as seen in the `function` variable declaration.

The [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) constructor takes a token ID and the string key that identifies the terminal.  Note that the `SimpleTokenId` class contains const token ID values and was generated for us by the [Language Designer](../language-designer-tool/index.md) application.  Token IDs are how the grammar parser matches terminals with tokens that are being read.  The tokens come from the [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) that was created by the [ILLParser](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParser) in use.  See the [Parser Infrastructure](parser-infrastructure.md) topic for more information on those types.

Some of the [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) objects are also assigned an [ErrorAlias](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ErrorAlias).  When parsing, if a terminal is expected, but the current token doesn't match, a parse error will be reported that will use the error alias if it is specified.  If not specified, it will fall back to using the terminal's key.

Now that our four terminals are defined, let's create our non-terminals.

## Step 2: Create Non-Terminals

We now declare [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) variables, one for each non-terminal in our grammar.

For this introduction we will just declare one non-terminal variable even though a real grammar would have a lot more and we are even referencing other non-terminals for `FunctionParameterList` and `Block` in our production below.

```csharp
var functionDeclaraion = new NonTerminal("FunctionDeclaration");
```

## Step 3: Configure Non-Terminal Productions

Finally, we come to the meat of the grammar.  All terminals and non-terminal variables have been declared and we're ready to get into some EBNF-like notation for our non-terminal production rules that consists of various [EbnfTerm](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerm) instances.

This is where the real magic of our grammar framework happens.  The object model has a lot of operator overloads, methods, indexers, and implicit casts so that you can define production rules with a minimal amount of C#/VB code.

For quick reference, the original EBNF syntax was defined as:

```
FunctionDeclaration:
	"function" "identifier" "(" FunctionParameterList? ")" Block
```

Let's define our example non-terminal production rule now.

```csharp
functionDeclaration.Production = @function + @identifier + @openParenthesis +
	functionParameterList.Optional() + @closeParenthesis + block;
```

In C#, it is our convention to have `@` characters at the start of our terminal variables so they stand out differently than non-terminal variables.  The `+` operator means concatenation.  Even though we are referencing [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) and [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal)-based variables here, operator overloading is actually converting each [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) reference to an [EbnfTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfTerminal) and each [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) reference to an [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal) behind the scenes.  Finally note the [Optional](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.Optional*) method call which wraps the [EbnfNonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfNonTerminal) for `FunctionParameterList` with an [EbnfQuantifier](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.EbnfQuantifier) that has a minimum of `0` and a maximum of `1`.

## Step 4: Configure Non-Terminal Can-Match Callbacks

Some grammars have more than one non-terminal that start with the same terminal and since our grammar is LL-based that defaults to one token of look-ahead, it is not allowed to have two or more non-terminals starting with the same terminal in an alternation together.  If that was allowed, then the parser would never have the possibility of matching anything but the first non-terminal in the alternation that started with that terminal.  This is called ambiguity and the grammar automatically detects it for you and warns you about it when it is found.

You can easily resolve ambiguity by setting a can-match callback on non-terminals that require it.  In that case, your custom callback code is executed to determine if a non-terminal can match rather than the normal "first set" way (one token of look-ahead) that is ambiguous.  Since the custom callback has access to all the remaining tokens in the text being parsed, it has infinite look-ahead.  Thus, the grammar as a whole can be considered LL(*).

This is a confusing concept but the [Walkthrough: Symbols and EBNF Terms](walkthrough-symbols-and-terms.md) topic shows an example of why and when to do this.

## Document End Terminal

Some languages like Visual Basic use line terminators to mark the end of statements.  Problems could arise if at the end of a document there was a line containing a statement and was immediately followed by the document end without a required line terminator in place.  Since line terminators are significant to completing the statement and there was a document end in place of the line terminator, a parse error would be reported.

To handle this scenario, the framework supports matching against document end.  The document end terminal must use the standard document end token ID specified in the static [DocumentEnd](xref:ActiproSoftware.Text.Lexing.Implementation.TokenIdProviderBase.DocumentEnd) property.

Once a document end terminal has been defined, it can be used in a production like this, which correctly handles the scenario mentioned above:

```csharp
statement.Production = statementBody + (@lineTerminator | @documentEnd);
```

### Important Notes

When matching against the document end terminal, it is very important to realize that the parser is already at the document end, so no text is consumed when the match is made.  This is unlike normal terminal matches in which case the matched text is consumed.

Thus, if the document end terminal is referenced in a scenario where it can be repeatedly matched (such as in a quantifier) without any other required terminal matches in between, the parser could enter an infinite loop scenario.

This example code shows what NOT to do.  Note the `OneOrMore` quantifier that could enter infinite looping at document end:

```csharp
statement.Production = statementBody + (@lineTerminator | @documentEnd).OneOrMore();
```

In languages like VB where there could be empty statements (effectively just a line terminator), make sure you only have the empty statement production reference the line terminator terminal, and not the document end terminal.

## Next Steps

Wasn't that easy?  Of course, a real grammar would have a lot more terminals, non-terminals, and production rules but you just follow the same process for them too.

There is one more thing to mention.  Let's assume we have the following input to the parser for this grammar:

```
function Add() {}
```

The default tree constructors will build a full AST for you without you needing to do anything.  Of course it includes much more information than is practical, which is why we'll show in [Tree Constructors](tree-constructors.md) topic how to optimize tree construction.  Anyhow, here is a text representation of the AST node output for that function using the default tree constructor:

```
FunctionDeclaration[
	"function"
	"Add"
	"("
	[]
	")"
	Block[
		"{"
		[]
		"}"
	]
]
```

Each AST node in the output automatically stores the proper start/end offsets for you too making it possible to query which node contains an offset later on.
