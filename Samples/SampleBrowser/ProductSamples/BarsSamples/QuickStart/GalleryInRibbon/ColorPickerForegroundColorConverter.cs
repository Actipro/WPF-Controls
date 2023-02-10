using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Media;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon {

	/// <summary>
	/// Converts a <see cref="ColorBarGalleryItemViewModel"/> into a <see cref="SolidColorBrush"/>
	/// that provides contrast against the color defined by the view model (i.e., white text for dark
	/// background colors and black text for light background colors).
	/// </summary>
	[ValueConversion(typeof(ColorBarGalleryItemViewModel), typeof(Brush))]
	public class TextForegroundColorConverter : IValueConverter {

		/// <inheritdoc cref="IValueConverter.Convert"/>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is ColorBarGalleryItemViewModel colorViewModel) {
				// Assume black text by default
				var foreColor = UIColor.FromColor(Colors.Black);

				// Adapt the color to the background
				foreColor.AdaptToBackground(colorViewModel.Color, isHighContrast: false);

				// Create a brush from the adapted color
				return new SolidColorBrush(foreColor.ToColor());
			}

			// Value could not be converted
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="IValueConverter.ConvertBack"/>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotSupportedException();
		}

	}

}
