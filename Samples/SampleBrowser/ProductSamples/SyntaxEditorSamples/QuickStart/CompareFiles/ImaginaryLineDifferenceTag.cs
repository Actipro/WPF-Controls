using ActiproSoftware.Text.Tagging;
using System;

#if WINFORMS
using ScreenUnits = System.Int32;
#elif WPF
using ScreenUnits = System.Double;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Defines an intra-line spacer to represent one or more imaginary lines in a file comparison (e.g. lines that
	/// do not exist in the file and must have space reserved to allow for side-by-side views to stay in sync).
	/// </summary>
	public class ImaginaryLineDifferenceTag : IIntraLineSpacerTag {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the total margin that must be reserved for the imaginary lines.
		/// </summary>
		private ScreenUnits Margin => LineCount * DefaultLineHeight;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IIntraLineSpacerTag.BottomMargin"/>
		public ScreenUnits BottomMargin => IsBottomPosition ? Margin : default(ScreenUnits);

		/// <summary>
		/// Gets or sets the default height of a line.
		/// </summary>
		public ScreenUnits DefaultLineHeight { get; set; }

		/// <summary>
		/// <summary>
		/// Gets or sets if the tag should be aligned below the line or above the line.
		/// </summary>
		/// <value>
		/// <c>true</c> to show the tag below the line.
		/// <c>false</c> to show the tag above the line.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsBottomPosition { get; set; } = true;

		/// <inheritdoc cref="IIntraLineSpacerTag.Key"/>
		public object Key { get; } = Guid.NewGuid();

		/// Gets or sets the number of imaginary lines represented by the tag.
		/// </summary>
		public int LineCount { get; set; }

		/// <inheritdoc cref="IIntraLineSpacerTag.TopMargin"/>
		public ScreenUnits TopMargin => IsBottomPosition ? default(ScreenUnits) : Margin;

	}
}
