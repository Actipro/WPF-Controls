---
title: "Walkthrough: Tree Constructors"
page-title: "Walkthrough: Tree Constructors - SyntaxEditor LL(*) Parser Framework"
order: 8
---
# Walkthrough: Tree Constructors

In the [Tree Constructors](tree-constructors.md) topic, we gave an introduction to the powerful tree construction mechanism that is built into the LL(*) Parser Framework and talked about each of the built-in tree construction nodes that are available.

In this walkthrough, we are going to revisit the Simple language and will enhance our grammar productions with core tree constructors so that we make the resulting AST very concise.

## Simple Language Grammar Review

The grammar productions we set up in the [Walkthrough: Symbols and EBNF Terms](walkthrough-symbols-and-terms.md) topic all use the default tree constructors, so the AST output when parsing documents is extremely verbose.

When we look at the output, we see lots of empty nodes that contain other nodes, nodes for tokens such as `(` that we don't care about, and lots of extra nodes for various expressions (like `AdditiveExpression`) that don't really apply here, but which are inserted due to how expressions are defined.

Let's fix all these problems right now by modifying out grammar productions to add tree constructors.

## Grammar Tree Constructor Enhancements

Here is the modified set of grammar productions we’ll use.  Note the tree constructors that are now present in nearly all of the productions as compared to our original grammar productions listed above.

```csharp
// Outputs a root 'CompilationUnit' node that contains the children of the ZeroOrMore quantifier, 
//   which is zero or more FunctionDeclarations
this.Root.Production = functionDeclaration.ZeroOrMore().SetLabel("decl")
	> Ast("CompilationUnit", AstChildrenFrom("decl"));

// Outputs a 'FunctionDeclaration' node with children:
//   1) The function declaration name value
//   2) A 'Parameters' node that contains each parameter name; or nothing gets inserted
//      if there are no parameters
//   3) The Block statement result
functionDeclaration.Production = @function + @identifier["name"] + @openParenthesis + functionParameterList["params"].Optional() + @closeParenthesis + block["block"]
	> Ast("FunctionDeclaration", AstFrom("name"), AstConditionalFrom("Parameters", "params"), AstFrom("block"));

// Outputs a 'FunctionParameterList' node whose children are parameter name values...
//   Is an excellent sample of how to build node for a delimited list
functionParameterList.Production = @identifier["param"] + (@comma + @identifier["param"] > AstFrom("param")).ZeroOrMore().SetLabel("moreparams")
	> Ast("FunctionParameterList", AstFrom("param"), AstChildrenFrom("moreparams"));

// Outputs the result that came from the appropriate statement type that was matched...
//   We don't want to wrap the results with a 'Statement' node so we use AstFrom
statement.Production = block["stmt"] > AstFrom("stmt")
	| emptyStatement > null
	| variableDeclarationStatement["stmt"] > AstFrom("stmt")
	| assignmentStatement["stmt"] > AstFrom("stmt")
	| returnStatement["stmt"] > AstFrom("stmt");

// Outputs a 'Block' node whose children are the contained statements
block.Production = @openCurlyBrace + statement.ZeroOrMore().SetLabel("stmt") + @closeCurlyBrace
	> Ast("Block", AstChildrenFrom("stmt"));
		
// Don't output anything since we don't care about empty statements
emptyStatement.Production = semiColon > null;

// Outputs a 'VariableDeclarationStatement' node whose child is the variable name
variableDeclarationStatement.Production = @var + @identifier["name"] + @semiColon
	> Ast("VariableDeclarationStatement", AstFrom("name"));

// Outputs a 'AssignmentStatement' node whose children are the variable name and the assigned expression
assignmentStatement.Production = @identifier["varname"] + @assignment + expression["exp"] + @semiColon
	> Ast("AssignmentStatement", AstFrom("varname"), AstFrom("exp"));

// Outputs a 'ReturnStatement' node whose child is the expression to return
returnStatement.Production = @return + expression["exp"] + @semiColon
	> Ast("ReturnStatement", AstFrom("exp"));

// Outputs the resulting node from the EqualityExpression
expression.Production = equalityExpression["exp"] > AstFrom("exp");

// Outputs the resulting node from the AdditiveExpression; or if an equality operator is found, 
//   outputs a '==' or '!=' node whose children are the left and right child expressions of the operator
equalityExpression.Production = additiveExpression["leftexp"] + ((@equality | @inequality).SetLabel("op") + equalityExpression["rightexp"]).Optional()
	> AstValueOfConditional(AstChildFrom("op"), AstFrom("leftexp"), AstFrom("rightexp"));

// Outputs the resulting node from the MultiplicativeExpression; or if an additive operator is found, 
//   outputs a '+' or '-' node whose children are the left and right child expressions of the operator
additiveExpression.Production = multiplicativeExpression["leftexp"] + ((@addition | @subtraction).SetLabel("op") + additiveExpression["rightexp"]).Optional()
	> AstValueOfConditional(AstChildFrom("op"), AstFrom("leftexp"), AstFrom("rightexp"));
			
// Outputs the resulting node from the PrimaryExpression; or if an multiplicative operator is found, 
//   outputs a '*' or '/' node whose children are the left and right child expressions of the operator
multiplicativeExpression.Production = primaryExpression["leftexp"] + ((@multiplication | @division).SetLabel("op") + multiplicativeExpression["rightexp"]).Optional()
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
functionAccessExpression.Production = @identifier["name"] + @openParenthesis + functionArgumentList["args"].Optional() + @closeParenthesis
	> Ast("FunctionAccessExpression", AstFrom("name"), AstConditionalFrom("Arguments", "args"));

// Outputs a 'FunctionArgumentList' node whose children are argument expressions...
//   Is an excellent sample of how to build node for a delimited list
functionArgumentList.Production = expression["exp"] + (@comma + expression["exp"] > AstFrom("exp")).ZeroOrMore().SetLabel("moreexps")
	> Ast("FunctionArgumentList", AstFrom("exp"), AstChildrenFrom("moreexps"));

// Outputs a 'ParenthesizedExpression' node whose child is the contained expression
parenthesizedExpression.Production = @openParenthesis + expression["exp"] + @closeParenthesis
	> Ast("ParenthesizedExpression", AstFrom("exp"));
```

You can see we’re now making use of labels and all the various Ast* methods described in the [Tree Constructors](tree-constructors.md) topic.  Let’s use the same input as we did at the end of the [Walkthrough: Symbols and EBNF Terms](walkthrough-symbols-and-terms.md) topic and see how our AST now looks:

```
CompilationUnit[
	FunctionDeclaration[
		"IncrementAndMultiply"
		Parameters[
			"x"
			"y"
		]
		Block[
			VariableDeclarationStatement[
				"result"
			]
			AssignmentStatement[
				"result"
				FunctionAccessExpression[
					"Increment"
					Arguments[
						SimpleName[
							"x"
						]
					]
				]
			]
			ReturnStatement[
				*[
					SimpleName[
						"result"
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

Now that’s a big improvement.  We only have nodes for information that is important to us.  And as mentioned in the past, each AST node that is generated automatically adds which start/end offsets it occurs over, which is invaluable information for consuming the AST later on.

## Elimination of Empty Value Nodes

You’ll note that we completely eliminated all empty value nodes that are generated by quantifiers.  An example is where in the root non-terminal’s production, we create a `CompilationUnit` whose child nodes come directly from the child nodes of the [ZeroOrMore](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ZeroOrMore*) quantifier.

## Elimination of Unimportant Terminal Nodes

We no longer have any AST nodes that relate to unimportant terminals such as `(` or `{`.  We now only have AST nodes for terminals that are meaningful to the code structure.

## Elimination of Expression Nodes That Don’t Apply

Due to how expressions must be defined to properly set up operator precedence, the default tree constructors would create a lot of nodes such as `AdditiveExpression` that didn’t apply for this input.  With our modifications, we’ve completely eliminated those extra expressions.  You’ll note our return statement at the end generates an AST node whose value is `*` (the operator used) and whose two children are the left and right child expressions to be multiplied.  This functionality is achieved via the tree construction node provided by the [AstValueOfConditional](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.AstValueOfConditional*) method.

## More Conditional Output

Although the above input doesn’t show it, say `IncrementAndMultiply` didn’t have any parameters.  Via use of the tree construction node created by [AstConditionalFrom](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar.AstConditionalFrom*), the `Parameters` node would have been left out of the AST output in that case.
