using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator {
    
    /// <summary>
	/// Represents a syntax language definition that renders a custom decorator under the text 'Actipro'.
    /// </summary>
    public class CustomSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomSyntaxLanguage</c> class.
		/// </summary>
		public CustomSyntaxLanguage() : base("CustomDecorator") {
			// Register a provider service that can create the custom adornment manager
			this.RegisterService(new AdornmentManagerProvider<CustomAdornmentManager>(typeof(CustomAdornmentManager)));

			// Register a tagger provider on the language as a service that can create CustomTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<CustomTagger>(typeof(CustomTagger)));

			// NOTE: Any other normal language services (lexer, parser, etc.) can be registered
			//   here too, but in this sample we are just showing adornments on a plain text language
		}
		
    }
	
}
