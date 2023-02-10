using ActiproSoftware.Windows.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {
	
	/// <summary>
	/// Represents a control for rendering a symbol preview.
	/// </summary>
	[ToolboxItem(false)]
	public class SymbolPresenter : Decorator {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render text.
		/// </summary>
		/// <param name="text">The text to display.</param>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateFormattedText(string text) {
			var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
			var fontSize = TextElement.GetFontSize(this);
			var foreground = TextElement.GetForeground(this);

			var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground, null, TextOptions.GetTextFormattingMode(this)
				#if NETCOREAPP
				, VisualTreeHelper.GetDpi(this).PixelsPerDip
				#endif
				);

			return formattedText;
		}

		/// <summary>
		/// Gets the view model in the data context.
		/// </summary>
		/// <value>The view model in the data context.</value>
		private SymbolBarGalleryItemViewModel ViewModel => (SymbolBarGalleryItemViewModel)this.DataContext;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void OnRender(DrawingContext drawingContext) {
			var viewModel = this.ViewModel;
			if ((viewModel != null) && !string.IsNullOrEmpty(viewModel.Symbol)) {
				var formattedText = this.CreateFormattedText(viewModel.Symbol);
				var location = new Point((this.ActualWidth - formattedText.Width) / 2.0, (this.ActualHeight - formattedText.Height) / 2.0);
				drawingContext.DrawText(formattedText, location, this.FlowDirection);
			}
		}
		
	}

}
