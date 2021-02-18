using System;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased {

	/// <summary>
	/// Represents a <c>Javascript</c> parser that performs code outlining.
	/// </summary>
	public class JavascriptOutliningParser : ParserBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>JavascriptOutliningParser</c> class.
		/// </summary>
		public JavascriptOutliningParser() : base("OutliningParser") {}
		
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
			if (request.Snapshot != null) {
				// Since the parser may be delayed a bit before this is called (due to threading), 
				//   base the outlining on the most current snapshot for the document, which may be
				//   newer than the one passed in the parse request
				return new JavascriptOutliningSource(request.Snapshot.Document.CurrentSnapshot);
			}

			return null;
		}
	}

}

