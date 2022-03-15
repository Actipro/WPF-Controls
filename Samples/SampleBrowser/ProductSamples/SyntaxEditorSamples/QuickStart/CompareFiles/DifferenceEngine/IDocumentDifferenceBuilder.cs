namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine {

	/// <summary>
	/// Defines an object that can build a collection of differences between two documents.
	/// </summary>
	public interface IDocumentDifferenceBuilder {

		/// <summary>
		/// Compares one document to another and stores the results as <see cref="OldDifferences"/> and <see cref="NewDifferences"/>.
		/// </summary>
		/// <param name="oldText">The text for the oldest version of a document.</param>
		/// <param name="newText">The text for the lastest version of a document.</param>
		/// <param name="ignoreWhiteSpace"><c>true</c> to ignore white space.</param>
		void Compare(string oldText, string newText, bool ignoreWhiteSpace);

		/// <summary>
		/// Gets the collection of differences in the lastest document from the last compare operation.
		/// </summary>
		IDifferenceCollection NewDifferences { get; }

		/// <summary>
		/// Gets the collection of differences in the oldest document from the last compare operation.
		/// </summary>
		IDifferenceCollection OldDifferences { get; }
	}
}
