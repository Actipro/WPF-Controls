using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted02;

/// <summary>
/// Represents a <c>Simple</c> parser (syntax/semantic analyzer) implementation
/// that scans code to build a list of all the functions that are defined.
/// <para>
/// A real production parser would be better designed to construct an AST of the code,
/// but this sample is only intended to show an introduction into how a parser can
/// be defined and registered with a language.
/// </para>
/// </summary>
public class SimpleParser() : ParserBase("Simple") {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IParseData Parse(IParseRequest request) {
		var parseData = new SimpleParseData();

		//
		// NOTE: Make sure that you've set up an ambient parse request dispatcher for your application
		//   (see documentation on 'Parse Requests and Dispatchers') so that this parser is called in
		//   a worker thread as the editor is updated
		//

		// Most parsers will use the request.TextBufferReader property in some fashion to scan through
		//   text and not a snapshot directly... in this basic sample though, we're going to use the
		//   tokenization provided by the snapshot's reader so we can only proceed if there is a
		//   snapshot passed to us
		if (request.Snapshot?.GetReader(0) is { } reader) {
			var isFunctionStart = false;
			while (!reader.IsAtSnapshotEnd) {
				var token = reader.ReadToken();
				if (token is not null) {
					switch (token.Key) {
						case "Keyword":
							// If a function token, mark that this is a function start... the next identifier should be the function name
							isFunctionStart = (reader.Snapshot.GetSubstring(token.TextRange) == "function");
							break;
						case "Identifier":
							// If this is the function name...
							if (isFunctionStart) {
								parseData.Functions.Add(new TextSnapshotRange(reader.Snapshot, token.TextRange));
								isFunctionStart = false;
							}
							break;
						case "Whitespace":
							// Ignore
							break;
						default:
							// Flag as no longer in a function start
							isFunctionStart = false;
							break;
					}
				}
			}
		}

		return parseData;
	}

}
