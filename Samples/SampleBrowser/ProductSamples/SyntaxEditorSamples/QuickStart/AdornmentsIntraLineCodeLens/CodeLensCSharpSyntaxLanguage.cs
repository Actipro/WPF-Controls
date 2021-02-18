using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
    /// <summary>
	/// Represents a <c>C#</c> syntax language definition that supports rendering adornments above certain declarations.
    /// </summary>
    public class CodeLensCSharpSyntaxLanguage : CSharpSyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CodeLensCSharpSyntaxLanguage</c> class.
		/// </summary>
		public CodeLensCSharpSyntaxLanguage() {
			// Register an updated parser that supports finding declarations within the parsing operation
			this.RegisterParser(new CodeLensCSharpParser());

			// Register a provider service that can create the custom adornment manager
			this.RegisterService(new AdornmentManagerProvider<CodeLensAdornmentManager>(typeof(CodeLensAdornmentManager)));

			// Register a tagger provider on the language as a service that can create CodeLensTag objects
			this.RegisterService(new CodeDocumentTaggerProvider<CodeLensTagger>(typeof(CodeLensTagger)));
		}
		
    }
	
}
