using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a control for rendering an underline preview.
/// </summary>
public class UnderlinePresenter : Decorator {

	#region Dependency Properties

	public static readonly DependencyProperty KindProperty
		= DependencyProperty.Register(nameof(Kind), typeof(UnderlineKind), typeof(UnderlinePresenter), new FrameworkPropertyMetadata(defaultValue: UnderlineKind.None, FrameworkPropertyMetadataOptions.AffectsRender));

	public static readonly DependencyProperty PaddingProperty
		= DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(UnderlinePresenter), new FrameworkPropertyMetadata(defaultValue: new Thickness(8.0, 5.0, 8.0, 5.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render the text "None".
	/// </summary>
	private FormattedText CreateNoneFormattedText() {
		var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
		var fontSize = TextElement.GetFontSize(this);
		var foreground = TextElement.GetForeground(this);

		var formattedText = new FormattedText("None", CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground, numberSubstitution: null,
			TextOptions.GetTextFormattingMode(this), VisualTreeHelper.GetDpi(this).PixelsPerDip);

		return formattedText;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The kind of underline.
	/// </summary>
	public UnderlineKind Kind {
		get => (UnderlineKind)GetValue(KindProperty);
		set => SetValue(KindProperty, value);
	}

	/// <inheritdoc/>
	protected override Size MeasureOverride(Size constraint) {
		var formattedText = CreateNoneFormattedText();

		return new Size(
			Math.Ceiling(Padding.Left + Math.Max(120.0, formattedText.WidthIncludingTrailingWhitespace) + Padding.Right),
			(Padding.Top + formattedText.Height + Padding.Bottom).Round()
		);
	}

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		var x1 = Padding.Left + 0.5;
		var x2 = ActualWidth - Padding.Right - 1.0;
		var y = ((ActualHeight - 1.0) / 2.0).Round() + 0.5;

		switch (Kind) {
			case UnderlineKind.Underline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0);
				drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
				break;
			}
			case UnderlineKind.DoubleUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0);
				drawingContext.DrawLine(pen, new Point(x1, y - 1.0), new Point(x2, y - 1.0));
				drawingContext.DrawLine(pen, new Point(x1, y + 1.0), new Point(x2, y + 1.0));
				break;
			}
			case UnderlineKind.ThickUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 2.0);
				drawingContext.DrawLine(pen, new Point(x1, y - 0.5), new Point(x2, y - 0.5));
				break;
			}
			case UnderlineKind.DottedUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyles.Dot };
				drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
				break;
			}
			case UnderlineKind.DashedUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyles.Dash };
				drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
				break;
			}
			case UnderlineKind.DotDashUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyles.DashDot };
				drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
				break;
			}
			case UnderlineKind.DotDotDashUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyles.DashDotDot };
				drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
				break;
			}
			case UnderlineKind.WaveUnderline: {
				var pen = new Pen(TextElement.GetForeground(this), 0.5);
				for (var x = x1 - 0.5; x < x2 - 1.0; x += 2.0) {
					if (x % 4.0 == 0.0) {
						// Draw up diagonal
						drawingContext.DrawLine(pen, new Point(x + 0.5, y + 1.0), new Point(x + 1.5, y));
					}
					else {
						// Draw down diagonal
						drawingContext.DrawLine(pen, new Point(x + 0.5, y - 1.0), new Point(x + 1.5, y));
					}
				}
				break;
			}
			case UnderlineKind.None:
			default: {
				var formattedText = CreateNoneFormattedText();
				var location = new Point(x1 - 0.5, (ActualHeight - formattedText.Height) / 2.0);

				drawingContext.DrawText(formattedText, location, FlowDirection);
				break;
			}
		}
	}

	/// <summary>
	/// The padding inside the control.
	/// </summary>
	/// <value>
	/// The default value is <c>8,5</c>.
	/// </value>
	public Thickness Padding {
		get => (Thickness)GetValue(PaddingProperty);
		set => SetValue(PaddingProperty, value);
	}

}
