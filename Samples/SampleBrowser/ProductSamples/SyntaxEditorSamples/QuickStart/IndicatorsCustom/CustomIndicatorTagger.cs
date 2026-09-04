using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom;

/// <summary>
/// Provides <see cref="CustomIndicatorTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this manager is attached.</param>
public class CustomIndicatorTagger(ICodeDocument document)
	: IndicatorClassificationTaggerBase<CustomIndicatorTag>("CustomIndicator", [new Ordering(TaggerKeys.Token, OrderPlacement.Before)], document, isForLanguage: true) { }
