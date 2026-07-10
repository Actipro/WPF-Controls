namespace ActiproSoftware.Windows.Themes;

/// <summary>
/// A theme catalog that describes the theme group name implementations and available themed resource dictionaries within this assembly.
/// </summary>
public class BarsMvvmThemeCatalog : ThemeCatalogBase {

	private IEnumerable<ThemedResourceDictionaryReference>? _dictionaryReferences;

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	#region BarsMvvmThemedResourceDictionaryReference

	/// <summary>
	/// Provides a reference to a themed <see cref="ResourceDictionary"/> and describes its contents.
	/// </summary>
	private class BarsMvvmThemedResourceDictionaryReference : ThemedResourceDictionaryReference {

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc/>
		protected override ResourceDictionary GetResourceDictionary()
			=> BarsMvvmResourceDictionary.Instance;

	}

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<ThemedResourceDictionaryReference> DictionaryReferences
		=> _dictionaryReferences ??= [new BarsMvvmThemedResourceDictionaryReference()];

}
