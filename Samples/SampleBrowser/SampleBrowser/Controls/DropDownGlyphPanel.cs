using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides an implementation of a <see cref="Panel"/> that measures a drop-down glyph (the last element) before filling the first element.
/// </summary>
public class DropDownGlyphPanel : Panel {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override Size ArrangeOverride(Size finalSize) {
		var internalChildren = InternalChildren;
		if (internalChildren.Count == 2) {
			var x = Math.Min(internalChildren[0].DesiredSize.Width, finalSize.Width - internalChildren[1].DesiredSize.Width).ClampToNonnegative();
			internalChildren[1].Arrange(new Rect(x, y: 0, internalChildren[1].DesiredSize.Width, finalSize.Height));
			internalChildren[0].Arrange(new Rect(x: 0, y: 0, width: x, finalSize.Height));
		}

		return finalSize;
	}

	/// <inheritdoc/>
	protected override Size MeasureOverride(Size availableSize) {
		var desiredWidth = 0.0;
		var desiredHeight = 0.0;

		var availableWidth = availableSize.Width;
		var availableHeight = availableSize.Height;

		var internalChildren = InternalChildren;
		if (internalChildren.Count == 2) {
			// Glyph
			internalChildren[1].Measure(new Size(availableWidth, availableHeight));
			availableWidth = (availableWidth - internalChildren[1].DesiredSize.Width).ClampToNonnegative();

			// Text
			internalChildren[0].Measure(new Size(availableWidth, availableHeight));

			// Calculate size
			desiredWidth = Math.Ceiling(internalChildren[0].DesiredSize.Width + internalChildren[1].DesiredSize.Width);
			desiredHeight = Math.Max(internalChildren[0].DesiredSize.Height, internalChildren[1].DesiredSize.Height).Round(RoundMode.CeilingToEven);
		}

		return new Size(desiredWidth, desiredHeight);
	}

}
