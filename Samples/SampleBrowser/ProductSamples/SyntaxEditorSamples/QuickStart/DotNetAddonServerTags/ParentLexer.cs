using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Lexing.Implementation;
using ActiproSoftware.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Represents a <c>Parent</c> lexer (lexical analyzer) implementation.
	/// </summary>
	internal class ParentLexer : MergableLexerBase {

		private LexicalStateCollection	lexicalStates;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ParentLexer</c> class.
		/// </summary>
		/// <param name="childLanguage">The child language.</param>
		public ParentLexer(ISyntaxLanguage childLanguage) {
			// Initialize lexical states
			lexicalStates = new LexicalStateCollection(this);

			ProgrammaticLexicalState defaultState = new ProgrammaticLexicalState(ParentLexicalStateId.Default, "Default");
			lexicalStates.Add(defaultState);
			this.DefaultLexicalStateCore = defaultState;

			ProgrammaticLexicalState childOutputBlockTransitionState = new ProgrammaticLexicalState(ParentLexicalStateId.ChildOutputBlock, "Child output block transition");
			childOutputBlockTransitionState.LexicalScopes.Add(new ProgrammaticLexicalScope(new ProgrammaticLexicalScopeMatch(this.IsChildOutputBlockTransitionStateScopeStart), 
				new ProgrammaticLexicalScopeMatch(this.IsChildOutputBlockTransitionStateScopeEnd)));
			lexicalStates.Add(childOutputBlockTransitionState);
			defaultState.ChildLexicalStates.Add(childOutputBlockTransitionState);

			ProgrammaticLexicalState childCodeBlockTransitionState = new ProgrammaticLexicalState(ParentLexicalStateId.ChildCodeBlock, "Child code block transition");
			childCodeBlockTransitionState.LexicalScopes.Add(new ProgrammaticLexicalScope(new ProgrammaticLexicalScopeMatch(this.IsChildCodeBlockTransitionStateScopeStart), 
				new ProgrammaticLexicalScopeMatch(this.IsChildCodeBlockTransitionStateScopeEnd)));
			lexicalStates.Add(childCodeBlockTransitionState);
			defaultState.ChildLexicalStates.Add(childCodeBlockTransitionState);

			IMergableLexer lexer = childLanguage.GetLexer() as IMergableLexer;
			if (lexer != null) {
				childOutputBlockTransitionState.Transition = new LexicalStateTransition(childLanguage, lexer.DefaultLexicalState, null);
				childCodeBlockTransitionState.Transition = new LexicalStateTransition(childLanguage, lexer.DefaultLexicalState, null);
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Represents the method that will handle token matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <returns>A <see cref="MergableLexerResult"/> indicating the lexer result.</returns>
		private MergableLexerResult IsChildCodeBlockTransitionStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope) {
			if (reader.Peek() == '%') {
				reader.Read();
				if (reader.Peek() == '>') {
					reader.Read();
					return new MergableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildCodeBlockEnd));
				}
				reader.ReadReverse();
			}
			return MergableLexerResult.NoMatch;
		}

		/// <summary>
		/// Represents the method that will handle token matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <returns>A <see cref="MergableLexerResult"/> indicating the lexer result.</returns>
		private MergableLexerResult IsChildCodeBlockTransitionStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope) {
			if (reader.Peek() == '<') {
				reader.Read();
				if (reader.Peek() == '%') {
					reader.Read();
					return new MergableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildCodeBlockStart));
				}
				reader.ReadReverse();
			}
			return MergableLexerResult.NoMatch;
		}
			
		/// <summary>
		/// Represents the method that will handle token matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <returns>A <see cref="MergableLexerResult"/> indicating the lexer result.</returns>
		private MergableLexerResult IsChildOutputBlockTransitionStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope) {
			if (reader.Peek() == '%') {
				reader.Read();
				if (reader.Peek() == '>') {
					reader.Read();
					return new MergableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildOutputBlockEnd));
				}
				reader.ReadReverse();
			}
			return MergableLexerResult.NoMatch;
		}

		/// <summary>
		/// Represents the method that will handle token matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <returns>A <see cref="MergableLexerResult"/> indicating the lexer result.</returns>
		private MergableLexerResult IsChildOutputBlockTransitionStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope) {
			if (reader.Peek() == '<') {
				reader.Read();
				if (reader.Peek() == '%') {
					reader.Read();
					if (reader.Peek() == '=') {
						reader.Read();
						return new MergableLexerResult(MatchType.ExactMatch, new LexicalScopeTokenData(lexicalScope, ParentTokenId.ChildOutputBlockStart));
					}
					reader.ReadReverse();
				}
				reader.ReadReverse();
			}
			return MergableLexerResult.NoMatch;
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
			int tokenId = ParentTokenId.Invalid;

			// Get the next character
			char ch = reader.Read();

			switch (lexicalState.Id) {
				case ParentLexicalStateId.Default: {
					// If the character is a letter or digit...
					if ((Char.IsLetter(ch) || (ch == '_'))) {
						// Parse the identifier
						tokenId = this.ParseIdentifier(reader, ch);
					}
					else if (Char.IsWhiteSpace(ch)) {
						// Consume sequential whitespace
						while (Char.IsWhiteSpace(reader.Peek())) 
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



}
