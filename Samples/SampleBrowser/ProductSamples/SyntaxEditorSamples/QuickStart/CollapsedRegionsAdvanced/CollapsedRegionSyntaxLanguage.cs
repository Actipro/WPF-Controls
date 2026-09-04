using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced;

/// <summary>
/// Represents a syntax language definition that can render intra-text collapsed region placeholders in the middle of text.
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

		// Register a provider service that can create the custom adornment manager
		RegisterService(new AdornmentManagerProvider<CollapsedRegionAdornmentManager>(typeof(CollapsedRegionAdornmentManager)));

		// Register a tagger provider on the language as a service that can create CollapsedRegionTag objects
		RegisterService(new CodeDocumentTaggerProvider<CollapsedRegionTagger>(typeof(CollapsedRegionTagger)));

		// Register a quick info provider for collapsed regions
		RegisterService(new CollapsedRegionQuickInfoProvider());
	}

}
