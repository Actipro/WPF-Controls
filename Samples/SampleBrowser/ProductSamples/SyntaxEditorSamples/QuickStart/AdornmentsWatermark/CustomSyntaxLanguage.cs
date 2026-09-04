using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsWatermark;

/// <summary>
/// Represents a syntax language definition that makes a smoke text effect using adornments when text is changed.
/// </summary>
public class CustomSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomSyntaxLanguage() : base("Watermark") {
		// Register a provider service that can create the custom adornment manager
		RegisterService(new AdornmentManagerProvider<WatermarkAdornmentManager>(typeof(WatermarkAdornmentManager)));

		// NOTE: Any other normal language services (lexer, parser, etc.) can be registered
		//   here too, but in this sample we are just showing adornments on a plain text language
	}

}
