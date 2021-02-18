using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom {
    
	/// <summary>
	/// Provides <see cref="CustomIndicatorTag"/> objects over text ranges.
	/// </summary>
	public class CustomIndicatorTagger : IndicatorClassificationTaggerBase<CustomIndicatorTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomIndicatorTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public CustomIndicatorTagger(ICodeDocument document) : 
			base("CustomIndicator", new Ordering[] { 
				new Ordering(TaggerKeys.Token, OrderPlacement.Before)
			}, document, true) {}

	}

}
