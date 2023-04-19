using ActiproSoftware.Windows.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a control for rendering a text style preview.
	/// </summary>
	[ToolboxItem(false)]
	public class TextStylePresenter : Decorator {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Background"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Background"/> dependency property.</value>
		public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(TextStylePresenter), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

		/// <summary>
		/// Identifies the <see cref="Padding"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Padding"/> dependency property.</value>
		public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(TextStylePresenter), new FrameworkPropertyMetadata(new Thickness(3.0, 0.0, 3.0, 0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render the label.
		/// </summary>
		/// <param name="viewModel">The <see cref="TextStyleBarGalleryItemViewModel"/> to examine.</param>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateFormattedText(TextStyleBarGalleryItemViewModel viewModel) {
			var textStyle = viewModel.Value;
			var fontFamily = new FontFamily(textStyle.FontFamilyName);
			var fontStyle = textStyle.Italic ? FontStyles.Italic : FontStyles.Normal;
			var fontWeight = textStyle.Bold ? FontWeights.Bold : FontWeights.Normal;
			var typeface = new Typeface(fontFamily, fontStyle, fontWeight, FontStretches.Normal);
			var fontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(textStyle.FontSize);
			var foreground = new SolidColorBrush(textStyle.TextColor);

			var formattedText = new FormattedText(viewModel.Label, CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground, null, TextOptions.GetTextFormattingMode(this), VisualTreeHelper.GetDpi(this).PixelsPerDip);

			return formattedText;
		}

		/// <summary>
		/// Returns the margin based on the current height.
		/// </summary>
		/// <returns>The margin based on the current height.</returns>
		private double GetMarginForHeight()
			=> this.ActualHeight >= 40.0 ? 4.0 : 2.0;
		
		/// <summary>
		/// Gets the view model in the data context.
		/// </summary>
		/// <value>The view model in the data context.</value>
		private TextStyleBarGalleryItemViewModel ViewModel 
			=> (TextStyleBarGalleryItemViewModel)this.DataContext;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> for the background.
		/// </summary>
		/// <value>The <see cref="Brush"/> for the background.</value>
		public Brush Background {
			get => (Brush)GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}
		
		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint)
			=> new Size(100.0, Math.Min(constraint.Height, 56.0));

		/// <inheritdoc/>
		protected override void OnRender(DrawingContext drawingContext) {
			var viewModel = this.ViewModel;
			if (viewModel != null) {
				// Fill in the entire presenter with a Transparent background
				var bounds = new Rect(0.0, 0.0, this.ActualWidth, this.ActualHeight);
				drawingContext.DrawRectangle(Brushes.Transparent, pen: null, bounds);

				// Deflate by a margin amount appropriate for the presenter height and fill in the background...
				//   This allows any hover/selection highlights from the gallery item container to show in the margin area
				var margin = GetMarginForHeight();
				bounds.Inflate(-margin, -margin);
				drawingContext.DrawRectangle(this.Background, pen: null, bounds);

				var clipBounds = new Rect(
					bounds.Left + this.Padding.Left, 
					bounds.Top + this.Padding.Top, 
					Math.Max(0.0, bounds.Width - this.Padding.Left - this.Padding.Right), 
					Math.Max(0.0, bounds.Height - this.Padding.Top - this.Padding.Bottom)
				);

				var formattedText = this.CreateFormattedText(viewModel);
				var location = formattedText.Width > clipBounds.Width
					? new Point(clipBounds.Left, (this.ActualHeight - formattedText.Height) / 2.0)  // Left-align
					: new Point((this.ActualWidth - formattedText.Width) / 2.0, (this.ActualHeight - formattedText.Height) / 2.0);  // Center

				// Draw the styled text
				drawingContext.PushClip(new RectangleGeometry(clipBounds));
				drawingContext.DrawText(formattedText, location, this.FlowDirection);
				drawingContext.Pop();
			}
		}
		
		/// <summary>
		/// Gets or sets the padding inside the control.
		/// </summary>
		/// <value>
		/// The padding inside the control.
		/// The default value is <c>3,0</c>.
		/// </value>
		public Thickness Padding {
			get => (Thickness)GetValue(PaddingProperty);
			set => SetValue(PaddingProperty, value);
		}
		
	}

}
