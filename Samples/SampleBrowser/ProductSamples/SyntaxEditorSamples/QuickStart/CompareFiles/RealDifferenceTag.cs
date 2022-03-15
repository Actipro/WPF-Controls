using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Defines a tag to represent non-imaginary differences resulting from a file comparison.
	/// </summary>
	public class RealDifferenceTag : ITag {

		/// <summary>
		/// Gets or sets if the tag is associated with the entire line.
		/// </summary>
		/// <value>
		/// <c>true</c> if the tag is for the entire line.
		/// <c>false</c> if the tag is for text within a line.
		/// </value>
		public bool IsForLine { get; set; }

		/// <summary>
		/// Gets or sets if the tag is associated with the latest version of compared content.
		/// </summary>
		/// <value>
		/// <c>true</c> if the tag is associated with the latest version.
		/// <c>false</c> if the tag is associated with the oldest version.
		/// </value>
		public bool IsLatest { get; set; }

		/// <summary>
		/// Gets or sets the kind of difference represented by the tag.
		/// </summary>
		public DifferenceKind Kind { get; set; }

	}

}
