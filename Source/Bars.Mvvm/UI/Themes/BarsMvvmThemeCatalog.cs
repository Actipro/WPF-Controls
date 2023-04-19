using System.Collections.Generic;
using System.Windows;

namespace ActiproSoftware.Windows.Themes {

	/// <summary>
	/// A theme catalog that describes the theme group name implementations and available themed resource dictionaries within this assembly.
	/// </summary>
	public class BarsMvvmThemeCatalog : ThemeCatalogBase {

		private IEnumerable<ThemedResourceDictionaryReference> dictionaryReferences;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region BarsMvvmThemedResourceDictionaryReference

		/// <summary>
		/// Provides a reference to a themed <see cref="ResourceDictionary"/> and describes its contents.
		/// </summary>
		private class BarsMvvmThemedResourceDictionaryReference : ThemedResourceDictionaryReference {
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Gets a <see cref="ResourceDictionary"/> instance.
			/// </summary>
			/// <returns>The <see cref="ResourceDictionary"/> instance.</returns>
			protected override ResourceDictionary GetResourceDictionary()
				=> BarsMvvmResourceDictionary.Instance;

		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of <see cref="ThemedResourceDictionaryReference"/> objects for the assembly.
		/// </summary>
		/// <value>
		/// The collection of <see cref="ThemedResourceDictionaryReference"/> objects for the assembly.
		/// </value>
		public override IEnumerable<ThemedResourceDictionaryReference> DictionaryReferences
			=> dictionaryReferences ??= new ThemedResourceDictionaryReference[] {
				new BarsMvvmThemedResourceDictionaryReference()
			};

	}

}
