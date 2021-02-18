using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightWord {
    
    /// <summary>
	/// Represents a syntax language definition that renders a custom background behind higlighted words.
    /// </summary>
    public class CustomSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomSyntaxLanguage</c> class.
		/// </summary>
		public CustomSyntaxLanguage() : base("CustomDecorator") {
			// Initialize this language from a language definition
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

			// Register a tagger provider on the language as a service that can create CustomTag objects
			this.RegisterService(new TextViewTaggerProvider<WordHighlightTagger>(typeof(WordHighlightTagger)));
		}
		
    }
	
}
