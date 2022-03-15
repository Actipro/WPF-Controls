namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine {

	/// <summary>
	/// Defines the difference between one item and another.
	/// </summary>
	public enum DifferenceKind {

		/// <summary>No difference.</summary>
		None,
		
		/// <summary>Content added.</summary>
		Added,

		/// <summary>Content modified.</summary>
		Modified,

		/// <summary>Content removed.</summary>
		Removed,

		/// <summary>An imaginary placeholder for content that does not exist.</summary>
		Imaginary,

	}

}
