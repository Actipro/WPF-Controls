using ActiproSoftware.Extensions;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a control for rendering a shape preview.
/// </summary>
public class ShapePresenter : Decorator {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private ShapeBarGalleryItemViewModel ViewModel
		=> (ShapeBarGalleryItemViewModel)DataContext;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		if (ViewModel is { } viewModel) {
			var foreground = TextElement.GetForeground(this);

			const double Extent = 20;

			var x = ((ActualWidth - Extent) / 2.0).Round();
			var y = ((ActualHeight - Extent) / 2.0).Round();

			var pen = new Pen(foreground, 1.0);

			switch (viewModel.Value) {
				case ShapeKind.Line:
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					break;
				case ShapeKind.LineArrow:
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 6.0, y + Extent - 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 0.5, y + Extent - 6.0), new Point(x + Extent - 0.5, y + Extent - 0.5));
					break;
				case ShapeKind.LineArrowDouble:
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + 6.0, y + 0.5));
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + 0.5, y + 6.0));
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 6.0, y + Extent - 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 0.5, y + Extent - 6.0), new Point(x + Extent - 0.5, y + Extent - 0.5));
					break;
				case ShapeKind.Oval:
					drawingContext.DrawEllipse(null, pen, new Point(x + Extent / 2.0, y + Extent / 2.0), Extent / 2.0, Extent / 2.0 - 2.0);
					break;
				case ShapeKind.Rectangle:
					drawingContext.DrawRectangle(null, pen, new Rect(x + 0.5, y + 2.5, Extent - 1.0, Extent - 5.0));
					break;
				case ShapeKind.RectangleRoundedCorners:
					drawingContext.DrawRoundedRectangle(null, pen, new Rect(x + 0.5, y + 2.5, Extent - 1.0, Extent - 5.0), 4.0, 4.0);
					break;
				case ShapeKind.RectangleSingleCornerSnipped:
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 2.5), new Point(x + Extent - 5.5, y + 2.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 5.5, y + 2.5), new Point(y + Extent - 0.5, y + 7.5));
					drawingContext.DrawLine(pen, new Point(y + Extent - 0.5, y + 7.5), new Point(x + Extent - 0.5, y + Extent - 2.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 0.5, y + Extent - 2.5), new Point(y + +0.5, y + Extent - 2.5));
					drawingContext.DrawLine(pen, new Point(y + +0.5, y + Extent - 2.5), new Point(x + 0.5, y + 2.5));
					break;
				case ShapeKind.RightTriangle:
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + 0.5), new Point(x + Extent - 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + Extent - 0.5, y + Extent - 0.5), new Point(x + 0.5, y + Extent - 0.5));
					drawingContext.DrawLine(pen, new Point(x + 0.5, y + Extent - 0.5), new Point(x + 0.5, y + 0.5));
					break;
			}
		}
	}

}
