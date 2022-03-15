namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine {

	/// <summary>
	/// Tracks a difference between a source and destination.
	/// </summary>
	public interface IDifference {

		/// <summary>
		/// Gets a collection of child differences (e.g. characters differences within a line).
		/// </summary>
		IDifferenceCollection Children { get; }

		/// <summary>
		/// Gets the kind of difference.
		/// </summary>
		DifferenceKind Kind { get; }

		/// <summary>
		/// Gets the length of the difference.
		/// </summary>
		int Length { get; }

		/// <summary>
		/// The zero-based position of the difference within a logical group (e.g. line index within a file or character index on a line).
		/// </summary>
		/// <remarks>
		/// The value is <c>null</c> for <see cref="DifferenceKind.Imaginary"/> differences.
		/// </remarks>
		int? Position { get; }

	}

}
