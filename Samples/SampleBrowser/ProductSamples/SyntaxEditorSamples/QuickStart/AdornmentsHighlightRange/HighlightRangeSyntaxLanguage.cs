using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightRange;

/// <summary>
/// Represents a syntax language definition that can highlight ranges of text.
/// </summary>
public class HighlightRangeSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public HighlightRangeSyntaxLanguage() : base("HighlightRange") {

		SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

		// NOTE:
		//   This sample uses a custom syntax language initialized from C# that automatically registers
		//   the tagger with the language, but the tagger can be registered to any existing ISyntaxLanguage as well.

		// Register a tagger provider on the language as a service that can create HighlightRangeTag objects
		RegisterService(new CodeDocumentTaggerProvider<HighlightRangeTagger>(typeof(HighlightRangeTagger)));
	}

}
