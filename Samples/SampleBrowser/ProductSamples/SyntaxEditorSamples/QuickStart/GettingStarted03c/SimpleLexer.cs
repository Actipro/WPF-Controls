using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Lexing.Implementation;
using MatchType = ActiproSoftware.Text.RegularExpressions.MatchType;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c;

/// <summary>
/// Represents a programmatic mergeable <c>Simple</c> lexer (lexical analyzer) implementation.
/// </summary>
public class SimpleLexer : MergeableLexerBase {

	private readonly bool _caseSensitive;
	private readonly LexicalStateCollection _lexicalStates;

	private static readonly Dictionary<string, int> _keywords = [];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="caseSensitive">Whether the language is case sensitive.</param>
	public SimpleLexer(bool caseSensitive) {
		// This is added for demo purposes... normally a language knows whether it is case sensitive or not
		_caseSensitive = caseSensitive;

		// Create ID providers
		LexicalStateIdProviderCore = new SimpleLexicalStateId();
		TokenIdProviderCore = new SimpleTokenId();

		// Initialize keywords
		if (_keywords.Count == 0) {
			for (var tokenId = TokenIdProviderCore.MinId; tokenId <= TokenIdProviderCore.MaxId; tokenId++) {
				// If the token ID is in the range of keyword IDs, add it to the keywords dictionary
				if ((tokenId >= SimpleTokenId.Function) && (tokenId <= SimpleTokenId.Var))
					_keywords.Add(TokenIdProviderCore.GetKey(tokenId)!.ToLowerInvariant(), tokenId);
			}
		}

		// Create the default lexical state
		var lexicalState = new ProgrammaticLexicalState(SimpleLexicalStateId.Default, "Default");
		_lexicalStates = new LexicalStateCollection(parentLexer: this) {
			lexicalState
		};
		DefaultLexicalStateCore = lexicalState;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<ILexicalStateTransition> GetAllLexicalStateTransitions()
		=> _lexicalStates.GetAllLexicalStateTransitions();

	/// <inheritdoc/>
	public override MergeableLexerResult GetNextToken(ITextBufferReader reader, ILexicalState lexicalState) {
		var tokenId = SimpleTokenId.Invalid;

		// Get the next character
		var ch = reader.Read();

		// If the character is a letter or digit...
		if ((char.IsLetter(ch) || (ch == '_'))) {
			// Parse the identifier
			tokenId = ParseIdentifier(reader, ch);
		}
		else if (!ch.IsLineTerminator() && char.IsWhiteSpace(ch)) {
			while (reader.Peek().IsLineTerminator() && char.IsWhiteSpace(reader.Peek()))
				reader.Read();
			tokenId = SimpleTokenId.Whitespace;
		}
		else {
			switch (ch) {
				case ',':
					tokenId = SimpleTokenId.Comma;
					break;
				case '(':
					tokenId = SimpleTokenId.OpenParenthesis;
					break;
				case ')':
					tokenId = SimpleTokenId.CloseParenthesis;
					break;
				case ';':
					tokenId = SimpleTokenId.SemiColon;
					break;
				case LineTerminators.CRChar:
				case LineTerminators.LFChar:
					// Line terminator
					tokenId = SimpleTokenId.Whitespace;
					break;
				case '{':
					tokenId = SimpleTokenId.OpenCurlyBrace;
					break;
				case '}':
					tokenId = SimpleTokenId.CloseCurlyBrace;
					break;
				case '/':
					tokenId = SimpleTokenId.Division;
					switch (reader.Peek()) {
						case '/':
							// Parse a single-line comment
							tokenId = ParseSingleLineComment(reader);
							break;
						case '*':
							// Parse a multi-line comment
							tokenId = ParseMultiLineComment(reader);
							break;
					}
					break;
				case '=':
					if (reader.Peek() == '=') {
						reader.Read();
						tokenId = SimpleTokenId.Equality;
					}
					else
						tokenId = SimpleTokenId.Assignment;
					break;
				case '!':
					if (reader.Peek() == '=') {
						reader.Read();
						tokenId = SimpleTokenId.Inequality;
					}
					break;
				case '+':
					tokenId = SimpleTokenId.Addition;
					break;
				case '-':
					tokenId = SimpleTokenId.Subtraction;
					break;
				case '*':
					tokenId = SimpleTokenId.Multiplication;
					break;
				default:
					if ((ch >= '0') && (ch <= '9')) {
						// Parse the number
						tokenId = ParseNumber(reader, ch);
					}
					break;
			}
		}

		if (tokenId != SimpleTokenId.Invalid) {
			return new MergeableLexerResult(MatchType.ExactMatch, new LexicalStateTokenData(lexicalState, tokenId));
		}
		else {
			reader.ReadReverse();
			return MergeableLexerResult.NoMatch;
		}
	}

	/// <summary>
	/// Parses an identifier.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="ch">The first character of the identifier.</param>
	/// <returns>The ID of the token that was matched.</returns>
	protected virtual int ParseIdentifier(ITextBufferReader reader, char ch) {
		// Get the entire word
		var startOffset = reader.Offset - 1;
		while (!reader.IsAtEnd) {
			var ch2 = reader.Read();
			// NOTE: This could be improved by supporting \u escape sequences
			if ((!char.IsLetterOrDigit(ch2)) && (ch2 != '_')) {
				reader.ReadReverse();
				break;
			}
		}

		// Determine if the word is a keyword
		if (char.IsLetter(ch)) {
			var subString = reader.GetSubstring(startOffset, reader.Offset - startOffset);
			if (!_caseSensitive)
				subString = subString.ToLowerInvariant();

			return _keywords.TryGetValue(subString, out var value)
				? value
				: SimpleTokenId.Identifier;
		}
		else
			return SimpleTokenId.Identifier;
	}

	/// <summary>
	/// Parses a multiple line comment.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <returns>The ID of the token that was matched.</returns>
	protected virtual int ParseMultiLineComment(ITextBufferReader reader) {
		reader.Read();
		while (reader.Offset < reader.Length) {
			if (reader.Peek() == '*') {
				if (reader.Offset + 1 < reader.Length) {
					if (reader.Peek(2) == '/') {
						reader.Read();
						reader.Read();
						break;
					}
				}
				else {
					reader.Read();
					break;
				}
			}
			reader.Read();
		}
		return SimpleTokenId.MultiLineCommentText;
	}

	/// <summary>
	/// Parses a number.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="ch">The first character of the number.</param>
	/// <returns>The ID of the token that was matched.</returns>
	protected virtual int ParseNumber(ITextBufferReader reader, char ch) {
		while (char.IsNumber(reader.Peek()))
			reader.Read();
		return SimpleTokenId.Number;
	}

	/// <summary>
	/// Parses a single line comment.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <returns>The ID of the token that was matched.</returns>
	protected virtual int ParseSingleLineComment(ITextBufferReader reader) {
		reader.Read();
		while ((!reader.IsAtEnd) && !reader.Peek().IsLineTerminator())
			reader.Read();
		return SimpleTokenId.SingleLineCommentText;
	}
}
