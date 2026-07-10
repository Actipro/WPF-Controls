using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced;

/// <summary>
/// Represents an adornment manager for a view that renders intra-text placeholders for collapsed regions.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class CollapsedRegionAdornmentManager(IEditorView view)
	: IntraTextAdornmentManagerBase<IEditorView, CollapsedRegionTag>(view, AdornmentLayerDefinitions.CollapsedRegion) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<CollapsedRegionTag> tagRange, TextBounds bounds) {
		// Create a border
		var outerBorder = new Border {
			Background = Brushes.Transparent,
			BorderBrush = Brushes.Gray,
			BorderThickness = new Thickness(1.0),
			CornerRadius = new CornerRadius(2.0),
			Cursor = Cursors.Arrow,
			SnapsToDevicePixels = true,
			Width = bounds.Width,
			Height = bounds.Height
		};
		AdornmentLayer.AddAdornment(reason, outerBorder, new Point(Math.Round(bounds.Left), Math.Round(bounds.Top)), tagRange.Tag.Key, removedCallback: null);

		// Create the text adornment
		var element = new TextBlock {
			IsHitTestVisible = false,
			Text = tagRange.Tag.Text,
			FontFamily = View.SyntaxEditor.FontFamily,
			FontSize = View.SyntaxEditor.FontSize,
			Foreground = Brushes.Gray
		};
		element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

		// Get the location
		var location = new Point(
			Math.Round(bounds.Left),
			Math.Round(bounds.Top + (bounds.Height - element.DesiredSize.Height) / 2)
		);

		// Add the text adornment to the layer
		AdornmentLayer.AddAdornment(reason, element, location, tagRange.Tag.Key, removedCallback: null);
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		// Call the base method
		base.OnClosed();
	}

}
