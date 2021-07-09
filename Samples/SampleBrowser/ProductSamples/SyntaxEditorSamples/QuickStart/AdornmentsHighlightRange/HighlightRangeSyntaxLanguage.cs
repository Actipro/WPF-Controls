using System;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightRange {
    
    /// <summary>
	/// Represents a syntax language definition that can highlight ranges of text.
    /// </summary>
    public class HighlightRangeSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>HighlightRangeSyntaxLanguage</c> class.
		/// </summary>
		public HighlightRangeSyntaxLanguage() : base("HighlightRange") {
			
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

			// Implementation Note:
			//
			// This sample uses a custom syntax language initialized from C# that automatically registers
			// the tagger with the language, but the tagger can be registered to any existing ISyntaxLanguage as well.

			// Register a tagger provider on the language as a service that can create HighlightRangeTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<HighlightRangeTagger>(typeof(HighlightRangeTagger)));
		}
		
    }
	
}
