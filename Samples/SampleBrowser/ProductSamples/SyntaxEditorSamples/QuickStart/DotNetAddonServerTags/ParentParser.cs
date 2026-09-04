using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Represents a <c>Parent</c> parser implementation.
/// </summary>
internal class ParentParser : ParserBase {

	private readonly ISyntaxLanguage _childLanguage;
	private readonly ILLParser _childParser;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="childLanguage">The child language.</param>
	public ParentParser(ISyntaxLanguage childLanguage) : base("Parent") {
		#if NET
		ArgumentNullException.ThrowIfNull(childLanguage);
		#else
		if (childLanguage is null)
			throw new ArgumentNullException(nameof(childLanguage));
		#endif

		// Initialize and pull out the parser (so it doesn't automatically get called)
		_childLanguage = childLanguage;
		_childParser = childLanguage.GetParser() as ILLParser
			?? throw new ArgumentException("No ILLParser service was found.", nameof(childLanguage));

		childLanguage.UnregisterParser();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IParseData Parse(IParseRequest request) {
		#if NET
		ArgumentNullException.ThrowIfNull(request);
		#else
		if (request is null)
			throw new ArgumentNullException(nameof(request));
		#endif
		if (request.Snapshot is not { } snapshot)
			throw new ArgumentException("The request must define a snapshot.");

		// Create parse data
		var parseData = new ParentParseData(snapshot);

		// Initialize generated text
		var generatedText = new StringBuilder()
			.AppendLine("using System;")
			.AppendLine("using System.Collections.Generic;")
			.AppendLine("using System.Linq;")
			.AppendLine()
			.AppendLine("[EditorBrowsable(EditorBrowsableState.Never)]")
			.AppendLine("class __Generated {")
			.AppendLine("\t[EditorBrowsable(EditorBrowsableState.Never)]")
			.AppendLine("\tvoid __WriteOutput() {");

		var sourceReader = snapshot.GetReader(0);
		var lastDelimiterOffset = 0;
		var lastDelimiterWasStart = false;
		while (!sourceReader.IsAtSnapshotEnd) {
			var token = sourceReader.ReadToken();
			if (token is not null) {
				switch (token.Id) {
					case ParentTokenId.ChildCodeBlockStart:
					case ParentTokenId.ChildOutputBlockStart:
						if (token.StartOffset - lastDelimiterOffset > 0) {
							// Append generated text
							var text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset));
							generatedText.Append("\t\tResponse.Write(@\"").Append(text.Replace("\"", "\"\"")).AppendLine("\");");
						}

						// Store the last delimiter offset
						lastDelimiterOffset = token.EndOffset;
						lastDelimiterWasStart = true;
						break;
					case ParentTokenId.ChildCodeBlockEnd:
						if (lastDelimiterWasStart && (token.StartOffset - lastDelimiterOffset > 0)) {
							// Get the text between the delimiters
							var text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset));
							generatedText.Append("\t\t");

							// Add a mapping
							parseData.TextRangeMappings.Add(Tuple.Create(new TextRange(lastDelimiterOffset, token.StartOffset), TextRange.FromSpan(generatedText.Length, text.Length)));

							// Append the text directly
							generatedText.Append(text).AppendLine();
						}

						// Store the last delimiter offset
						lastDelimiterOffset = token.EndOffset;
						lastDelimiterWasStart = false;
						break;
					case ParentTokenId.ChildOutputBlockEnd:
						if (lastDelimiterWasStart && (token.StartOffset - lastDelimiterOffset > 0)) {
							// Get the text between the delimiters and append a Response.Write
							var text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, token.StartOffset));
							generatedText.Append("\t\tResponse.Write(");

							// Add a mapping
							parseData.TextRangeMappings.Add(Tuple.Create(new TextRange(lastDelimiterOffset, token.StartOffset), TextRange.FromSpan(generatedText.Length, text.Length)));

							// Append the text directly
							generatedText.Append(text).AppendLine(");");
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
			var text = sourceReader.Snapshot.GetSubstring(new TextRange(lastDelimiterOffset, sourceReader.Snapshot.Length));
			generatedText.Append("\t\tResponse.Write(@\"").Append(text.Replace("\"", "\"\"")).AppendLine("\");");
		}

		// Store the generated text
		generatedText.AppendLine("\t}");
		generatedText.AppendLine("}");

		// Get parse data for the translated code
		var generatedDocument = new CodeDocument {
			Language = _childLanguage
		};
		generatedDocument.SetText(generatedText.ToString());

		// Get a reader
		var generatedReader = generatedDocument.CurrentSnapshot.GetReader(0).BufferReader;

		// Create a request
		var generatedRequest = new ParseRequest(Guid.NewGuid().ToString(), generatedReader, _childParser, generatedDocument) {
			Snapshot = generatedDocument.CurrentSnapshot
		};

		// Parse
		generatedDocument.ParseData = _childParser.Parse(generatedRequest);
		parseData.GeneratedParseData = generatedDocument.ParseData as ILLParseData;

		return parseData;
	}

}
