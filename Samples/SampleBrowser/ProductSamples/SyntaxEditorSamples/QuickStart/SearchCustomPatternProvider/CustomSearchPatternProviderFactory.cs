using System;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Text.Searching.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchCustomPatternProvider {

	/// <summary>
	/// Implements a custom factory that creates <see cref="ISearchPatternProvider"/> objects.
	/// </summary>
	public class CustomSearchPatternProviderFactory : ISearchPatternProviderFactory {
			
		/// <summary>
		/// Creates the collection of <see cref="ISearchPatternProvider"/> objects to use.
		/// </summary>
		/// <returns>A <see cref="ISearchPatternProviderCollection"/> containing the <see cref="ISearchPatternProvider"/> objects that were created.</returns>
		public virtual ISearchPatternProviderCollection CreateProviders() {
			ISearchPatternProviderCollection providers = new SearchPatternProviderCollection();
			providers.Add(SearchPatternProviders.Normal);
			providers.Add(CustomSearchPatternProvider.Instance);
			providers.Add(SearchPatternProviders.RegularExpression);
			providers.Add(SearchPatternProviders.Wildcard);
			return providers;
		}
		
	}
}
