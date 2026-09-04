using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsColorPreview;

/// <summary>
/// Provides a tag for color previews.
/// </summary>
public class ColorPreviewTag : ITag {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The color.
	/// </summary>
	public Color Color { get; set; }

}
