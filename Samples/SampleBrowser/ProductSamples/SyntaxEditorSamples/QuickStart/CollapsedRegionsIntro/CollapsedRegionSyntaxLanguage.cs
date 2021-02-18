using System;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsIntro {
    
    /// <summary>
	/// Represents a syntax language definition that can collapse text regions.
    /// </summary>
    public class CollapsedRegionSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CollapsedRegionSyntaxLanguage</c> class.
		/// </summary>
		public CollapsedRegionSyntaxLanguage() : base("CollapsedRegion") {
			// Initialize this language from a language definition
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

			// Register a tagger provider on the language as a service that can create CollapsedRegionTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<CollapsedRegionTagger>(typeof(CollapsedRegionTagger)));
		}
		
    }
	
}
