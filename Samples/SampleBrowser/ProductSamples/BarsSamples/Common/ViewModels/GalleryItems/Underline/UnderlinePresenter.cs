using ActiproSoftware.Windows.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a control for rendering an underline preview.
	/// </summary>
	public class UnderlinePresenter : Decorator {

		#region Dependency Properties

		public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(UnderlineKind), typeof(UnderlinePresenter), new FrameworkPropertyMetadata(UnderlineKind.None, FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(UnderlinePresenter), new FrameworkPropertyMetadata(new Thickness(8.0, 5.0, 8.0, 5.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render the text "None".
		/// </summary>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateNoneFormattedText() {
			var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
			var fontSize = TextElement.GetFontSize(this);
			var foreground = TextElement.GetForeground(this);

			var formattedText = new FormattedText("None", CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground, null, TextOptions.GetTextFormattingMode(this)
				#if NETCOREAPP
				, VisualTreeHelper.GetDpi(this).PixelsPerDip
				#endif
				);

			return formattedText;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the kind of underline.
		/// </summary>
		/// <value>One of the <see cref="UnderlineKind"/> values.</value>
		public UnderlineKind Kind {
			get => (UnderlineKind)GetValue(KindProperty);
			set => SetValue(KindProperty, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint) {
			var formattedText = this.CreateNoneFormattedText();
			
			return new Size(
				Math.Ceiling(this.Padding.Left + Math.Max(120.0, formattedText.WidthIncludingTrailingWhitespace) + this.Padding.Right),
				Math.Round(this.Padding.Top + formattedText.Height + this.Padding.Bottom, MidpointRounding.AwayFromZero)
				);
		}

		/// <inheritdoc/>
		protected override void OnRender(DrawingContext drawingContext) {
			var x1 = this.Padding.Left + 0.5;
			var x2 = this.ActualWidth - this.Padding.Right - 1.0;
			var y = Math.Round((this.ActualHeight - 1.0) / 2.0, MidpointRounding.AwayFromZero) + 0.5;

			switch (this.Kind) {
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
				default: {  // None
					var formattedText = this.CreateNoneFormattedText();
					var location = new Point(x1 - 0.5, (this.ActualHeight - formattedText.Height) / 2.0);

					drawingContext.DrawText(formattedText, location, this.FlowDirection);
					break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the padding inside the control.
		/// </summary>
		/// <value>
		/// The padding inside the control.
		/// The default value is <c>8,5</c>.
		/// </value>
		public Thickness Padding {
			get => (Thickness)GetValue(PaddingProperty);
			set => SetValue(PaddingProperty, value);
		}

	}

}
