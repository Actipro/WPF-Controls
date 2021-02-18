---
title: "The 'Simple' Language (used in samples)"
page-title: "The 'Simple' Language (used in samples) - SyntaxEditor Reference"
order: 12
---
# The 'Simple' Language (used in samples)

The 'Simple' language is a very basic sort of language that we created to help demonstrate how to implement language-related features with SyntaxEditor and the text/parsing framework.  It is used in several samples.

The Simple language supports statements, expressions, and function/variable declarations.

## Code Sample

This snippet shows some sample Simple language code:

```
/*
	The Simple language demonstrates a C-like
	language that only knows several keywords and 
	operators.  All variables are integer numbers.
*/

function Add(x, y) {
	return x + y;
}

function Increment(x) {
	return (x + 1);
}

function IncrementAndMultiply(x, y) {
	// This function calls another function
	var result;
	result = Increment(x);
	return result * y;
}
```

## Lexical Structure

The Simple language is case sensitive, similar to C.

Identifiers must start with a letter or underscore, and can only contain letters, digits, and underscore characters.

Only integer numbers are supported.

Keywords include:

- `function` - Declares a function
- `return` - Returns a value
- `var` - Declares a variable

Operators include:

- `=` - Assignment
- `==` - Equality
- `!=` - Inequality
- `+` - Addition
- `-` - Subtraction
- `*` - Multiplication
- `/` - Division

Delimiters include:

- `{` - Scope start
- `}` - Scope end
- `(` - Group start
- `)` - Group end
- `,` - Comma
- `;` - Statement end

Comments include:

- `// Single-line comment`
- `/* Range comment */`

## Semantic Structure (in EBNF)

This EBNF describes the semantic structure of the Simple language:

```
CompilationUnit:
	FunctionDeclarationList?

FunctionDeclarationList:
	FunctionDeclaration
	FunctionDeclarationList FunctionDeclaration

FunctionDeclaration:
	"function" "identifier" "(" FunctionParameterList? ")" Block

FunctionParameterList:
	"identifier"
	FunctionParameterList "," "identifier"

StatementList:
	Statement
	StatementList Statement

Statement:
	Block
	EmptyStatement
	VariableDeclarationStatement
	AssignmentStatement
	ReturnStatement
	
Block:
	"{" StatementList? "}"

EmptyStatement:
	";"
	
VariableDeclarationStatement:
	"var" "identifier" ";"

AssignmentStatement:
	"identifier" "=" Expression ";"
	
ReturnStatement:
	"return" Expression ";"
	
Expression: 
	EqualityExpression

EqualityExpression:
	AdditiveExpression
	AdditiveExpression "==" EqualityExpression
	AdditiveExpression "!=" EqualityExpression
	
AdditiveExpression:
	MultiplicativeExpression
	MultiplicativeExpression "+" AdditiveExpression
	MultiplicativeExpression "-" AdditiveExpression

MultiplicativeExpression:
	PrimaryExpression
	PrimaryExpression "*" MultiplicativeExpression
	PrimaryExpression "/" MultiplicativeExpression
	
PrimaryExpression:
	NumberExpression
	SimpleName
	FunctionAccessExpression
	ParenthesizedExpression

NumberExpression:
	"number"

SimpleName:
	"identifier"

FunctionAccessExpression:
	"identifier" "(" FunctionArgumentList? ")"

FunctionArgumentList:
	FunctionArgument
	FunctionArgumentList "," FunctionArgument

FunctionArgument:
	Expression

ParenthesizedExpression:
	"(" Expression ")"
```
