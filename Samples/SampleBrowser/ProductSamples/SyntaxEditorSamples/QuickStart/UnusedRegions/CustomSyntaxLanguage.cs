using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions {
    
    /// <summary>
	/// Represents a syntax language definition that renders a squiggle under the text 'Actipro'.
    /// </summary>
    public class CustomSyntaxLanguage : CSharpSyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomSyntaxLanguage</c> class.
		/// </summary>
		public CustomSyntaxLanguage() {
			// Register a tagger provider on the language as a service that can track IUnusedRegion objects
			this.RegisterService(new CodeDocumentTaggerProvider<CustomUnusedRegionTagger>(typeof(CustomUnusedRegionTagger)));

			// Wrap the default C# parser with a custom one that can return additional information about unused regions
			this.RegisterParser(new CustomParser(this.GetParser()));
		}
		
    }
	
}
