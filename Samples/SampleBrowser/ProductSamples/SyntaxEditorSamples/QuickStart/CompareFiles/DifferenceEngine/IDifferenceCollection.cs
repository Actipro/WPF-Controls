using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine {

	/// <summary>
	/// A collection of <see cref="IDifference"/> instances.
	/// </summary>
	public interface IDifferenceCollection : ICollection<IDifference> {

		/// <summary>
		/// Gets the difference at the specified zero-based index in the collection.
		/// </summary>
		/// <param name="index">The zero-baesd index.</param>
		/// <returns>The <see cref="IDifference"/> at the given <paramref name="index"/>.</returns>
		IDifference this[int index] { get; }

	}
}
