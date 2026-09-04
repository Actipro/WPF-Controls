using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Provides an <see cref="IIntraLineSpacerTag"/> implementation that reserves intra-line space for an element.
/// </summary>
public class CodeLensTag(CodeLensDeclaration declaration) : IIntraLineSpacerTag {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IIntraLineSpacerTag.BottomMargin"/>
	public double BottomMargin { get; set; }

	/// <summary>
	/// The <see cref="CodeLensDeclaration"/> related to this tag.
	/// </summary>
	public CodeLensDeclaration Declaration { get; } = declaration ?? throw new ArgumentNullException(nameof(declaration));

	/// <inheritdoc cref="IIntraLineSpacerTag.Key"/>
	public object Key
		=> Declaration.Key;

	/// <inheritdoc cref="IIntraLineSpacerTag.TopMargin"/>
	public double TopMargin { get; set; }

}
