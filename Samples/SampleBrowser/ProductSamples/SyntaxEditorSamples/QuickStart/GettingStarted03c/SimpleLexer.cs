using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Lexing.Implementation;
using ActiproSoftware.Text.RegularExpressions;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c {

	/// <summary>
	/// Represents a programmatic mergable <c>Simple</c> lexer (lexical analyzer) implementation.
	/// </summary>
	public class SimpleLexer : MergableLexerBase {

		private bool caseSensitive;
		private LexicalStateCollection lexicalStates;

		private static Dictionary<string, int> keywords = new Dictionary<string, int>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SimpleLexer</c> class.
		/// </summary>
		/// <param name="caseSensitive">Whether the language is case sensitive.</param>
		public SimpleLexer(bool caseSensitive) {
			// This is added for demo purposes... normally a language knows whether it is case sensitive or not
			this.caseSensitive = caseSensitive;

			// Create ID providers
			this.LexicalStateIdProviderCore = new SimpleLexicalStateId();
			this.TokenIdProviderCore = new SimpleTokenId();

			// Initialize keywords
			if (keywords.Count == 0) {
				for (int tokenId = this.TokenIdProviderCore.MinId; tokenId <= this.TokenIdProviderCore.MaxId; tokenId++) {
					// If the token ID is in the range of keyword IDs, add it to the keywords dictionary
					if ((tokenId >= SimpleTokenId.Function) && (tokenId <= SimpleTokenId.Var))
						keywords.Add(this.TokenIdProviderCore.GetKey(tokenId).ToLowerInvariant(), tokenId);
				}
			}

			// Create the default lexical state
			ProgrammaticLexicalState lexicalState = new ProgrammaticLexicalState(SimpleLexicalStateId.Default, "Default");
			lexicalStates = new LexicalStateCollection(this);
			lexicalStates.Add(lexicalState);
			this.DefaultLexicalStateCore = lexicalState;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns a collection containing all <see cref="ILexicalStateTransition"/> objects within the lexer.
		/// </summary>
		/// <returns>A collection containing all <see cref="ILexicalStateTransition"/> objects within the lexer.</returns>
		/// <remarks>
		/// This method allows consumers to see which language transitions can be made within the lexer.
		/// </remarks>
		public override IEnumerable<ILexicalStateTransition> GetAllLexicalStateTransitions() {
			return lexicalStates.GetAllLexicalStateTransitions();
		}

		/// <summary>
		/// Performs a lex to return the next <see cref="MergableLexerResult"/> 
		/// from a <see cref="ITextBufferReader"/> and seeks past it if there is a match.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that specifies the current state.</param>
		/// <returns>A <see cref="MergableLexerResult"/> indicating the lexer result.</returns>
		public override MergableLexerResult GetNextToken(ITextBufferReader reader, ILexicalState lexicalState) {
			// Initialize
			int tokenId = SimpleTokenId.Invalid;

			// Get the next character
			char ch = reader.Read();

			// If the character is a letter or digit...
			if ((Char.IsLetter(ch) || (ch == '_'))) {
				// Parse the identifier
				tokenId = this.ParseIdentifier(reader, ch);
			}
			else if ((ch != '\n') && (Char.IsWhiteSpace(ch))) {
				while ((reader.Peek() != '\n') && (Char.IsWhiteSpace(reader.Peek())))
					reader.Read();
				tokenId = SimpleTokenId.Whitespace;
			}
			else {
				tokenId = SimpleTokenId.Invalid;
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
					case '\n':
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
								tokenId = this.ParseSingleLineComment(reader);
								break;
							case '*':
								// Parse a multi-line comment
								tokenId = this.ParseMultiLineComment(reader);
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
							tokenId = this.ParseNumber(reader, ch);
						}
						break;
				}
			}

			if (tokenId != SimpleTokenId.Invalid) {
				return new MergableLexerResult(MatchType.ExactMatch, new LexicalStateTokenData(lexicalState, tokenId));
			}
			else {
				reader.ReadReverse();
				return MergableLexerResult.NoMatch;
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
			int startOffset = reader.Offset - 1;
			while (!reader.IsAtEnd) {
				char ch2 = reader.Read();
				// NOTE: This could be improved by supporting \u escape sequences
				if ((!char.IsLetterOrDigit(ch2)) && (ch2 != '_')) {
					reader.ReadReverse();
					break;
				}
			}

			// Determine if the word is a keyword
			if (Char.IsLetter(ch)) {
				int value;
				String subString = reader.GetSubstring(startOffset, reader.Offset - startOffset);
				if (!caseSensitive)
					subString = subString.ToLowerInvariant();

				return keywords.TryGetValue(subString, out value) ? value : SimpleTokenId.Identifier;
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
			while (Char.IsNumber(reader.Peek()))
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
			while ((!reader.IsAtEnd) && (reader.Peek() != '\n'))
				reader.Read();
			return SimpleTokenId.SingleLineCommentText;
		}
	}
}
