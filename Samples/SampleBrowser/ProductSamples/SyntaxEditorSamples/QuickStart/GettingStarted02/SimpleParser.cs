using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted02 {
    
	/// <summary>
	/// Represents a <c>Simple</c> parser (syntax/semantic analyzer) implementation
	/// that scans code to build a list of all the functions that are defined.
	/// <para>
	/// A real production parser would be better designed to construct an AST of the code,
	/// but this sample is only intended to show an introduction into how a parser can 
	/// be defined and registered with a language.
	/// </para>
	/// </summary>
    public class SimpleParser : ParserBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SimpleParser</c> class.
		/// </summary>
		public SimpleParser() : base("Simple") {}
		
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
			SimpleParseData parseData = new SimpleParseData();

			//
			// NOTE: Make sure that you've set up an ambient parse request dispatcher for your application
			//   (see documentation on 'Parse Requests and Dispatchers') so that this parser is called in 
			//   a worker thread as the editor is updated
			//

			// Most parsers will use the request.TextBufferReader property in some fashion to scan through 
			//   text and not a snapshot directly... in this basic sample though, we're going to use the 
			//   tokenization provided by the snapshot's reader so we can only proceed if there is a 
			//   snapshot passed to us
			if (request.Snapshot != null) {
				ITextSnapshotReader reader = request.Snapshot.GetReader(0);

				bool isFunctionStart = false;
				while (!reader.IsAtSnapshotEnd) {
					IToken token = reader.ReadToken();
					if (token != null) {
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
}
