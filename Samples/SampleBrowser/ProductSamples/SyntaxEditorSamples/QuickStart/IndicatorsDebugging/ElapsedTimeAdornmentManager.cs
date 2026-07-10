using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Represents an adornment manager for a view that renders elapsed times.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class ElapsedTimeAdornmentManager(IEditorView view) : IntraTextAdornmentManagerBase<IEditorView, ElapsedTimeTag>(view, _layerDefinition) {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("ElapsedTime", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.Before));

	private const double FontSizeAdjustment = 0.9;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<ElapsedTimeTag> tagRange, TextBounds bounds) {
		var boundsList = viewLine.GetTextBounds(new TextRange(tagRange.SnapshotRange.StartOffset)).ToArray();
		if (boundsList?.Length == 1) {
			// Create a text block
			var adornment = new TextBlock() {
				FontFamily = SystemFonts.MessageFontFamily,
				FontSize = Math.Round(View.DefaultFontSize * FontSizeAdjustment, MidpointRounding.AwayFromZero),
				Opacity = 0.6,
				Text = tagRange.Tag.TimeSpanText
			};

			// Measure the adornment and determine its display location
			adornment.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
			var adornmentLocation = new Point(
				boundsList[0].Left + View.DefaultCharacterWidth,
				boundsList[0].Top + ((bounds.Height - adornment.DesiredSize.Height) / 2.0)
			);

			// Add the adornment to the layer
			AdornmentLayer.AddAdornment(reason, adornment, adornmentLocation, tagRange.Tag.Key, removedCallback: null);
		}
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
