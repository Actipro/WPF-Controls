using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Controls.Views.Primitives;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.CustomPanelRandom;

/// <summary>
/// Represents a custom panel that randomly arranges it's child elements.
/// </summary>
public class RandomPanel : PanelBase {

	private readonly Random _random = new(Environment.TickCount);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override Size ArrangeElements(IList<UIElement> elements, Size finalSize) {
		// Cache and reset layout pending flag
		bool isLayoutUpdatePending = IsLayoutUpdatePending;
		IsLayoutUpdatePending = false;

		// Iterate over the elements and arrange
		foreach (var element in elements) {
			if (element is not null) {
				var desiredSize = element.DesiredSize;

				// Calculate a random x/y position that keeps the element in the view
				var x = Math.Max(_random.NextDouble() * (finalSize.Width - desiredSize.Width), 0);
				var y = Math.Max(_random.NextDouble() * (finalSize.Height - desiredSize.Height), 0);

				// Update the arrange state with the new arrange rect, but if there are leaving elements then don't move the element
				var state = new ArrangeState(element, leaving: false, isLayoutUpdatePending);
				state.ArrangeRect = HasLeavingChildren
					? state.PreviousArrangeRect
					: new Rect(x, y, desiredSize.Width, desiredSize.Height);

				SetArrangeState(element, state);
			}
		}

		return finalSize;
	}

	/// <inheritdoc/>
	public override Size MeasureElements(IList<UIElement> elements, Size availableSize) {
		// Measure each element, and return the largest width and height needed
		var desiredSize = new Size();
		foreach (var element in elements) {
			if (element is not null) {
				element.Measure(availableSize);

				desiredSize.Width = Math.Max(element.DesiredSize.Width, desiredSize.Width);
				desiredSize.Height = Math.Max(element.DesiredSize.Height, desiredSize.Height);
			}
		}
		return desiredSize;
	}

}
