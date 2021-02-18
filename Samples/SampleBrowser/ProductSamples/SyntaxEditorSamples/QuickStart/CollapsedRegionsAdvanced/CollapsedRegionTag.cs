using System;
using System.Windows;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced {
    
	/// <summary>
	/// Provides an <see cref="ICollapsedRegionTag"/> implementation that controls collapsed regions.
	/// </summary>
	public class CollapsedRegionTag : ICollapsedRegionTag, IIntraTextSpacerTag {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Gets the baseline of the spacer, which is the distance from the top of the spacer to the text baseline.
		/// </summary>
		/// <value>The baseline of the spacer, which is the distance from the top of the spacer to the text baseline.</value>
		/// <remarks>
		/// This property can return the height of the spacer to sit the spacer on top of the text baseline.
		/// </remarks>
		public double Baseline { get; set; }

		/// <summary>
		/// Gets or sets an object that can be used to uniquely identify the spacer.
		/// </summary>
		/// <value>An object that can be used to uniquely identify the spacer.</value>
		public object Key { get; set; }
	
		/// <summary>
		/// Gets the size of the spacer.
		/// </summary>
		/// <value>The size of the spacer.</value>
		public Size Size { get; set; }

		/// <summary>
		/// Gets or sets the text to display.
		/// </summary>
		/// <value>The text to display.</value>
		public string Text { get; set; }

		/// <summary>
		/// Creates an <see cref="IIntraTextSpacerTag"/>-based tag snapshot range for this tag.
		/// </summary>
		/// <param name="snapshotRange">The <see cref="TextSnapshotRange"/> for the tag.</param>
		/// <returns>The <see cref="IIntraTextSpacerTag"/>-based tag snapshot range for this tag that was created.</returns>
		public TagSnapshotRange<IIntraTextSpacerTag> ToIntraTextSpacerTagRange(TextSnapshotRange snapshotRange) {
			return new TagSnapshotRange<IIntraTextSpacerTag>(snapshotRange, this);
		}

	}
	
}
