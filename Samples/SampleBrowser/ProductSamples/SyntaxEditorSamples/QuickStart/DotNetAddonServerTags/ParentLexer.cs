using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Lexing.Implementation;
using MatchType = ActiproSoftware.Text.RegularExpressions.MatchType;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Represents a <c>Parent</c> lexer (lexical analyzer) implementation.
/// </summary>
internal class ParentLexer : MergeableLexerBase {

	private readonly LexicalStateCollection _lexicalStates;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="childLanguage">The child language.</param>
	public ParentLexer(ISyntaxLanguage childLanguage) {
		// Initialize lexical states
		_lexicalStates = new LexicalStateCollection(this);

		var defaultState = new ProgrammaticLexicalState(ParentLexicalStateId.Default, "Default");
		_lexicalStates.Add(defaultState);
		DefaultLexicalStateCore = defaultState;

		var childOutputBlockTransitionState = new ProgrammaticLexicalState(ParentLexicalStateId.ChildOutputBlock, "Child output block transition");
		childOutputBlockTransitionState.LexicalScopes.Add(new ProgrammaticLexicalScope(
			new ProgrammaticLexicalScopeMatch(IsChildOutputBlockTransitionStateScopeStart),
			new ProgrammaticLexicalScopeMatch(IsChildOutputBlockTransitionStateScopeEnd)
		));
		_lexicalStates.Add(childOutputBlockTransitionState);
		defaultState.ChildLexicalStates.Add(childOutputBlockTransitionState);

		var childCodeBlockTransitionState = new ProgrammaticLexicalState(ParentLexicalStateId.ChildCodeBlock, "Child code block transition");
		childCodeBlockTransitionState.LexicalScopes.Add(new ProgrammaticLexicalScope(
			new ProgrammaticLexicalScopeMatch(IsChildCodeBlockTransitionStateScopeStart),
			new ProgrammaticLexicalScopeMatch(IsChildCodeBlockTransitionStateScopeEnd)
		));
		_lexicalStates.Add(childCodeBlockTransitionState);
		defaultState.ChildLexicalStates.Add(childCodeBlockTransitionState);

		if (childLanguage.GetLexer() is IMergeableLexer { DefaultLexicalState: { } childLexicalState }) {
			childOutputBlockTransitionState.Transition = new LexicalStateTransition(childLanguage, childLexicalState, childLexicalScope: null);
			childCodeBlockTransitionState.Transition = new LexicalStateTransition(childLanguage, childLexicalState, childLexicalScope: null);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Represents the method that will handle token matching callbacks.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
	/// <returns>A <see cref="MergeableLexerResult"/> indicating the lexer result.</returns>
	private MergeableLexerResult IsChildCodeBlockTransitionStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope) {
		if (reader.Peek() == '%') {
			reader.Read();
			if (reader.Peek() == '>') {
				reader.Read();
				return new MergeableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildCodeBlockEnd));
			}
			reader.ReadReverse();
		}
		return MergeableLexerResult.NoMatch;
	}

	/// <summary>
	/// Represents the method that will handle token matching callbacks.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
	/// <returns>A <see cref="MergeableLexerResult"/> indicating the lexer result.</returns>
	private MergeableLexerResult IsChildCodeBlockTransitionStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope) {
		if (reader.Peek() == '<') {
			reader.Read();
			if (reader.Peek() == '%') {
				reader.Read();
				return new MergeableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildCodeBlockStart));
			}
			reader.ReadReverse();
		}
		return MergeableLexerResult.NoMatch;
	}

	/// <summary>
	/// Represents the method that will handle token matching callbacks.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
	/// <returns>A <see cref="MergeableLexerResult"/> indicating the lexer result.</returns>
	private MergeableLexerResult IsChildOutputBlockTransitionStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope) {
		if (reader.Peek() == '%') {
			reader.Read();
			if (reader.Peek() == '>') {
				reader.Read();
				return new MergeableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildOutputBlockEnd));
			}
			reader.ReadReverse();
		}
		return MergeableLexerResult.NoMatch;
	}

	/// <summary>
	/// Represents the method that will handle token matching callbacks.
	/// </summary>
	/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
	/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
	/// <returns>A <see cref="MergeableLexerResult"/> indicating the lexer result.</returns>
	private MergeableLexerResult IsChildOutputBlockTransitionStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope) {
		if (reader.Peek() == '<') {
			reader.Read();
			if (reader.Peek() == '%') {
				reader.Read();
				if (reader.Peek() == '=') {
					reader.Read();
					return new MergeableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildOutputBlockStart));
				}
				reader.ReadReverse();
			}
			reader.ReadReverse();
		}
		return MergeableLexerResult.NoMatch;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<ILexicalStateTransition> GetAllLexicalStateTransitions()
		=> _lexicalStates.GetAllLexicalStateTransitions();

	/// <inheritdoc/>
	public override MergeableLexerResult GetNextToken(ITextBufferReader reader, ILexicalState lexicalState) {
		// Initialize
		int tokenId = ParentTokenId.Invalid;

		// Get the next character
		char ch = reader.Read();

		switch (lexicalState.Id) {
			case ParentLexicalStateId.Default: {
				// If the character is a letter or digit...
				if ((char.IsLetter(ch) || (ch == '_'))) {
					// Parse the identifier
					tokenId = ParseIdentifier(reader, ch);
				}
				else if (char.IsWhiteSpace(ch)) {
					// Consume sequential whitespace
					while (char.IsWhiteSpace(reader.Peek()))
						reader.Read();
					tokenId = ParentTokenId.Whitespace;
				}
				else {
					// Invalid
					tokenId = ParentTokenId.Invalid;
				}
				break;
			}
		}

		if (tokenId != ParentTokenId.Invalid) {
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
		int startOffset = reader.Offset - 1;
		while (!reader.IsAtEnd) {
			char ch2 = reader.Read();
			if ((!char.IsLetterOrDigit(ch2)) && (ch2 != '_')) {
				reader.ReadReverse();
				break;
			}
		}

		// This language only has one keyword named "date"
		if (reader.GetSubstring(startOffset, reader.Offset - startOffset) == "date")
			return ParentTokenId.DateKeyword;

		// Word is an identifier
		return ParentTokenId.Identifier;
	}

}
