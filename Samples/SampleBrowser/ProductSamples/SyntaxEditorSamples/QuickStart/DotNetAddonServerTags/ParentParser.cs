using System;
using System.Collections.Generic;
using System.Text;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Represents a <c>Parent</c> parser implementation.
	/// </summary>
	internal class ParentParser : ParserBase {

		private ISyntaxLanguage childLanguage;
		private ILLParser		childParser;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ParentParser</c> class.
		/// </summary>
		/// <param name="childLanguage">The child language.</param>
		public ParentParser(ISyntaxLanguage childLanguage) : base("Parent") {
			if (childLanguage == null)
				throw new ArgumentNullException("childLanguage");

			// Initialize and pull out the parser (so it doesn't automatically get called)
			this.childLanguage = childLanguage;
			this.childParser = childLanguage.GetParser() as ILLParser;
			if (this.childParser == null)
				throw new ArgumentException("No ILLParser service was found.", "childLanguage");

			childLanguage.UnregisterParser();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Performs a parsing operation using the parameters specified in the supplied <see cref="IParseRequest"/>
		/// and returns the resulting parse data.
		/// </summary>
		/// <param name="request">The <see cref="IParseRequest"/> that contains data about the requested parsing operation.</param>
		/// <returns>An <see cref="IParseData"/> that is the result of the parsing operation.</returns>
		/// <remarks>
		/// A <see cref="IParseRequestDispatcher"/> typically calls this method when a queued parse request is ready to be processed.
		/// </remarks>
		public override IParseData Parse(IParseRequest request) {
			if (request == null)
				throw new ArgumentNullException("request");

			// Create parse data
			ParentParseData parseData = new ParentParseData();
			parseData.Snapshot = request.Snapshot;
			
			// Initialize generated text
			StringBuilder generatedText = new StringBuilder();
			generatedText.Append("using System;\n");
			generatedText.Append("using System.Collections.Generic;\n\n");
			generatedText.Append("using System.Linq;\n\n");
			generatedText.Append("[EditorBrowsable(EditorBrowsableState.Never)]\n");
			generatedText.Append("class __Generated {\n");
			generatedText.Append("\t[EditorBrowsable(EditorBrowsableState.Never)]\n");
			generatedText.Append("\tvoid __WriteOutput() {\n");

			ITextSnapshotReader sourceReader = request.Snapshot.GetReader(0);
			int lastDelimiterOffset = 0;
			bool lastDelimiterWasStart = false;
			while (!sourceReader.IsAtSnapshotEnd) {
				IToken token = sourceReader.ReadToken();
				if (token != null) {
					switch (token.Id) {
						case ParentTokenId.ChildCodeBlockStart:
						case ParentTokenId.ChildOutputBlockStart:
							if (token.StartOffset - lastDelimiterOffset > 0) {
								// Append generated text
								string text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset), LineTerminator.Newline);
								generatedText.Append("\t\tResponse.Write(@\"");
								generatedText.Append(text.Replace("\"", "\"\""));
								generatedText.Append("\");\n");
							}

							// Store the last delimiter offset
							lastDelimiterOffset = token.EndOffset;
							lastDelimiterWasStart = true;
							break;
						case ParentTokenId.ChildCodeBlockEnd:
							if ((lastDelimiterWasStart) && (token.StartOffset - lastDelimiterOffset > 0)) {
								// Get the text between the delimiters
								string text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset), LineTerminator.Newline);
								generatedText.Append("\t\t");

								// Add a mapping
								parseData.TextRangeMappings.Add(Tuple.Create(new TextRange(lastDelimiterOffset, token.StartOffset), TextRange.FromSpan(generatedText.Length, text.Length)));

								// Append the text directly
								generatedText.Append(text);
								generatedText.Append("\n");
							}

							// Store the last delimiter offset
							lastDelimiterOffset = token.EndOffset;
							lastDelimiterWasStart = false;
							break;
						case ParentTokenId.ChildOutputBlockEnd:
							if ((lastDelimiterWasStart) && (token.StartOffset - lastDelimiterOffset > 0)) {
								// Get the text between the delimiters and append a Response.Write
								string text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset), LineTerminator.Newline);
								generatedText.Append("\t\tResponse.Write(");

								// Add a mapping
								parseData.TextRangeMappings.Add(Tuple.Create(new TextRange(lastDelimiterOffset, token.StartOffset), TextRange.FromSpan(generatedText.Length, text.Length)));

								// Append the text directly
								generatedText.Append(text);
								generatedText.Append(");\n");
							}

							// Store the last delimiter offset
							lastDelimiterOffset = token.EndOffset;
							lastDelimiterWasStart = false;
							break;
					}
				}
			}

			if (lastDelimiterOffset < sourceReader.Snapshot.Length) {
				// Append generated text
				string text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, sourceReader.Snapshot.Length), LineTerminator.Newline);
				generatedText.Append("\t\tResponse.Write(@\"");
				generatedText.Append(text.Replace("\"", "\"\""));
				generatedText.Append("\");\n");
			}

			// Store the generated text
			generatedText.Append("\t}\n");
			generatedText.Append("}\n");

			// Get parse data for the translated code
			CodeDocument generatedDocument = new CodeDocument();
			generatedDocument.Language = childLanguage;
			generatedDocument.SetText(generatedText.ToString());

			// Get a reader
			ITextBufferReader generatedReader = generatedDocument.CurrentSnapshot.GetReader(0).BufferReader;

			// Create a request
			ParseRequest generatedRequest = new ParseRequest(Guid.NewGuid().ToString(), generatedReader, childParser, generatedDocument);
			generatedRequest.Snapshot = generatedDocument.CurrentSnapshot;

			// Parse
			generatedDocument.ParseData = childParser.Parse(generatedRequest);
			parseData.GeneratedParseData = generatedDocument.ParseData as ILLParseData;

			return parseData;
		}

	}

}
