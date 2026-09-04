namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;

/// <summary>
/// A collection of <see cref="IDifference"/> instances.
/// </summary>
public interface IDifferenceCollection : ICollection<IDifference> {

	/// <summary>
	/// The difference at the specified zero-based index in the collection.
	/// </summary>
	/// <param name="index">The zero-based index.</param>
	IDifference this[int index] { get; }

}
