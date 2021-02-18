using System;
using System.Globalization;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04a {

	/// <summary>
	/// Represents a <see cref="Grammar"/> for the <c>Simple</c> language.
	/// </summary>
	public class SimpleGrammar : Grammar {
		
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
			var statement = new NonTerminal("Statement");
			var block = new NonTerminal("Block");
			var emptyStatement = new NonTerminal("EmptyStatement");
			var variableDeclarationStatement = new NonTerminal("VariableDeclarationStatement");
			var assignmentStatement = new NonTerminal("AssignmentStatement");
			var returnStatement = new NonTerminal("ReturnStatement");
			var expression = new NonTerminal("Expression");
			var equalityExpression = new NonTerminal("EqualityExpression");
			var additiveExpression = new NonTerminal("AdditiveExpression");
			var multiplicativeExpression = new NonTerminal("MultiplicativeExpression");
			var primaryExpression = new NonTerminal("PrimaryExpression");
			var numberExpression = new NonTerminal("NumberExpression");
			var simpleName = new NonTerminal("SimpleName");
			var functionAccessExpression = new NonTerminal("FunctionAccessExpression");
			var functionArgumentList = new NonTerminal("FunctionArgumentList");
			var parenthesizedExpression = new NonTerminal("ParenthesizedExpression");

			//
			// Configure non-terminal can-match callbacks
			//

			// NOTE: Shows how to add custom code to resolve ambiguity between SimpleName/FunctionAccessExpression
			functionAccessExpression.CanMatchCallback = CanMatchFunctionAccessExpression;

			//
			// Configure non-terminal productions
			//

			this.Root.Production = functionDeclaration.ZeroOrMore();

			functionDeclaration.Production = @function + @identifier + @openParenthesis + functionParameterList.Optional() + @closeParenthesis + block;

			// NOTE: Shows how to properly handle left recursion
			functionParameterList.Production = @identifier + (@comma + @identifier).ZeroOrMore();

			statement.Production = block
				| emptyStatement
				| variableDeclarationStatement
				| assignmentStatement
				| returnStatement;

			block.Production = @openCurlyBrace + statement.ZeroOrMore() + @closeCurlyBrace;
		
			emptyStatement.Production = semiColon.ToTerm().ToProduction();

			variableDeclarationStatement.Production = @var + @identifier + @semiColon;

			assignmentStatement.Production = @identifier + @assignment + expression + @semiColon;

			returnStatement.Production = @return + expression + @semiColon;

			expression.Production = equalityExpression.ToTerm().ToProduction();

			// NOTE: Shows how to properly handle left recursion
			equalityExpression.Production = additiveExpression + ((@equality | @inequality) + equalityExpression).Optional();

			// NOTE: Shows how to properly handle left recursion
			additiveExpression.Production = multiplicativeExpression + ((@addition | @subtraction) + additiveExpression).Optional();

			// NOTE: Shows how to properly handle left recursion
			multiplicativeExpression.Production = primaryExpression + ((@multiplication | @division) + multiplicativeExpression).Optional();

			primaryExpression.Production = numberExpression
				| functionAccessExpression  // NOTE: Moved up so that its can-match callback occurs before the first set for 'simpleName' is checked
				| simpleName
				| parenthesizedExpression;

			numberExpression.Production = @number.ToTerm().ToProduction();

			simpleName.Production = @identifier.ToTerm().ToProduction();

			functionAccessExpression.Production = @identifier + @openParenthesis + functionArgumentList.Optional() + @closeParenthesis;

			// NOTE: Shows how to properly handle left recursion
			functionArgumentList.Production = expression + (@comma + expression).ZeroOrMore();

			parenthesizedExpression.Production = @openParenthesis + expression + @closeParenthesis;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
