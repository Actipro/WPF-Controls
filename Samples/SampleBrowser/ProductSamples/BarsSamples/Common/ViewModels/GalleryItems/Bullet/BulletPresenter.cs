using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a control for rendering a bullet preview.
/// </summary>
public class BulletPresenter : Decorator {

	#region Dependency Properties

	public static readonly DependencyProperty PaddingProperty
		= DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(BulletPresenter), new FrameworkPropertyMetadata(defaultValue: new Thickness(6.0, 10.0, 6.0, 10.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

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
	/// The view model in the data context.
	/// </summary>
	private BulletBarGalleryItemViewModel ViewModel
		=> (BulletBarGalleryItemViewModel)DataContext;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		var viewModel = ViewModel ?? new BulletBarGalleryItemViewModel(BulletKind.None);
		var kind = viewModel.Value;
		if (kind == BulletKind.None) {
			if (viewModel.Label is { Length: > 0 } label) {
				var formattedText = CreateFormattedText(label);
				var location = new Point((ActualWidth - formattedText.Width) / 2.0, (ActualHeight - formattedText.Height) / 2.0);
				drawingContext.DrawText(formattedText, location, FlowDirection);
			}
		}
		else {
			var foreground = TextElement.GetForeground(this);

			var xCenter = (ActualWidth / 2.0).Round();
			var yCenter = (ActualHeight / 2.0).Round();

			const double Radius = 5.0;

			switch (kind) {
				case BulletKind.Circle:
					drawingContext.DrawEllipse(brush: null, new Pen(foreground, 1.0), new Point(xCenter, yCenter), Radius, Radius);
					break;
				case BulletKind.FilledCircle:
					drawingContext.DrawEllipse(foreground, pen: null, new Point(xCenter, yCenter), Radius, Radius);
					break;
				case BulletKind.FilledSquare:
					drawingContext.DrawRectangle(foreground, pen: null, new Rect(xCenter - Radius, yCenter - Radius, 2 * Radius, 2 * Radius));
					break;
				case BulletKind.Square:
					drawingContext.DrawRectangle(brush: null, new Pen(foreground, 1.0), new Rect(xCenter - Radius + 0.5, yCenter - Radius + 0.5, 2 * Radius - 1.0, 2 * Radius - 1.0));
					break;
			}
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
