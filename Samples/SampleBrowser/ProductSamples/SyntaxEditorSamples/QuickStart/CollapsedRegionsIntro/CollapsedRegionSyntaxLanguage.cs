using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsIntro;

/// <summary>
/// Represents a syntax language definition that can collapse text regions.
/// </summary>
public class CollapsedRegionSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CollapsedRegionSyntaxLanguage() : base("CollapsedRegion") {
		// Initialize this language from a language definition
		SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

		// Register a tagger provider on the language as a service that can create CollapsedRegionTag objects
		RegisterService(new CodeDocumentTaggerProvider<CollapsedRegionTagger>(typeof(CollapsedRegionTagger)));
	}

}
