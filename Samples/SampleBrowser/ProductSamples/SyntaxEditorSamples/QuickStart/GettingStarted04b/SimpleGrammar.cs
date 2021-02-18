using System;
using System.Globalization;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04b {

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

			functionAccessExpression.CanMatchCallback = CanMatchFunctionAccessExpression;

			//
			// Configure non-terminal productions
			//

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
