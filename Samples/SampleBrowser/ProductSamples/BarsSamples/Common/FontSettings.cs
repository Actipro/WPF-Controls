using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Defines default font-related settings.
	/// </summary>
	public static class FontSettings {

		public const string DefaultFontFamilyName = "Calibri";
		public const double DefaultFontSize = 11.0;
		
		public const string HeadingFontFamilyName = "Calibri Light";
		public const double Heading1FontSize = DefaultFontSize + 5;
		public const double Heading2FontSize = DefaultFontSize + 2;
		public const double Heading3FontSize = DefaultFontSize + 1;
		public const double TitleFontSize = 28.0;

		public static readonly FontFamily DefaultFontFamily = new FontFamily(DefaultFontFamilyName);
		public static readonly FontFamily HeadingFontFamily = new FontFamily(HeadingFontFamilyName);

		public static readonly double DefaultWpfFontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(DefaultFontSize);
		public static readonly double Heading1WpfFontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(Heading1FontSize);
		public static readonly double Heading2WpfFontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(Heading2FontSize);
		public static readonly double TitleWpfFontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(TitleFontSize);


	}

}
