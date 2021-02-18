using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsIntro {
    
	/// <summary>
	/// Provides <see cref="CollapsedRegionTag"/> objects over text ranges.
	/// </summary>
	public class CollapsedRegionTagger : CollectionTagger<ICollapsedRegionTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CollapsedRegionTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public CollapsedRegionTagger(ICodeDocument document) : base("CollapsedRegionTagger", null, document, true) {}
		
	}

}
