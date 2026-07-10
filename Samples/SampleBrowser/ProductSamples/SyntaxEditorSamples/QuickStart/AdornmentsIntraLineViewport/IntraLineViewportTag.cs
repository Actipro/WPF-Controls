using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport;

/// <summary>
/// Provides an <see cref="IIntraLineSpacerTag"/> implementation that reserves intra-line space for an element.
/// </summary>
public class IntraLineViewportTag : IIntraLineSpacerTag {

	private const double MaxAdornmentHeight = 300.0;
	private const double MinAdornmentHeight = 90.0;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IIntraLineSpacerTag.BottomMargin"/>
	public double BottomMargin { get; set; }

	/// <inheritdoc cref="IIntraLineSpacerTag.Key"/>
	public object? Key { get; set; }

	/// <inheritdoc cref="IIntraLineSpacerTag.TopMargin"/>
	public double TopMargin { get; set; }

	/// <summary>
	/// Updates the bottom margin based on the view height.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> to examine.</param>
	public void UpdateBottomMargin(IEditorView view) {
		var height = Math.Round(view.TextAreaViewportBounds.Height / 3.0);
		BottomMargin = MathHelper.Range(height, MinAdornmentHeight, MaxAdornmentHeight);
	}

}
