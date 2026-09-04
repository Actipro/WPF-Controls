using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsSquigglesIntro;

/// <summary>
/// Represents a syntax language definition that renders a squiggle under the text 'Actipro'.
/// </summary>
public class CustomSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomSyntaxLanguage() : base("Squiggles") {
		// Register a tagger provider on the language as a service that can track ISquiggleTag objects
		RegisterService(new CodeDocumentTaggerProvider<CustomSquiggleTagger>(typeof(CustomSquiggleTagger)));

		// Register a squiggle tag quick info provider
		RegisterService(new SquiggleTagQuickInfoProvider());

		// NOTE: Any other normal language services (lexer, parser, etc.) can be registered
		//   here too, but in this sample we are just showing adornments on a plain text language
	}

}
