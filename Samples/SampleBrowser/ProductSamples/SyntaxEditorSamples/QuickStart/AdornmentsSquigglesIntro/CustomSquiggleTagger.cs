using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsSquigglesIntro;

/// <summary>
/// Provides <see cref="ISquiggleTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this manager is attached.</param>
public class CustomSquiggleTagger(ICodeDocument document) : CollectionTagger<ISquiggleTag>("ActiproPatternBasedSquiggle", orderings: null, document, isForLanguage: true) {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CustomSquiggleTagger() {
		// Register the classification type for a warning that will render the squiggle in green
		AmbientHighlightingStyleRegistry.Instance.Register(ClassificationTypes.Warning, new HighlightingStyle(Colors.Green));
	}

}
