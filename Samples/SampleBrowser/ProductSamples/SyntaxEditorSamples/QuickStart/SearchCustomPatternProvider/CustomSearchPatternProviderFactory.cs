using ActiproSoftware.Text.Searching;
using ActiproSoftware.Text.Searching.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchCustomPatternProvider;

/// <summary>
/// Implements a custom factory that creates <see cref="ISearchPatternProvider"/> objects.
/// </summary>
public class CustomSearchPatternProviderFactory : ISearchPatternProviderFactory {

	/// <inheritdoc cref="ISearchPatternProviderFactory.CreateProviders"/>
	public virtual ISearchPatternProviderCollection CreateProviders() {
		return new SearchPatternProviderCollection {
			SearchPatternProviders.Normal,
			CustomSearchPatternProvider.Instance,
			SearchPatternProviders.RegularExpression,
			SearchPatternProviders.Wildcard
		};
	}

}
