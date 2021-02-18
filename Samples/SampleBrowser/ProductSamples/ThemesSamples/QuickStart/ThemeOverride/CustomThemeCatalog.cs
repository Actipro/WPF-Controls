using System;
using System.Collections.Generic;
using System.Reflection;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.ThemeOverride {

	/// <summary>
	/// A theme catalog that describes the theme group name implementations and available themed resource dictionaries within this assembly.
	/// </summary>
	public class CustomThemeCatalog : ThemeCatalogBase {

		private static IEnumerable<ThemedResourceDictionaryReference> dictionaryReferences;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of <see cref="ThemedResourceDictionaryReference"/> objects for the assembly.
		/// </summary>
		/// <value>
		/// The collection of <see cref="ThemedResourceDictionaryReference"/> objects for the assembly.
		/// </value>
		public override IEnumerable<ThemedResourceDictionaryReference> DictionaryReferences {
			get {
				if (dictionaryReferences == null) {
					string baseUri = ResourceHelper.GetLocationUriStringBase(Assembly.GetExecutingAssembly()) + "ProductSamples/ThemesSamples/QuickStart/ThemeOverride/Includes/";

					dictionaryReferences = new ThemedResourceDictionaryReference[] {
						new ThemedResourceDictionaryReference() { 
							LocationUri = new Uri(baseUri + "OfficeGreen.xaml", UriKind.RelativeOrAbsolute),
							Themes = new string[] { 
								ThemeNames.OfficeColorfulGreen
							},
						},
						new ThemedResourceDictionaryReference() { 
							LocationUri = new Uri(baseUri + "OfficeIndigo.xaml", UriKind.RelativeOrAbsolute),
							Themes = new string[] { 
								ThemeNames.OfficeColorfulIndigo
							},
						},
						new ThemedResourceDictionaryReference() { 
							AreThemesExclusive = true,
							LocationUri = new Uri(baseUri + "Other.xaml", UriKind.RelativeOrAbsolute),
							Themes = new string[] { 
								ThemeNames.OfficeColorfulGreen,
								ThemeNames.OfficeColorfulIndigo
							},
						},
					};
				}
				return dictionaryReferences;
			}
		}

	}

}
