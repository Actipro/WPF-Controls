using System.Windows.Documents;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a control for rendering a symbol preview.
/// </summary>
[ToolboxItem(false)]
public class SymbolPresenter : Decorator {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render text.
	/// </summary>
	/// <param name="text">The text to display.</param>
	/// <returns>The <see cref="FormattedText"/> that was created.</returns>
	private FormattedText CreateFormattedText(string text) {
		var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
		var fontSize = TextElement.GetFontSize(this);
		var foreground = TextElement.GetForeground(this);

		var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground,
			numberSubstitution: null, TextOptions.GetTextFormattingMode(this), VisualTreeHelper.GetDpi(this).PixelsPerDip
		);

		return formattedText;
	}

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private SymbolBarGalleryItemViewModel ViewModel
		=> (SymbolBarGalleryItemViewModel)DataContext;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnRender(DrawingContext drawingContext) {
		if (ViewModel?.Value is { Length: > 0 } symbol) {
			var formattedText = CreateFormattedText(symbol);
			var location = new Point((ActualWidth - formattedText.Width) / 2.0, (ActualHeight - formattedText.Height) / 2.0);
			drawingContext.DrawText(formattedText, location, FlowDirection);
		}
	}

}
