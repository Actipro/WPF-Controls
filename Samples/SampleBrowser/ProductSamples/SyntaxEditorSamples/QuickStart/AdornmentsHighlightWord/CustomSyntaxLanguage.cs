using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightWord;

/// <summary>
/// Represents a syntax language definition that renders a custom background behind highlighted words.
/// </summary>
public class CustomSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomSyntaxLanguage() : base("CustomDecorator") {
		// Initialize this language from a language definition
		SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

		// Register a tagger provider on the language as a service that can create CustomTag objects
		RegisterService(new TextViewTaggerProvider<WordHighlightTagger>(typeof(WordHighlightTagger)));
	}

}
