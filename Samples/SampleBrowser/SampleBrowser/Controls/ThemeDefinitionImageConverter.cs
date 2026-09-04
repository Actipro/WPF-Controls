using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Represents a value converter that returns whether the specified value is null, and if it is a string, also if it is null or empty.
/// </summary>
[ValueConversion(typeof(string), typeof(ImageSource))]
public class ThemeDefinitionImageConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var menuItem = value as MenuItem;
		var themeName = menuItem?.Tag as string;

		var accentColor = Colors.Transparent;
		Color backgroundColor;
		Color borderColor;
		var isAccentBelow = false;

		switch (themeName) {
			case ThemeNames.HighContrast:
				backgroundColor = UIColor.FromWebColor("#000000").ToColor();
				borderColor = UIColor.FromWebColor("#00ff00").ToColor();
				break;
			case ThemeNames.Black:
			case ThemeNames.MetroBlack:
				backgroundColor = UIColor.FromWebColor("#111112").ToColor();
				borderColor = UIColor.FromWebColor("#494c50").ToColor();
				break;
			case ThemeNames.Dark:
			case ThemeNames.MetroDark:
				backgroundColor = UIColor.FromWebColor("#2d2f32").ToColor();
				borderColor = UIColor.FromWebColor("#575a5f").ToColor();
				break;
			case ThemeNames.Light:
			case ThemeNames.MetroLight:
			case ThemeNames.OfficeColorfulBlue:
			case ThemeNames.OfficeColorfulGreen:
			case ThemeNames.OfficeColorfulIndigo:
			case ThemeNames.OfficeColorfulOrange:
			case ThemeNames.OfficeColorfulPink:
			case ThemeNames.OfficeColorfulPurple:
			case ThemeNames.OfficeColorfulRed:
			case ThemeNames.OfficeColorfulTeal:
			case ThemeNames.OfficeColorfulYellow:
				backgroundColor = UIColor.FromWebColor("#f1f1f2").ToColor();
				borderColor = UIColor.FromWebColor("#c7c9cb").ToColor();
				break;
			case ThemeNames.White:
			case ThemeNames.MetroWhite:
			case ThemeNames.OfficeWhiteBlue:
			case ThemeNames.OfficeWhiteGreen:
			case ThemeNames.OfficeWhiteIndigo:
			case ThemeNames.OfficeWhiteOrange:
			case ThemeNames.OfficeWhitePink:
			case ThemeNames.OfficeWhitePurple:
			case ThemeNames.OfficeWhiteRed:
			case ThemeNames.OfficeWhiteTeal:
			case ThemeNames.OfficeWhiteYellow:
				backgroundColor = UIColor.FromWebColor("#ffffff").ToColor();
				borderColor = UIColor.FromWebColor("#d8d9da").ToColor();
				break;
			case ThemeNames.AeroNormalColor:
				accentColor = UIColor.FromWebColor("#a5c0db").ToColor();
				backgroundColor = UIColor.FromWebColor("#dce1ed").ToColor();
				borderColor = UIColor.FromWebColor("#abadb3").ToColor();
				break;
			case ThemeNames.Office2010Black:
				accentColor = UIColor.FromWebColor("#767676").ToColor();
				backgroundColor = UIColor.FromWebColor("#bababa").ToColor();
				borderColor = UIColor.FromWebColor("#abadb3").ToColor();
				break;
			case ThemeNames.Office2010Blue:
				accentColor = UIColor.FromWebColor("#bed2e8").ToColor();
				backgroundColor = UIColor.FromWebColor("#cfdded").ToColor();
				borderColor = UIColor.FromWebColor("#abadb3").ToColor();
				break;
			case ThemeNames.Office2010Silver:
				accentColor = UIColor.FromWebColor("#e8ebed").ToColor();
				backgroundColor = UIColor.FromWebColor("#e5e9ee").ToColor();
				borderColor = UIColor.FromWebColor("#abadb3").ToColor();
				break;
			default:
				// Unknown theme
				return null;
		}

		switch (themeName) {
			case ThemeNames.MetroBlack:
			case ThemeNames.MetroDark:
			case ThemeNames.MetroLight:
			case ThemeNames.MetroWhite:
				accentColor = UIColor.FromWebColor("#0077ac").ToColor();
				break;
			case ThemeNames.OfficeColorfulBlue:
			case ThemeNames.OfficeWhiteBlue:
				accentColor = UIColor.FromWebColor("#106ebe").ToColor();
				break;
			case ThemeNames.OfficeColorfulGreen:
			case ThemeNames.OfficeWhiteGreen:
				accentColor = UIColor.FromWebColor("#217346").ToColor();
				break;
			case ThemeNames.OfficeColorfulIndigo:
			case ThemeNames.OfficeWhiteIndigo:
				accentColor = UIColor.FromWebColor("#2b579a").ToColor();
				break;
			case ThemeNames.OfficeColorfulOrange:
			case ThemeNames.OfficeWhiteOrange:
				accentColor = UIColor.FromWebColor("#b7472a").ToColor();
				break;
			case ThemeNames.OfficeColorfulPink:
			case ThemeNames.OfficeWhitePink:
				accentColor = UIColor.FromWebColor("#7619ab").ToColor();
				break;
			case ThemeNames.OfficeColorfulPurple:
			case ThemeNames.OfficeWhitePurple:
				accentColor = UIColor.FromWebColor("#823979").ToColor();
				break;
			case ThemeNames.OfficeColorfulRed:
			case ThemeNames.OfficeWhiteRed:
				accentColor = UIColor.FromWebColor("#c51d17").ToColor();
				break;
			case ThemeNames.OfficeColorfulTeal:
			case ThemeNames.OfficeWhiteTeal:
				accentColor = UIColor.FromWebColor("#087666").ToColor();
				break;
			case ThemeNames.OfficeColorfulYellow:
			case ThemeNames.OfficeWhiteYellow:
				accentColor = UIColor.FromWebColor("#e6aa11").ToColor();
				break;
		}

		switch (themeName) {
			case ThemeNames.MetroBlack:
			case ThemeNames.MetroDark:
			case ThemeNames.MetroLight:
			case ThemeNames.MetroWhite:
			case ThemeNames.OfficeColorfulBlue:
			case ThemeNames.OfficeColorfulGreen:
			case ThemeNames.OfficeColorfulIndigo:
			case ThemeNames.OfficeColorfulOrange:
			case ThemeNames.OfficeColorfulPink:
			case ThemeNames.OfficeColorfulPurple:
			case ThemeNames.OfficeColorfulRed:
			case ThemeNames.OfficeColorfulTeal:
			case ThemeNames.OfficeColorfulYellow:
			case ThemeNames.OfficeWhiteBlue:
			case ThemeNames.OfficeWhiteGreen:
			case ThemeNames.OfficeWhiteIndigo:
			case ThemeNames.OfficeWhiteOrange:
			case ThemeNames.OfficeWhitePink:
			case ThemeNames.OfficeWhitePurple:
			case ThemeNames.OfficeWhiteRed:
			case ThemeNames.OfficeWhiteTeal:
			case ThemeNames.OfficeWhiteYellow:
				borderColor = accentColor;
				break;
		}

		switch (themeName) {
			case ThemeNames.MetroBlack:
			case ThemeNames.MetroDark:
			case ThemeNames.MetroLight:
			case ThemeNames.MetroWhite:
			case ThemeNames.OfficeWhiteBlue:
			case ThemeNames.OfficeWhiteGreen:
			case ThemeNames.OfficeWhiteIndigo:
			case ThemeNames.OfficeWhiteOrange:
			case ThemeNames.OfficeWhitePink:
			case ThemeNames.OfficeWhitePurple:
			case ThemeNames.OfficeWhiteRed:
			case ThemeNames.OfficeWhiteTeal:
			case ThemeNames.OfficeWhiteYellow:
				isAccentBelow = true;
				break;
		}

		var accentBrush = new SolidColorBrush(accentColor);
		var backgroundBrush = new SolidColorBrush(backgroundColor);
		var borderBrush = new SolidColorBrush(borderColor);

		accentBrush.Freeze();
		backgroundBrush.Freeze();
		borderBrush.Freeze();

		var group = new DrawingGroup();

		var backgroundDrawing = new GeometryDrawing {
			Brush = backgroundBrush,
			Pen = new Pen(borderBrush, 1),
			Geometry = new RectangleGeometry(new Rect(0.5, 0.5, 15, 15))
		};
		group.Children.Add(backgroundDrawing);

		var accentDrawing = new GeometryDrawing {
			Brush = accentBrush,
			Geometry = new RectangleGeometry(new Rect(1, (isAccentBelow ? 11 : 1), 14, 4))
		};
		group.Children.Add(accentDrawing);

		var image = new Image() {
			Width = 16,
			Height = 16,
			Source = new DrawingImage() {
				Drawing = group
			}
		};

		return image;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
