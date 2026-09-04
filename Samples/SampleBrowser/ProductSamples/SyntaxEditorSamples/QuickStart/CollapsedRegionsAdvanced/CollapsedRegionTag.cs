using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced;

/// <summary>
/// Provides an <see cref="ICollapsedRegionTag"/> implementation that controls collapsed regions.
/// </summary>
public class CollapsedRegionTag : ICollapsedRegionTag, IIntraTextSpacerTag {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IIntraTextSpacerTag.Baseline"/>
	public double Baseline { get; set; }

	/// <inheritdoc cref="IIntraTextSpacerTag.IsSpacerBefore"/>
	public bool IsSpacerBefore
		=> true;

	/// <summary>
	/// An object that can be used to uniquely identify the spacer.
	/// </summary>
	public object? Key { get; set; }

	/// <inheritdoc cref="IIntraTextSpacerTag.Size"/>
	public Size Size { get; set; }

	/// <summary>
	/// The text to display.
	/// </summary>
	public string? Text { get; set; }

	/// <summary>
	/// Creates an <see cref="IIntraTextSpacerTag"/>-based tag snapshot range for this tag.
	/// </summary>
	/// <param name="snapshotRange">The <see cref="TextSnapshotRange"/> for the tag.</param>
	public TagSnapshotRange<IIntraTextSpacerTag> ToIntraTextSpacerTagRange(TextSnapshotRange snapshotRange)
		=> new(snapshotRange, tag: this);

}
