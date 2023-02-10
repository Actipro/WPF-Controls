using ActiproSoftware.Windows.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a control for rendering a numbering preview.
	/// </summary>
	public class NumberingPresenter : Decorator {

		private const double LineVisualSpacer = 3.0;
		private const double TextLineSpacer = 5.0;

		#region Dependency Properties

		public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register(nameof(LineBrush), typeof(Brush), typeof(NumberingPresenter), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(NumberingPresenter), new FrameworkPropertyMetadata(new Thickness(6.0, 10.0, 6.0, 10.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

		#endregion Dependency Properties

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
		/// Gets all of the text values that will be displayed in the preview.
		/// </summary>
		/// <returns>An array of 3 text values.</returns>
		private string[] GetBulletTexts() {
			var viewModel = this.ViewModel;
			if (viewModel != null) {
				var format = viewModel.Format;

				switch (viewModel.Kind) {
					case NumberingKind.ArabicNumeral:
						return new string[] { string.Format(format, "1"), string.Format(format, "2"), string.Format(format, "3") };
					case NumberingKind.LowerAlpha:
						return new string[] { string.Format(format, "a"), string.Format(format, "b"), string.Format(format, "c") };
					case NumberingKind.LowerRomanNumeral:
						return new string[] { string.Format(format, "i"), string.Format(format, "ii"), string.Format(format, "iii") };
					case NumberingKind.UpperAlpha:
						return new string[] { string.Format(format, "A"), string.Format(format, "B"), string.Format(format, "C") };
					case NumberingKind.UpperRomanNumeral:
						return new string[] { string.Format(format, "I"), string.Format(format, "II"), string.Format(format, "III") };
					default:  // None
						return new string[] { "None" };
				}
			}
			else
				return new string[] { null, null, null };
		}

		/// <summary>
		/// Gets the view model in the data context.
		/// </summary>
		/// <value>The view model in the data context.</value>
		private NumberingBarGalleryItemViewModel ViewModel => (NumberingBarGalleryItemViewModel)this.DataContext;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> used for lines.
		/// </summary>
		/// <value>A <see cref="Brush"/>.</value>
		public Brush LineBrush {
			get => (Brush)GetValue(LineBrushProperty);
			set => SetValue(LineBrushProperty, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint) {
			var formattedText = this.CreateFormattedText("1");
			var extent = Math.Round(this.Padding.Top + 3 * formattedText.Height + 2 * TextLineSpacer + this.Padding.Bottom, MidpointRounding.AwayFromZero);
			
			return new Size(extent, extent);
		}

		/// <inheritdoc/>
		protected override void OnRender(DrawingContext drawingContext) {
			var bulletTexts = this.GetBulletTexts();

			var viewModel = this.ViewModel;
			if ((viewModel != null) && (viewModel.Kind != NumberingKind.None)) {
				var line1FormattedText = this.CreateFormattedText(bulletTexts[0]);
				var line2FormattedText = this.CreateFormattedText(bulletTexts[1]);
				var line3FormattedText = this.CreateFormattedText(bulletTexts[2]);

				var yCenter = Math.Round(this.ActualHeight / 2.0, MidpointRounding.AwayFromZero);
				var lineVisualPen = new Pen(this.LineBrush, 2.0);

				var location = new Point(this.Padding.Left, yCenter - line2FormattedText.Height / 2.0 - TextLineSpacer - line1FormattedText.Height);
				drawingContext.DrawText(line1FormattedText, location, this.FlowDirection);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line1FormattedText.Width + LineVisualSpacer, location.Y + line1FormattedText.Height / 2.0), 
					new Point(this.ActualWidth - this.Padding.Right, location.Y + line1FormattedText.Height / 2.0));

				location = new Point(this.Padding.Left, yCenter - line2FormattedText.Height / 2.0);
				drawingContext.DrawText(line2FormattedText, location, this.FlowDirection);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line2FormattedText.Width + LineVisualSpacer, location.Y + line2FormattedText.Height / 2.0), 
					new Point(this.ActualWidth - this.Padding.Right, location.Y + line2FormattedText.Height / 2.0));

				location = new Point(this.Padding.Left, yCenter + line2FormattedText.Height / 2.0 + TextLineSpacer);
				drawingContext.DrawText(line3FormattedText, location, this.FlowDirection);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line3FormattedText.Width + LineVisualSpacer, location.Y + line3FormattedText.Height / 2.0), 
					new Point(this.ActualWidth - this.Padding.Right, location.Y + line3FormattedText.Height / 2.0));
			}
			else {
				var formattedText = this.CreateFormattedText(bulletTexts[0]);
				var location = new Point((this.ActualWidth - formattedText.Width) / 2.0, (this.ActualHeight - formattedText.Height) / 2.0);
				drawingContext.DrawText(formattedText, location, this.FlowDirection);
			}
		}

		/// <summary>
		/// Gets or sets the padding inside the control.
		/// </summary>
		/// <value>
		/// The padding inside the control.
		/// The default value is <c>6,10</c>.
		/// </value>
		public Thickness Padding {
			get => (Thickness)GetValue(PaddingProperty);
			set => SetValue(PaddingProperty, value);
		}

	}

}
