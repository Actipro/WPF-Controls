using System;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {
    
    /// <summary>
	/// Represents a syntax language definition that can render notes in the middle of text.
    /// </summary>
    public class IntraTextNoteSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraTextNoteSyntaxLanguage</c> class.
		/// </summary>
		public IntraTextNoteSyntaxLanguage() : base("IntraTextNote") {
			// Initialize this language from a language definition
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "CSharp.langdef");

			// Register a provider service that can create the custom adornment manager
			this.RegisterService(new AdornmentManagerProvider<IntraTextNoteAdornmentManager>(typeof(IntraTextNoteAdornmentManager)));

			// Register a tagger provider on the language as a service that can create IntraTextNoteTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<IntraTextNoteTagger>(typeof(IntraTextNoteTagger)));
		}
		
    }
	
}
