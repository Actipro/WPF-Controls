using System;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsColorPreview {
    
    /// <summary>
	/// Represents a syntax language definition that can render color previews.
    /// </summary>
    public class ColorPreviewSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ColorPreviewSyntaxLanguage</c> class.
		/// </summary>
		public ColorPreviewSyntaxLanguage() : base("ColorPreview") {
			// Initialize this language from a language definition
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "Css.langdef");

			// Register a tagger provider on the language as a service that can create ColorPreviewTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<ColorPreviewTagger>(typeof(ColorPreviewTagger)));

			// Register a provider service that can create the custom adornment manager
			this.RegisterService(new AdornmentManagerProvider<ColorPreviewAdornmentManager>(typeof(ColorPreviewAdornmentManager)));

		}
		
    }
	
}
