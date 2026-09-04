using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLineStart;

/// <summary>
/// Represents a syntax language definition that performs custom classification, which drives syntax highlighting.
/// </summary>
public class CustomSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomSyntaxLanguage() : base("Custom") {
		// Register a tagger provider on the language as a service that can create IClassificationTag objects
		RegisterService(new CodeDocumentTaggerProvider<CustomClassificationTagger>());

		// NOTE: Any other normal language services (lexer, parser, etc.) can be registered
		//   here too, but in this sample we are just showing adornments on a plain text language
	}

}
