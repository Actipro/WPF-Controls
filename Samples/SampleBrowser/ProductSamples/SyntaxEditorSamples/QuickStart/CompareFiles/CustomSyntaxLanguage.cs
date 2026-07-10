using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments.Implementation;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;

/// <summary>
/// Represents a syntax language definition that renders backgrounds behind alternating rows.
/// </summary>
public class CustomSyntaxLanguage : CSharpSyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomSyntaxLanguage() : base() {
		// Register a tagger provider on the language as a service that can create real and imaginary difference tag objects
		RegisterService(new TextViewTaggerProvider<CompareFilesRealLinesTagger>(typeof(CompareFilesRealLinesTagger)));
		RegisterService(new TextViewTaggerProvider<CompareFilesImaginaryLinesTagger>(typeof(CompareFilesImaginaryLinesTagger)));

		// Register a provider service that can create the custom adornment managers for real and imaginary lines
		RegisterService(new AdornmentManagerProvider<CompareFilesRealLinesAdornmentManager>(typeof(CompareFilesRealLinesAdornmentManager)));
		RegisterService(new AdornmentManagerProvider<CompareFilesImaginaryLinesAdornmentManager>(typeof(CompareFilesImaginaryLinesAdornmentManager)));

		// Remove outlining since side-by-side views must be kept in sync
		this.UnregisterOutliner();
	}

}
