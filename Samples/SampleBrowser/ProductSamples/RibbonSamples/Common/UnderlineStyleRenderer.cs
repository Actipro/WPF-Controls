using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common;

/// <summary>
/// Renders <see cref="UnderlineStyle"/> values to an element.
/// </summary>
public class UnderlineStyleRenderer {

	/// <summary>
	/// Draws an <see cref="UnderlineStyle"/> to an element.
	/// </summary>
	/// <param name="e">The event data.</param>
	/// <param name="underlineStyle">The <see cref="UnderlineStyle"/> to draw.</param>
	public static void Render(CustomDrawElementCustomDrawEventArgs e, UnderlineStyle underlineStyle) {
		// Find the y-coordinate
		double y = e.Element.RenderSize.Height / 2;

		e.DrawingContext.PushGuidelineSet(new GuidelineSet([10.5, 139.5], [y - 0.5, y + 0.5]));

		// Draw the underline
		switch (underlineStyle) {
			case UnderlineStyle.Underline:
				e.DrawingContext.DrawLine(new Pen(Brushes.Black, 1), new Point(10, y), new Point(140, y));
				break;
			case UnderlineStyle.DoubleUnderline:
				e.DrawingContext.DrawLine(new Pen(Brushes.Black, 1), new Point(10, y - 1), new Point(140, y - 1));
				e.DrawingContext.DrawLine(new Pen(Brushes.Black, 1), new Point(10, y + 1), new Point(140, y + 1));
				break;
			case UnderlineStyle.ThickUnderline:
				e.DrawingContext.DrawRectangle(Brushes.Black, null, new Rect(10, y, 130, 2));
				break;
			case UnderlineStyle.DottedUnderline: {
				var pen = new Pen(Brushes.Black, 1) {
					DashStyle = DashStyles.Dot
				};
				e.DrawingContext.DrawLine(pen, new Point(10, y), new Point(140, y));
				break;
			}
			case UnderlineStyle.DashedUnderline: {
				var pen = new Pen(Brushes.Black, 1) {
					DashStyle = DashStyles.Dash
				};
				e.DrawingContext.DrawLine(pen, new Point(10, y), new Point(140, y));
				break;
			}
			case UnderlineStyle.DotDashUnderline: {
				var pen = new Pen(Brushes.Black, 1) {
					DashStyle = DashStyles.DashDot
				};
				e.DrawingContext.DrawLine(pen, new Point(10, y), new Point(140, y));
				break;
			}
			case UnderlineStyle.DotDotDashUnderline: {
				var pen = new Pen(Brushes.Black, 1) {
					DashStyle = DashStyles.DashDotDot
				};
				e.DrawingContext.DrawLine(pen, new Point(10, y), new Point(140, y));
				break;
			}
			case UnderlineStyle.WaveUnderline: {
				var pen = new Pen(Brushes.Black, 0.5);
				double x = 10;
				for (; x < 140; x += 2) {
					if (x % 4 == 0) {
						// Draw up diagonal
						e.DrawingContext.DrawLine(pen, new Point(x, y + 1), new Point(x + 1, y));
					}
					else {
						// Draw down diagonal
						e.DrawingContext.DrawLine(pen, new Point(x, y - 1), new Point(x + 1, y));
					}
				}
				break;
			}
		}

		e.DrawingContext.Pop();
	}

}
