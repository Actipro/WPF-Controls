using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Parsing;
using System;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions {

	/// <summary>
	/// Implements a custom parser that wraps the default parser and returns additional parse data results.
	/// </summary>
	public class CustomParser : IParser {

		private IParser wrappedParser;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>CustomParser</c> class.
		/// </summary>
		public CustomParser(IParser parser) {
			if (parser == null)
				throw new ArgumentNullException(nameof(parser));

			this.wrappedParser = parser;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the string-based key that identifies the object.
		/// </summary>
		/// <value>The string-based key that identifies the object.</value>
		public string Key {
			get {
				return "Custom";
			}
		}

		/// <summary>
		/// Performs a parsing operation using the parameters specified in the supplied <see cref="IParseRequest"/>
		/// and returns the resulting parse data.
		/// </summary>
		/// <param name="request">The <see cref="IParseRequest"/> that contains data about the requested parsing operation.</param>
		/// <returns>An <see cref="IParseData"/> that is the result of the parsing operation.</returns>
		/// <remarks>
		/// A <see cref="IParseRequestDispatcher"/> typically calls this method when a queued parse request is ready to be processed.
		/// </remarks>
		public IParseData Parse(IParseRequest request) {
			var wrappedParseData = wrappedParser.Parse(request) as IDotNetParseData;
			if (wrappedParseData != null) {
				var parseData = new CustomDotNetParseData(wrappedParseData);

				// NOTE: Normally you would place code here that inspects the parsed snapshot text or its AST and determines unused regions...
				//   However this sample is showing off the visual feature itself so the text ranges use hard-coded offset values below

				parseData.UnusedRanges = new NormalizedTextSnapshotRangeCollection(new TextSnapshotRange[] {
					new TextSnapshotRange(wrappedParseData.Snapshot, 14, 47),
					new TextSnapshotRange(wrappedParseData.Snapshot, 454, 473),
					new TextSnapshotRange(wrappedParseData.Snapshot, 476, 502)
				});

				return parseData;
			}

			return null;
		}

	}

}
