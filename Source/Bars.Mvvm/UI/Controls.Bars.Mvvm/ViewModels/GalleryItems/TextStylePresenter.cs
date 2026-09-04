using ActiproSoftware.Extensions;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a control for rendering a text style preview.
/// </summary>
[ToolboxItem(false)]
public class TextStylePresenter : Decorator {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Background"/> property.
	/// </summary>
	public static readonly DependencyProperty BackgroundProperty
		= DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(TextStylePresenter), new FrameworkPropertyMetadata(defaultValue: Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

	/// <summary>
	/// Defines the <see cref="Padding"/> property.
	/// </summary>
	public static readonly DependencyProperty PaddingProperty
		= DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(TextStylePresenter), new FrameworkPropertyMetadata(defaultValue: new Thickness(3.0, 0.0, 3.0, 0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render the label.
	/// </summary>
	/// <param name="viewModel">The <see cref="TextStyleBarGalleryItemViewModel"/> to examine.</param>
	/// <returns>The <see cref="FormattedText"/> that was created.</returns>
	private FormattedText CreateFormattedText(TextStyleBarGalleryItemViewModel viewModel) {
		var textStyle = viewModel.Value
			?? throw new ArgumentException($"The view model's value must define a {nameof(TextStyle)}.");

		var fontFamily = new FontFamily(textStyle.FontFamilyName);
		var fontStyle = textStyle.Italic ? FontStyles.Italic : FontStyles.Normal;
		var fontWeight = textStyle.Bold ? FontWeights.Bold : FontWeights.Normal;
		var typeface = new Typeface(fontFamily, fontStyle, fontWeight, FontStretches.Normal);
		var fontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(textStyle.FontSize);
		var foreground = new SolidColorBrush(textStyle.TextColor);

		var formattedText = new FormattedText(viewModel.Label, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground,
			numberSubstitution: null, TextOptions.GetTextFormattingMode(this), VisualTreeHelper.GetDpi(this).PixelsPerDip
		);

		return formattedText;
	}

	/// <summary>
	/// Returns the margin based on the current height.
	/// </summary>
	private double GetMarginForHeight()
		=> (ActualHeight >= 40.0) ? 4.0 : 2.0;

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private TextStyleBarGalleryItemViewModel ViewModel
		=> (TextStyleBarGalleryItemViewModel)DataContext;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Brush"/> for the background.
	/// </summary>
	public Brush Background {
		get => (Brush)GetValue(BackgroundProperty);
		set => SetValue(BackgroundProperty, value);
	}

	/// <inheritdoc/>
	protected override Size MeasureOverride(Size constraint)
		=> new(100.0, Math.Min(constraint.Height, 56.0));

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		if (ViewModel is { } viewModel) {
			// Fill in the entire presenter with a Transparent background
			var bounds = new Rect(0.0, 0.0, ActualWidth, ActualHeight);
			drawingContext.DrawRectangle(Brushes.Transparent, pen: null, bounds);

			// Deflate by a margin amount appropriate for the presenter height and fill in the background...
			//   This allows any hover/selection highlights from the gallery item container to show in the margin area
			var margin = GetMarginForHeight();
			bounds.Inflate(-margin, -margin);
			drawingContext.DrawRectangle(Background, pen: null, bounds);

			var clipBounds = new Rect(
				bounds.Left + Padding.Left,
				bounds.Top + Padding.Top,
				(bounds.Width - Padding.Left - Padding.Right).ClampToNonnegative(),
				(bounds.Height - Padding.Top - Padding.Bottom).ClampToNonnegative()
			);

			var formattedText = CreateFormattedText(viewModel);
			var location = formattedText.Width > clipBounds.Width
				? new Point(clipBounds.Left, (ActualHeight - formattedText.Height) / 2.0)  // Left-align
				: new Point((ActualWidth - formattedText.Width) / 2.0, (ActualHeight - formattedText.Height) / 2.0);  // Center

			// Draw the styled text
			drawingContext.PushClip(new RectangleGeometry(clipBounds));
			drawingContext.DrawText(formattedText, location, FlowDirection);
			drawingContext.Pop();
		}
	}

	/// <summary>
	/// The padding inside the control.
	/// </summary>
	/// <value>
	/// The default value is <c>3,0</c>.
	/// </value>
	public Thickness Padding {
		get => (Thickness)GetValue(PaddingProperty);
		set => SetValue(PaddingProperty, value);
	}

}
