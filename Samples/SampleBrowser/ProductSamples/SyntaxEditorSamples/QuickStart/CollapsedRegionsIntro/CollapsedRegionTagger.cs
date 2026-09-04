using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsIntro;

/// <summary>
/// Provides <see cref="CollapsedRegionTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this tagger is attached.</param>
public class CollapsedRegionTagger(ICodeDocument document)
	: CollectionTagger<ICollapsedRegionTag>("CollapsedRegionTagger", orderings: null, document, isForLanguage: true) { }
