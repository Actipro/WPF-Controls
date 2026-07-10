using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Themes;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.ThemeOverride;

/// <summary>
/// A theme catalog that describes the theme group name implementations and available themed resource dictionaries within this assembly.
/// </summary>
public class CustomThemeCatalog : ThemeCatalogBase {

	private static IEnumerable<ThemedResourceDictionaryReference>? _dictionaryReferences;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<ThemedResourceDictionaryReference> DictionaryReferences {
		get {
			if (_dictionaryReferences is null) {
				string baseUri = ResourceHelper.GetLocationUriStringBase(Assembly.GetExecutingAssembly()) + "ProductSamples/ThemesSamples/QuickStart/ThemeOverride/Includes/";

				_dictionaryReferences = [
					new ThemedResourceDictionaryReference() {
						LocationUri = new Uri(baseUri + "OfficeGreen.xaml", UriKind.RelativeOrAbsolute),
						Themes = [
							ThemeNames.OfficeColorfulGreen
						],
					},
					new ThemedResourceDictionaryReference() {
						LocationUri = new Uri(baseUri + "OfficeIndigo.xaml", UriKind.RelativeOrAbsolute),
						Themes = [
							ThemeNames.OfficeColorfulIndigo
						],
					},
					new ThemedResourceDictionaryReference() {
						AreThemesExclusive = true,
						LocationUri = new Uri(baseUri + "Other.xaml", UriKind.RelativeOrAbsolute),
						Themes = [
							ThemeNames.OfficeColorfulGreen,
							ThemeNames.OfficeColorfulIndigo
						],
					},
				];
			}
			return _dictionaryReferences;
		}
	}

}
