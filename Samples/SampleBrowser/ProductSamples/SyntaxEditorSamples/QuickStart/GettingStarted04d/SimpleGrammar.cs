using System;
using System.Globalization;
using System.Linq;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d {

	/// <summary>
	/// Represents a <see cref="Grammar"/> for the <c>Simple</c> language.
	/// </summary>
	public class SimpleGrammar : Grammar {

		private NonTerminal statement;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleGrammar</c> class.
		/// </summary>
		public SimpleGrammar() : base("Simple") {

			//
			// Create terminals
			//

			var @addition = new Terminal(SimpleTokenId.Addition, "Addition") { ErrorAlias = "'+'" };
			var @assignment = new Terminal(SimpleTokenId.Assignment, "Assignment") { ErrorAlias = "'='" };
			var @closeCurlyBrace = new Terminal(SimpleTokenId.CloseCurlyBrace, "CloseCurlyBrace") { ErrorAlias = "'}'" };
			var @closeParenthesis = new Terminal(SimpleTokenId.CloseParenthesis, "CloseParenthesis") { ErrorAlias = "')'" };
			var @comma = new Terminal(SimpleTokenId.Comma, "Comma") { ErrorAlias = "','" };
			var @division = new Terminal(SimpleTokenId.Division, "Division") { ErrorAlias = "'/'" };
			var @equality = new Terminal(SimpleTokenId.Equality, "Equality") { ErrorAlias = "'=='" };
			var @function = new Terminal(SimpleTokenId.Function, "Function") { ErrorAlias = "'function'" };
			var @identifier = new Terminal(SimpleTokenId.Identifier, "Identifier");
			var @inequality = new Terminal(SimpleTokenId.Inequality, "Inequality") { ErrorAlias = "'!='" };
			var @multiplication = new Terminal(SimpleTokenId.Multiplication, "Multiplication") { ErrorAlias = "'*'" };
			var @number = new Terminal(SimpleTokenId.Number, "Number");
			var @openCurlyBrace = new Terminal(SimpleTokenId.OpenCurlyBrace, "OpenCurlyBrace") { ErrorAlias = "'{'" };
			var @openParenthesis = new Terminal(SimpleTokenId.OpenParenthesis, "OpenParenthesis") { ErrorAlias = "'('" };
			var @return = new Terminal(SimpleTokenId.Return, "Return") { ErrorAlias = "'return'" };
			var @semiColon = new Terminal(SimpleTokenId.SemiColon, "SemiColon") { ErrorAlias = "';'" };
			var @subtraction = new Terminal(SimpleTokenId.Subtraction, "Subtraction") { ErrorAlias = "'-'" };
			var @var = new Terminal(SimpleTokenId.Var, "Var") { ErrorAlias = "'var'" };

			//
			// Create non-terminals
			//

			this.Root = new NonTerminal("CompilationUnit");
			var functionDeclaration = new NonTerminal("FunctionDeclaration");
			var functionParameterList = new NonTerminal("FunctionParameterList");

			// NOTE: ErrorAlias added so that statements will report as expected when they error
			statement = new NonTerminal("Statement") { ErrorAlias = "Statement" };
	
			var block = new NonTerminal("Block");
			var blockChildStatement = new NonTerminal("BlockChildStatement");
			var emptyStatement = new NonTerminal("EmptyStatement");
			var variableDeclarationStatement = new NonTerminal("VariableDeclarationStatement");
			var assignmentStatement = new NonTerminal("AssignmentStatement");
			var returnStatement = new NonTerminal("ReturnStatement");
			var expression = new NonTerminal("Expression");
			var equalityExpression = new NonTerminal("EqualityExpression");
			var additiveExpression = new NonTerminal("AdditiveExpression");
			var multiplicativeExpression = new NonTerminal("MultiplicativeExpression");

			// NOTE: ErrorAlias added so that expressions will report as expected when they error
			var primaryExpression = new NonTerminal("PrimaryExpression") { ErrorAlias = "Expression" };
			
			var numberExpression = new NonTerminal("NumberExpression");
			var simpleName = new NonTerminal("SimpleName");
			var functionAccessExpression = new NonTerminal("FunctionAccessExpression");
			var functionArgumentList = new NonTerminal("FunctionArgumentList");
			var parenthesizedExpression = new NonTerminal("ParenthesizedExpression");

			//
			// Configure non-terminal can-match callbacks
			//

			// NOTE: Next line added so that FunctionDeclaration will always be examined, even if the next token is not 'function'...
			//   This ensures that a non-function token at the start of the document will not prevent the rest of the document
			//   from being parsed... similiar logic applies to BlockChildStatement except we don't force a match if a '}' is found
			functionDeclaration.CanMatchCallback = CanAlwaysMatch;
			blockChildStatement.CanMatchCallback = CanMatchBlockChildStatement;
			functionAccessExpression.CanMatchCallback = CanMatchFunctionAccessExpression;

			//
			// Configure non-terminal productions
			//

			// Outputs a root 'CompilationUnit' node that contains the children of the ZeroOrMore quantifier, 
			//   which is zero or more FunctionDeclarations...
			// If an error occurs in a FunctionDeclaration we will call the AdvanceToDefaultState method,
			//   that is defined below... it advances to the next 'Function' token and flags to continue parsing
			this.Root.Production = functionDeclaration.OnError(AdvanceToDefaultState).ZeroOrMore().SetLabel("decl")
				> Ast<CompilationUnit>().AddToCollectionProperty(cu => cu.Members, AstChildrenFrom("decl"));

			// Outputs a 'FunctionDeclaration' node with children:
			//   1) The function declaration name value
			//   2) A 'Parameters' node that contains each parameter name; or nothing gets inserted
			//      if there are no parameters
			//   3) The Block statement result
			functionDeclaration.Production = @function + @identifier["name"] + @openParenthesis.OnErrorContinue() + functionParameterList["params"].Optional() + @closeParenthesis.OnErrorContinue() + block["block"].OnErrorContinue()
				> Ast<FunctionDeclaration>()
					.SetProperty(d => d.Name, AstFrom("name"))
					.AddToCollectionProperty(d => d.Parameters, AstChildrenFrom("params"))
					.SetProperty(d => d.Body, AstFrom("block"));

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
				> Ast<BlockStatement>().AddToCollectionProperty(s => s.ChildStatements, AstChildrenFrom("stmt"));
		
			// Forces a child statement to try to match via the CanMatch callback, unless the next token is a '}'
			blockChildStatement.Production = statement["stmt"] > AstFrom("stmt");
		
			// Don't output anything since we don't care about empty statements
			emptyStatement.Production = semiColon > null;

			// Outputs a 'VariableDeclarationStatement' node whose child is the variable name
			variableDeclarationStatement.Production = @var + @identifier["name"] + @semiColon.OnErrorContinue()
				> Ast<VariableDeclarationStatement>().SetProperty(s => s.Name, AstFrom("name"));

			// Outputs a 'AssignmentStatement' node whose children are the variable name and the assigned expression
			assignmentStatement.Production = @identifier["varname"] + @assignment + expression["exp"].OnErrorContinue() + @semiColon.OnErrorContinue()
				> Ast<AssignmentStatement>()
					.SetProperty(s => s.VariableName, AstFrom("varname"))
					.SetProperty(s => s.Expression, AstFrom("exp"));

			// Outputs a 'ReturnStatement' node whose child is the expression to return
			returnStatement.Production = @return + expression["exp"].OnErrorContinue() + @semiColon.OnErrorContinue()
				> Ast<ReturnStatement>().SetProperty(s => s.Expression, AstFrom("exp"));

			// Outputs the resulting node from the EqualityExpression
			expression.Production = equalityExpression["exp"] > AstFrom("exp");

			// Outputs the resulting node from the AdditiveExpression; or if an equality operator is found, 
			//   outputs a '==' or '!=' node whose children are the left and right child expressions of the operator
			equalityExpression.Production = additiveExpression["leftexp"] + (
				(
					@equality > Ast(OperatorKind.Equality.ToString()) 
					| @inequality > Ast(OperatorKind.Inequality.ToString())
				).SetLabel("op") + equalityExpression["rightexp"].OnErrorContinue()).Optional()
				> AstLeftAssociativity<Expression, BinaryOperatorExpression>(
					AstConditional<BinaryOperatorExpression>(AstFrom("leftexp"), AstFrom("rightexp"))
						.SetProperty(e => e.Operator, AstFrom("op"))
						.SetProperty(e => e.LeftExpression, AstFrom("leftexp"))
						.SetProperty(e => e.RightExpression, AstFrom("rightexp")),
					n => n.LeftExpression,
					n => n.RightExpression, 
					(n1, n2) => BinaryOperatorKindCheck(n1, n2, OperatorKind.Equality, OperatorKind.Inequality));

			// Outputs the resulting node from the MultiplicativeExpression; or if an additive operator is found, 
			//   outputs a '+' or '-' node whose children are the left and right child expressions of the operator
			additiveExpression.Production = multiplicativeExpression["leftexp"] + (
				(
					@addition > Ast(OperatorKind.Addition.ToString()) 
					| @subtraction > Ast(OperatorKind.Subtraction.ToString())
				).SetLabel("op") + additiveExpression["rightexp"].OnErrorContinue()).Optional()
				> AstLeftAssociativity<Expression, BinaryOperatorExpression>(
					AstConditional<BinaryOperatorExpression>(AstFrom("leftexp"), AstFrom("rightexp"))
						.SetProperty(e => e.Operator, AstFrom("op"))
						.SetProperty(e => e.LeftExpression, AstFrom("leftexp"))
						.SetProperty(e => e.RightExpression, AstFrom("rightexp")),
					n => n.LeftExpression,
					n => n.RightExpression, 
					(n1, n2) => BinaryOperatorKindCheck(n1, n2, OperatorKind.Addition, OperatorKind.Subtraction));
			
			// Outputs the resulting node from the PrimaryExpression; or if an multiplicative operator is found, 
			//   outputs a '*' or '/' node whose children are the left and right child expressions of the operator
			multiplicativeExpression.Production = primaryExpression["leftexp"] + (
				(
					@multiplication > Ast(OperatorKind.Multiplication.ToString()) 
					| @division > Ast(OperatorKind.Division.ToString())
				).SetLabel("op") + multiplicativeExpression["rightexp"].OnErrorContinue()).Optional()
				> AstLeftAssociativity<Expression, BinaryOperatorExpression>(
					AstConditional<BinaryOperatorExpression>(AstFrom("leftexp"), AstFrom("rightexp"))
						.SetProperty(e => e.Operator, AstFrom("op"))
						.SetProperty(e => e.LeftExpression, AstFrom("leftexp"))
						.SetProperty(e => e.RightExpression, AstFrom("rightexp")),
					n => n.LeftExpression, 
					n => n.RightExpression, 
					(n1, n2) => BinaryOperatorKindCheck(n1, n2, OperatorKind.Multiplication, OperatorKind.Division));

			// Outputs the result that came from the appropriate expression type that was matched...
			//   We don't want to wrap the results with a 'Expression' node so we use AstFrom
			primaryExpression.Production = numberExpression["exp"] > AstFrom("exp")
				| functionAccessExpression["exp"] > AstFrom("exp")
				| simpleName["exp"] > AstFrom("exp")
				| parenthesizedExpression["exp"] > AstFrom("exp");

			// Outputs the numeric value
			numberExpression.Production = @number["num"]
				> Ast<LiteralExpression>()
					.SetProperty(e => e.Kind, LiteralExpressionKind.Number)
					.SetProperty(e => e.StringValue, AstFrom("num"));

			// Outputs the name value
			simpleName.Production = @identifier["name"]
				> Ast<SimpleName>().SetProperty(n => n.Text, AstFrom("name"));

			// Outputs a 'FunctionAccessExpression' node with children:
			//   1) The function name
			//   2) An 'Arguments' node that contains each argument name; or nothing gets inserted
			//      if there are no arguments
			functionAccessExpression.Production = @identifier["name"] + @openParenthesis + functionArgumentList["args"].Optional() + @closeParenthesis.OnErrorContinue()
				> Ast<FunctionAccessExpression>()
					.SetProperty(e => e.Name, AstFrom("name"))
					.AddToCollectionProperty(e => e.Arguments, AstChildrenFrom("args"));

			// Outputs a 'FunctionArgumentList' node whose children are argument expressions...
			//   Is an excellent sample of how to build node for a delimited list
			functionArgumentList.Production = expression["exp"] + (@comma + expression["exp"].OnErrorContinue() > AstFrom("exp")).ZeroOrMore().SetLabel("moreexps")
				> Ast("FunctionArgumentList", AstFrom("exp"), AstChildrenFrom("moreexps"));

			// Outputs a 'ParenthesizedExpression' node whose child is the contained expression
			parenthesizedExpression.Production = @openParenthesis + expression["exp"] + @closeParenthesis.OnErrorContinue()
				> Ast<ParenthesizedExpression>().SetProperty(e => e.ChildExpression, AstFrom("exp"));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Advances the token reader to the next 'function' token from where parsing can resume.
		/// </summary>
		/// <param name="state">A <see cref="IParserState"/> that provides information about the parser's current state.</param>
		/// <returns>An <see cref="IParserErrorResult"/> value indicating a result.</returns>
		private IParserErrorResult AdvanceToDefaultState(IParserState state) {
			state.TokenReader.AdvanceTo(SimpleTokenId.Function);
			return ParserErrorResults.Continue;
		}
		
		/// <summary>
		/// Advances the token reader to the next statement or block end from where parsing can resume.
		/// </summary>
		/// <param name="state">A <see cref="IParserState"/> that provides information about the parser's current state.</param>
		/// <returns>An <see cref="IParserErrorResult"/> value indicating a result.</returns>
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
		
		/// <summary>
		/// Examines two <see cref="BinaryOperatorExpression"/> objects to ensure they both have one of the specified operators.
		/// </summary>
		/// <param name="expr1">The first <see cref="BinaryOperatorExpression"/>.</param>
		/// <param name="expr2">The second <see cref="BinaryOperatorExpression"/>.</param>
		/// <param name="operators">The collection of allowed operator kinds.</param>
		/// <returns>
		/// <c>true</c> if both expressions have valid operators; otherwise, <c>false</c>.
		/// </returns>
		internal static bool BinaryOperatorKindCheck(BinaryOperatorExpression expr1, BinaryOperatorExpression expr2, params OperatorKind[] operators) {
			if ((expr1 != null) && (expr2 != null) && (operators != null)) {
				// Ensure the expressions both contain one of the desired operators (to ensure proper operator precedence)
				return ((operators.Contains(expr1.Operator)) && (operators.Contains(expr2.Operator)));
			}
			return false;
		}
		
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
		
		/// <summary>
		/// Returns whether the <c>FunctionAccessExpression</c> non-terminal can match.
		/// </summary>
		/// <param name="state">A <see cref="IParserState"/> that provides information about the parser's current state.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="NonTerminal"/> can match with the current state; otherwise, <c>false</c>.
		/// </returns>
		private bool CanMatchFunctionAccessExpression(IParserState state) {
			return (state.TokenReader.LookAheadToken.Id == SimpleTokenId.Identifier) && 
				(state.TokenReader.GetLookAheadToken(2).Id == SimpleTokenId.OpenParenthesis);
		}
		
	}

}
