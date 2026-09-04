using ActiproSoftware.Extensions;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom;

/// <summary>
/// Represents an <see cref="IIndicatorTag"/> that renders a custom indicator over a text range.
/// </summary>
public class CustomIndicatorTag : IndicatorClassificationTagBase {

	private static readonly ClassificationType _customIndicatorClassificationType = new("Custom Indicator");

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CustomIndicatorTag() {
		// This sample assumes the editor will use the AmbientHighlightingStyleRegistry
		var registry = AmbientHighlightingStyleRegistry.Instance;

		// Configure light/dark color palettes with default colors
		var key = _customIndicatorClassificationType.Key;
		registry.LightColorPalette?.SetForeground(key, UIColor.FromWebColor("#004000"));
		registry.LightColorPalette?.SetBackground(key, UIColor.FromWebColor("#ebf1dd"));
		registry.DarkColorPalette?.SetForeground(key, UIColor.FromWebColor("#95db7d"));
		registry.DarkColorPalette?.SetBackground(key, UIColor.FromWebColor("#265e4d"));

		// Associate a default style with the classification type
		//   and the current color palette color will be automatically applied
		registry.Register(_customIndicatorClassificationType, new HighlightingStyle());
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IClassificationType ClassificationType
		=> _customIndicatorClassificationType;

	/// <inheritdoc/>
	public override void DrawGlyph(TextViewDrawContext context, ITextViewLine viewLine, TagSnapshotRange<IIndicatorTag> tagRange, Rect bounds) {
		var diameter = (Math.Min(bounds.Width, bounds.Height) - 2.0).Round().ClampToRange(8, 13);
		var x = bounds.X + (bounds.Width - diameter) / 2.0;
		var y = bounds.Y + (bounds.Height - diameter) / 2.0;

		// Create a circle glyph that uses the same foreground/background colors as the highlighting style
		var key = _customIndicatorClassificationType.Key;
		var colorPalette = AmbientHighlightingStyleRegistry.Instance.CurrentColorPalette;
		context.FillEllipse(new Rect(x, y, diameter, diameter), colorPalette.GetBackground(key) ?? Colors.Green);
		context.DrawEllipse(new Rect(x, y, diameter, diameter), colorPalette.GetForeground(key) ?? Colors.DarkGreen, LineKind.Solid, 1);
	}

}
