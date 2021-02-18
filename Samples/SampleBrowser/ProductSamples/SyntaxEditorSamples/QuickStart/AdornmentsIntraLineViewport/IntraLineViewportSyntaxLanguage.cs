using System;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport {
    
    /// <summary>
	/// Represents a syntax language definition that can render an adornment fixed within the text area.
    /// </summary>
    public class IntraLineViewportSyntaxLanguage : CSharpSyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraLineViewportSyntaxLanguage</c> class.
		/// </summary>
		public IntraLineViewportSyntaxLanguage() {
			// Register a provider service that can create the custom adornment manager
			this.RegisterService(new AdornmentManagerProvider<IntraLineViewportAdornmentManager>(typeof(IntraLineViewportAdornmentManager)));

			// Register a tagger provider on the language as a service that can create IntraLineViewportTag objects
			this.RegisterService(new TextViewTaggerProvider<IntraLineViewportTagger>(typeof(IntraLineViewportTagger)));
		}
		
    }
	
}
