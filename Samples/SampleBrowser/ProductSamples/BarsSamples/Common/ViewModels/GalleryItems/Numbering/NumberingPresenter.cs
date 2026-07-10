using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a control for rendering a numbering preview.
/// </summary>
public class NumberingPresenter : Decorator {

	private const double LineVisualSpacer = 3.0;
	private const double TextLineSpacer = 5.0;

	#region Dependency Properties

	public static readonly DependencyProperty LineBrushProperty
		= DependencyProperty.Register(nameof(LineBrush), typeof(Brush), typeof(NumberingPresenter), new FrameworkPropertyMetadata(defaultValue: null, FrameworkPropertyMetadataOptions.AffectsRender));

	public static readonly DependencyProperty PaddingProperty
		= DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(NumberingPresenter), new FrameworkPropertyMetadata(defaultValue: new Thickness(6.0, 10.0, 6.0, 10.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render text.
	/// </summary>
	/// <param name="text">The text to display.</param>
	private FormattedText CreateFormattedText(string text) {
		var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
		var fontSize = TextElement.GetFontSize(this);
		var foreground = TextElement.GetForeground(this);

		var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground, numberSubstitution: null,
			TextOptions.GetTextFormattingMode(this), VisualTreeHelper.GetDpi(this).PixelsPerDip);

		return formattedText;
	}

	/// <summary>
	/// Returns all of the text values that will be displayed in the preview.
	/// </summary>
	/// <param name="kind">The numbering kind whose text will be generating.</param>
	/// <param name="format">The format to be used.</param>
	/// <returns>An array of 3 text values.</returns>
	private static string?[] GetBulletTexts(NumberingKind kind, string format) {
		switch (kind) {
			case NumberingKind.ArabicNumeral:
				return [string.Format(format, "1"), string.Format(format, "2"), string.Format(format, "3")];
			case NumberingKind.LowerAlpha:
				return [string.Format(format, "a"), string.Format(format, "b"), string.Format(format, "c")];
			case NumberingKind.LowerRomanNumeral:
				return [string.Format(format, "i"), string.Format(format, "ii"), string.Format(format, "iii")];
			case NumberingKind.UpperAlpha:
				return [string.Format(format, "A"), string.Format(format, "B"), string.Format(format, "C")];
			case NumberingKind.UpperRomanNumeral:
				return [string.Format(format, "I"), string.Format(format, "II"), string.Format(format, "III")];
			default:
				return [null, null, null];
		}
	}

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private NumberingBarGalleryItemViewModel ViewModel
		=> (NumberingBarGalleryItemViewModel)DataContext;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Brush"/> used for lines.
	/// </summary>
	public Brush? LineBrush {
		get => (Brush)GetValue(LineBrushProperty);
		set => SetValue(LineBrushProperty, value);
	}

	/// <inheritdoc/>
	protected override Size MeasureOverride(Size constraint) {
		var formattedText = CreateFormattedText("1");
		var extent = (Padding.Top + 3 * formattedText.Height + 2 * TextLineSpacer + Padding.Bottom).Round();

		return new Size(extent, extent);
	}

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		var viewModel = ViewModel ?? new NumberingBarGalleryItemViewModel(NumberingKind.None);
		if (viewModel.Value == NumberingKind.None) {
			if (viewModel.Label is { Length: > 0 } label) {
				var formattedText = CreateFormattedText(label);
				var location = new Point((ActualWidth - formattedText.Width) / 2.0, (ActualHeight - formattedText.Height) / 2.0);
				drawingContext.DrawText(formattedText, location, FlowDirection);
			}
		}
		else {
			var bulletTexts = GetBulletTexts(viewModel.Value, viewModel.Format);
			var line1FormattedText = CreateFormattedText(bulletTexts[0] ?? string.Empty);
			var line2FormattedText = CreateFormattedText(bulletTexts[1] ?? string.Empty);
			var line3FormattedText = CreateFormattedText(bulletTexts[2] ?? string.Empty);

			var yCenter = Math.Round(ActualHeight / 2.0, MidpointRounding.AwayFromZero);
			var lineVisualPen = new Pen(LineBrush, 2.0);

			var location = new Point(Padding.Left, yCenter - line2FormattedText.Height / 2.0 - TextLineSpacer - line1FormattedText.Height);
			drawingContext.DrawText(line1FormattedText, location, FlowDirection);
			drawingContext.DrawLine(lineVisualPen,
				new Point(location.X + line1FormattedText.Width + LineVisualSpacer, location.Y + line1FormattedText.Height / 2.0),
				new Point(ActualWidth - Padding.Right, location.Y + line1FormattedText.Height / 2.0)
			);

			location = new Point(Padding.Left, yCenter - line2FormattedText.Height / 2.0);
			drawingContext.DrawText(line2FormattedText, location, FlowDirection);
			drawingContext.DrawLine(lineVisualPen,
				new Point(location.X + line2FormattedText.Width + LineVisualSpacer, location.Y + line2FormattedText.Height / 2.0),
				new Point(ActualWidth - Padding.Right, location.Y + line2FormattedText.Height / 2.0)
			);

			location = new Point(Padding.Left, yCenter + line2FormattedText.Height / 2.0 + TextLineSpacer);
			drawingContext.DrawText(line3FormattedText, location, FlowDirection);
			drawingContext.DrawLine(lineVisualPen,
				new Point(location.X + line3FormattedText.Width + LineVisualSpacer, location.Y + line3FormattedText.Height / 2.0),
				new Point(ActualWidth - Padding.Right, location.Y + line3FormattedText.Height / 2.0)
			);
		}
	}

	/// <summary>
	/// The padding inside the control.
	/// </summary>
	/// <value>
	/// The default value is <c>6,10</c>.
	/// </value>
	public Thickness Padding {
		get => (Thickness)GetValue(PaddingProperty);
		set => SetValue(PaddingProperty, value);
	}

}
